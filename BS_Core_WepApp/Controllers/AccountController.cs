using BS_Core_WepApp.Key;
using BS_Core_WepApp.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BS_Core_WepApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(cls_keys.ApiKey));
                    var a = await auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.Name, true);
                    ModelState.AddModelError(string.Empty, "Please Verify your email then login.");
                    ModelState.Clear();
                }
                else
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                    ModelState.AddModelError(string.Empty, errors.ToString());
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(cls_keys.ApiKey));
                await auth.SendPasswordResetEmailAsync(forgot.Email);
                ModelState.AddModelError(string.Empty, "The password link has been sent to your e-mail address.");
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ModelState.Clear();
            }
            return View();
        }

        [HttpPost]
        public IActionResult LogOff()
        {
            HttpContext.Session.Remove("bt_userToken");
            HttpContext.Session.Remove("bt_userEmail");
            HttpContext.Session.Remove("bt_userRole");
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}