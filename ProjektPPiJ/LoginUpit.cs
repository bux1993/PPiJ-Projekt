using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektPPiJ.Models;

namespace ProjektPPiJ
{
    public class LoginUpit
    {

        private BazaEntities db;

        public bool LoginUspjesan(string username, string password)
        {
            var user = db.UserInfo.Where(u => u.Username.Equals(username));
            if (user == null)
            {
                Console.WriteLine("Bro do you even exist");
                return false;
            }
            if ((user as UserInfo).Password.Equals(password))
            {
                Console.WriteLine("bravo buraz");
                return true;
            }
            Console.WriteLine("Krivi pass moronu");
            return false;
        }
    }
}