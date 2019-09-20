using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrackingSystem.Data.Interfaces;
using TrackingSystem.Data.Models;
using Newtonsoft.Json;


namespace TrackingSystem.Web.WebApi
{
    //[RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUser db;

        public UsersController(IUser db)
        {
            this.db = db;
        }
        
        [HttpGet]
        public User Get(int id)
        {
            var model = db.Get(id);
            return model;
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            try
            {
                var model = db.GetAll();
                if (model != null)
                    return db.GetAll();
                else
                    return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //[Route("api/users/create/{user}")]
        [HttpPost]
        public IHttpActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Add(user);
                var model = db.GetAll();
                return Ok(Json(model));
            }
            else
            {
                return BadRequest("Invalid Request!");
            }
        }
        //[Route("api/users/update/{user}")]
        //[HttpPost]
        //public IHttpActionResult Update(User user)
        //{
        //    try
        //    {
        //        IEnumerable<User> model;
        //        if (ModelState.IsValid)
        //        {
        //            db.Update(user);
        //            model = db.GetAll();
        //            return Ok(Json(model));
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid request!");
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return BadRequest("Invalid request!");
        //    }
        //}
        //[Route("api/users/delete/{id:int}")]
        //[HttpPost]
        //public IHttpActionResult Delete(int id)
        //{
        //    var model = db.Get(id);
        //    var displayAll = db.GetAll();
        //    if (model != null)
        //    {
        //        db.Delete(id);
        //        return Json(displayAll);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
