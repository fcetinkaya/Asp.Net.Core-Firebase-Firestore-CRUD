using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using BS_Core_WepApp.Key;
using BS_Core_WepApp.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Storage;
using BS_Core_WepApp.Services;

namespace BS_Core_WepApp.Controllers
{
    public class StorageController : Controller
    {
        private IHostEnvironment _env;
        private IJavaScriptService _javascriptService;
        FirestoreDb firestoreDb;
        public StorageController(IHostEnvironment env, IJavaScriptService javaScriptService)
        {
            _javascriptService = javaScriptService;
            _env = env;
            string keyPath = Path.Combine(_env.ContentRootPath, "Key\\nodejs-tutorial-4dbfe-firebase-adminsdk-muv6v-7944c5c977.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyPath);
            firestoreDb = FirestoreDb.Create(cls_keys.projectId);
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("bt_userToken") != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Index(UserModel userModel)
        {
            string _strToken = HttpContext.Session.GetString("bt_userToken");
            Query userquery = firestoreDb.Collection("fthTest-users");
            QuerySnapshot documentSnapshots = await userquery.GetSnapshotAsync();
            List<UserModel> lstUser = new List<UserModel>();

            foreach (DocumentSnapshot item in documentSnapshots.Documents)
            {
                if (item.Exists)
                {
                    Dictionary<string, object> user = item.ToDictionary();
                    string jsonUser = JsonConvert.SerializeObject(user);
                    userModel = JsonConvert.DeserializeObject<UserModel>(jsonUser);

                    userModel.Id = item.Id.ToString();
                    userModel.date = item.CreateTime.Value.ToDateTime();
                    lstUser.Add(userModel);
                }
            }
            List<UserModel> sortUserList = lstUser.OrderBy(x => x.date).ToList();
            return View(sortUserList);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("bt_userToken") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel userModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string _strToken = HttpContext.Session.GetString("bt_userToken");
                DocumentReference docRef = await firestoreDb.Collection("fthTest-users").AddAsync(userModel);

                // Insert Storage
                string link = null;
                FileStream fs = null;
                if (file.Length > 0)
                {
                    string path = Path.Combine(_env.ContentRootPath, "upload");
                    string fileName = $"f_{Guid.NewGuid()}-{file.FileName}";
                    using (var memoyrStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(memoyrStream);
                    }
                    using (fs = new FileStream(Path.Combine(path, fileName), FileMode.Open))
                    {
                        var cancellation = new CancellationTokenSource();
                        var upload = new FirebaseStorage(
                              cls_keys.BucketFile,
                               new FirebaseStorageOptions
                               {
                                   AuthTokenAsyncFactory = () => Task.FromResult(_strToken),
                                   ThrowOnCancel = true
                               })
                               .Child("fthTest-users")
                              .Child(docRef.Id)
                               .Child(fileName)
                               .PutAsync(fs, cancellation.Token);

                        // error during upload will be thrown when await the task
                        link = await upload;
                    }
                    System.IO.File.Delete(Path.Combine(path, fileName));

                    // Update image path
                    await docRef.UpdateAsync("logoPath", link);
                }
                return RedirectToAction("Index", "Storage");
            }
            return View();
        }

        public IActionResult Edit(string? id)
        {
            if (HttpContext.Session.GetString("bt_userToken") != null)
            {
                DocumentReference docRef = firestoreDb.Collection("fthTest-users").Document(id);
                DocumentSnapshot snapshot = docRef.GetSnapshotAsync().GetAwaiter().GetResult();
                UserModel userModel = snapshot.ConvertTo<UserModel>();
                userModel.Id = snapshot.Id;
                userModel.date = snapshot.CreateTime.Value.ToDateTime();
                return View(userModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserModel userModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string _strToken = HttpContext.Session.GetString("bt_userToken");
                DocumentReference colref = firestoreDb.Collection("fthTest-users").Document(userModel.Id);
                colref.SetAsync(userModel).GetAwaiter().GetResult();

                string link = null;
                FileStream fs = null;
                string path = Path.Combine(_env.ContentRootPath, "upload");
                string fileName = $"f_{Guid.NewGuid()}-{file.FileName}";
                using (var memoyrStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(memoyrStream);
                }
                using (fs = new FileStream(Path.Combine(path, fileName), FileMode.Open))
                {
                    var cancellation = new CancellationTokenSource();
                    var upload = new Firebase.Storage.FirebaseStorage(
                          cls_keys.BucketFile,
                           new Firebase.Storage.FirebaseStorageOptions
                           {
                               AuthTokenAsyncFactory = () => Task.FromResult(_strToken),
                               ThrowOnCancel = true
                           })
                           .Child("fthTest-users")
                          .Child(colref.Id)
                           .Child(fileName)
                           .PutAsync(fs, cancellation.Token);

                    // error during upload will be thrown when await the task
                    link = await upload;
                }
                System.IO.File.Delete(Path.Combine(path, fileName));

                // Update image path
                await colref.UpdateAsync("logoPath", link);
                return RedirectToAction("Index", "Storage");
            }
            return View();
        }

        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (HttpContext.Session.GetString("bt_userToken") != null)
            {
                DocumentReference docRef = firestoreDb.Collection("fthTest-users").Document(id);
                DocumentSnapshot snapshot = docRef.GetSnapshotAsync().GetAwaiter().GetResult();
                UserModel userModel = snapshot.ConvertTo<UserModel>();
                userModel.Id = snapshot.Id;
                return View(userModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(UserModel userModel)
        {
            DocumentReference usrRef = firestoreDb.Collection("fthTest-users").Document(userModel.Id);
            usrRef.DeleteAsync().GetAwaiter().GetResult();
            return RedirectToAction("Index", "Storage");
        }

        public IActionResult Details(string id)
        {
            if (HttpContext.Session.GetString("bt_userToken") != null)
            {
                if (id == null)
                {
                    return BadRequest();
                }

                DocumentReference docRef = firestoreDb.Collection("fthTest-users").Document(id);
                DocumentSnapshot snapshot = docRef.GetSnapshotAsync().GetAwaiter().GetResult();
                if (snapshot.Exists)
                {
                    UserModel usr = snapshot.ConvertTo<UserModel>();
                    usr.Id = snapshot.Id;
                    usr.date = snapshot.CreateTime.Value.ToDateTime();
                    return View(usr);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
}