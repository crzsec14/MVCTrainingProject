using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TrackingSystem.Data.Enums;
using TrackingSystem.Data.Helpers;
using TrackingSystem.Data.Interfaces;
using TrackingSystem.Data.Models;

namespace TrackingSystem.Web.Controllers
{

    public class AuthController : Controller
    {
        private readonly IUser db;

        // GET: Login
        public AuthController(IUser db)
        {
            this.db = db;
        }

        public ActionResult Login()
        {
            var model = new User();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var userExist = db.Login(user);
            if (userExist)
            {
                var model = db.GetByUsername(user.Username);
                db.UpdateUserStatus(Status.LoggedIn, model.Id);
                var identity = new ClaimsIdentity("ApplicationCookies");
                identity.AddClaim(new Claim("ID", model.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                HttpContext.GetOwinContext().Authentication.SignIn(identity);
                Session["CurrentUser"] = identity.Name;
                ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Success", "Successfully logged in.", SweetAlertMessageType.success);
                return View();
            }
            else
            {
                ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Error", "Username or password is incorrect.", SweetAlertMessageType.error);
                return View();
            }
        }
        public ActionResult Logout()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var id = identity.Claims.FirstOrDefault().Value;
            db.UpdateUserStatus(Status.LoggedOut, Convert.ToInt32(id));
            Session["CurrentUser"] = null;
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
        public ActionResult LoginFacebook()
        {
            HttpContext.GetOwinContext().Authentication.Challenge(new Microsoft.Owin.Security.AuthenticationProperties
            {
                RedirectUri = "/Users"
            }, "Facebook");

            return new HttpUnauthorizedResult();
        }
    }
}