using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackingSystem.Data.Database;
using TrackingSystem.Data.Enums;
using TrackingSystem.Data.Interfaces;
using TrackingSystem.Data.Models;


namespace TrackingSystem.Data.Services
{
    public class UserService : IUser
    {
        private readonly TrackingSystemDbContext db;

        IPasswordHasher passwordHasher = new PasswordHasher();
        public UserService(TrackingSystemDbContext db)
        {
            this.db = db;
        }
        public void Add(User user)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var id = identity.Claims.FirstOrDefault().Value;

            User user1 = new User()
            {
                Id = user.Id,
                Username = user.Username,
                Password = passwordHasher.Hash(user.Password),
                Attempts = 0,
                CreatedBy = Convert.ToInt32(id),
                CreatedDate = DateTime.Now,
                Status = Status.None,
                UserType = user.UserType
            };
            db.Users.Add(user1);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public User Get(int id)
        {
            return db.Users.FirstOrDefault(usr => usr.Id == id);
        }
        //public List<User> Sort(string sort_order)
        //{
        //    var model = from usr in db.Users select usr;
            
        //    return model.ToList();
        //}
        public IEnumerable<User> GetAll()
        {
            //var val = db.Users.FirstOrDefault();
            return from usr in db.Users
                   select usr;

        }

        public void Update(User user)
        {
            var model = Get(user.Id);

            User user1 = new User()
            {
                Id = user.Id,
                Username = user.Username,
                Password = passwordHasher.Hash(user.Password),
                UserType = user.UserType,
                Attempts = model.Attempts,
                CreatedBy = model.CreatedBy,
                Status = model.Status,
                CreatedDate = model.CreatedDate
            };
            var entity = db.Users.Where(u => u.Id == user.Id).AsQueryable().FirstOrDefault();
            db.Entry(entity).CurrentValues.SetValues(user1);
            db.SaveChanges();
        }
        public bool Login(User user)
        {
            var password = passwordHasher.Hash(user.Password).ToString();
            var model = db.Users.Any(u => u.Username == user.Username && u.Password == password);
            return model;
        }

        public void UpdateUserStatus(Status status, int id)
        {
            if (status == Status.LoggedIn)
            {
                UserStatus userStatus = new UserStatus()
                {
                    UserID = id,
                    LoggedInDate = DateTime.Now,
                    LoggedOutDate = null,
                };
                db.UserStatus.Add(userStatus);
            }
            else if (status == Status.LoggedOut)
            {
                UserStatus userStatus = new UserStatus()
                {
                    UserID = id,
                    LoggedInDate = null,
                    LoggedOutDate = DateTime.Now,
                };
                db.UserStatus.Add(userStatus);
            }
            else
            {
                return;
            }

            db.SaveChanges();
        }

        public User GetByUsername(string username)
        {
            return db.Users.FirstOrDefault(usr => usr.Username == username);
        }

        public IEnumerable<User> Search(string data)
        {
            return from usr in db.Users
                   where usr.Username.Contains(data) || usr.Username.Contains(data)
                   select usr;
        }
    }
}
