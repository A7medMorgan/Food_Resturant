using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant
{
    class Admin
    {
        const string username_admin = "Ahmed";
        const string password_admin = "123";

        public Boolean login (string email, string pass)
        {
            if (email.Equals(username_admin) && pass.Equals(password_admin))
            {
                return true;
            }
            else { return false; }
        }
    }
}
