using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystem.Data.Interfaces
{
    public interface IPasswordHasher
    {
        bool CompareHashToPassword(string hash, string password);
        string Hash(string password);
    }
}
