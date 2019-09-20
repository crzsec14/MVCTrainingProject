using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystem.Data.Enums;
using TrackingSystem.Data.Models;

namespace TrackingSystem.Data.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> GetAll();
        
        //User GetAll()
        IEnumerable<User> Search(string data);
        User Get(int id);
        User GetByUsername(string username);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        void UpdateUserStatus(Status status, int id);
        bool Login(User ser);
    }
}
