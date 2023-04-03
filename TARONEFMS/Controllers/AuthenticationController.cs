using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TARONEFMS.Models;
using TARONEFMS.Utilities;

namespace TARONEFMS.Controllers
{
    
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel ulm)
        {

            LoginUtil lu = new LoginUtil();
            UserLoginModel ulogged = new UserLoginModel();
            ulogged = lu.GetUserLoginByCredentials(ulm.UserId, ulm.Password);
            if (ulogged != null)
            {
                FormsAuthentication.SetAuthCookie(ulogged.Id.ToString(), false);
                //System.Web.HttpContext.Current.Session["Id"] = ulogged.Id;
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("CredentialError", "User is not exist or User Id and Password is incorrect.");
                return View("Login");
            }
        }

        public ActionResult Signout()
        {
            System.Web.HttpContext.Current.Session.RemoveAll();
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authentication");
        }
    }
}