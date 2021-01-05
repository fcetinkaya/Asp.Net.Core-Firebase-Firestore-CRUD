# Asp.net Core MVC - Firebase Tutorial
> Asp.net Core MVC - Firebase CRUD {Firestore and Storage}, SignUp and SignIn

## Table of contents
* [General info](#general-info)
* [Screenshots](#screenshots)
* [Technologies](#technologies)
* [Code Examples](#code-examples)

## General info
<h4>Firebase Firestore CRUD</h4>
<h4>Firebase File Upload on Storage</h4>
<h4>Firebase Authentication</h4>

1. Add Nugets for Owin Security and Firebase Authentication
2. Add Firebase Api Key for Configuration
3. Account Controller For Signup,Login and Logoff Methods
4. Add Models for Signup and Login
5. Add Owin Startup Authentication class and Method
6. Verify User from Firebase Authentication then signin user and add user info to Claim Identities


<h4>Firebase CRUD</h4>

1. Register new user Account

2. Create Project In Firebase
3. Copy Secret Key for Authentication
4. Create Realtime Database and Copy database Path (Secret Key and Database Path use to Firebase Configuration in C#)
5. Create Asp.net Core MVC Project
6. Add Firesharp nugget and Add reference in project
7. Add Model and Controller.
8. Firebase Configuration in Controller.


<h4> Firebase Storage</h4>

1. Install Nuget for Firebase Storage and Auth
2. Post file to View
3. Firebase Storage Connection
4. File Upload to Bucket

## Screenshots
![Example screenshot](BS_Core_WepApp/ScreenShot/ScreenShot/Account-SignUp.jpg)
![Example screenshot](./BS_Core_WepApp/ScreenShot/Account-SignIn.jpg)
![Example screenshot](../ScreenShot/Account-ForgotPassword.jpg)
![Example screenshot](../ScreenShot/Storage-index.png)
![Example screenshot](../ScreenShot/Storage-Create.png)
![Example screenshot](../ScreenShot/Storage-Delete.png)
![Example screenshot](../ScreenShot/Storage-Details.png)
![Example screenshot](../ScreenShot/Storage-Edit.jpg)

## Technologies
* .NET Framework 4.6.1+
* .NET Standard 3.1, providing .NET Core support
* Install latest version of Visual Studio : https://www.visualstudio.com/downloads/
* ASP.NET MVC Pattern : https://dotnet.microsoft.com/apps/aspnet/mvc
* Firebase : https://firebase.google.com/

## Code Examples
Show examples of usage:
```
 public StorageController(IHostEnvironment env, IJavaScriptService javaScriptService)
        {
            _javascriptService = javaScriptService;
            _env = env;
            string keyPath = Path.Combine(_env.ContentRootPath, "Key\\nodejs-tutorial-4dbfe-firebase-adminsdk-muv6v-7944c5c977.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyPath);
            firestoreDb = FirestoreDb.Create(cls_keys.projectId);
        }
```
