using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingSystem.Data.Interfaces;
using TrackingSystem.Data.Models;
using TrackingSystem.Data.Helpers;
namespace TrackingSystem.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUser db;

        // GET: Users
        public UsersController(IUser db)
        {
            this.db = db;
        }
    
        public ActionResult Sort(string Sorting_Order)
        {
            var model = db.GetAll();
            switch (Sorting_Order)
            {
                case "Username_Desc":
                    model = model.OrderByDescending(m => m.Username);
                    break;
                case "Date_Asc":
                    model = model.OrderBy(m => m.CreatedDate);
                    break;
                case "Date_Desc":
                    model = model.OrderByDescending(m => m.CreatedDate);
                    break;
                case "UserType_Asc":
                    model = model.OrderBy(m => m.UserType);
                    break;
                case "UserType_Desc":
                    model = model.OrderByDescending(m => m.UserType);
                    break;
                default:
                    model = model.OrderBy(m => m.Username);
                    break;
            }
            return View(model.ToList());
        }
        [HttpGet]
        public ActionResult Index(string item)
        {
            if (String.IsNullOrEmpty(item))
            {
                var model = db.GetAll();
                return View(model);
            }
            else
            {
                var model = db.Search(item);
                return View("Index", model);
            }
        }

        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {

            if (ModelState.IsValid)
            {
                db.Add(user);
                var model = db.GetByUsername(user.Username);
                return View("Details", model);
            };
            return View();
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = db.Get(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(User user)
        {
            var model = db.Get(user.Id);            

            if (model != null)
            {
                db.Update(user);
                return View("Details", model);
            }
            return View("NotFound");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var allUsers = db.GetAll();
            var currentUser = db.GetByUsername(Session["CurrentUser"].ToString());
            if(currentUser.Id != id)
            {
                SweetAlertHelper.ShowMessage("Warning", "Are you sure you want to delete this user?", SweetAlertMessageType.warning);                                
                return View();
            }
            else
            {
                SweetAlertHelper.ShowMessage("Warning", "User is currently logged in.", SweetAlertMessageType.warning);
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Delete(User user)
        {
            var model = db.Get(user.Id);
            var getAllmodel = db.GetAll();
            var getCurrentUser = db.GetByUsername(Session["CurrentUser"].ToString());
            if (getCurrentUser.Id != user.Id)
            {
                if (user.Id >= 2)
                {
                    if (model != null)
                    {
                        db.Delete(model.Id);
                        return View("Index", getAllmodel);
                    }
                    else
                    {
                        return View("NotFound");
                    }
                }
                else
                {
                    ViewBag.SweetAlertShowMessage = SweetAlertHelper.Delete("Error", "You cannot delete default users!", SweetAlertMessageType.error);
                    return View("Index", getAllmodel);
                }
            }
            else
            {
                ViewBag.SweetAlertShowMessage = SweetAlertHelper.Delete("Error", "User is currently logged in.", SweetAlertMessageType.error);
                return View("Index", getAllmodel);
            }
        }
        //[HttpPut]
        //public ActionResult Delete(int id)
        //{
        //    var model = db.Get(id);

        //    if (model != null)
        //    {
        //        db.Delete(id);
        //        return View("Index");
        //    };

        //    return View("NotFound");
        //}
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = db.Get(id);
            if (model != null)
            {
                return View(model);
            }
            return View("NotFound");
        }
        public ActionResult NotFound()
        {
            return View();
        }

    }
}