using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CompanyStructuresWebAPI.Model;

namespace CompanyStructuresWebAPI.Helper
{
    public class Authenticator
    {
        public static bool isAuthenticated(string authBase64)
        {
            if (authBase64 != null)
            {
                authBase64 = authBase64.Remove(0, 6);

                string decodedKey = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authBase64));

                string[] authData = decodedKey.Split(":");

                User user = new User();
                user.Name = authData[0];
                user.Password = authData[1];

                if ((user.Name == "KnarfRetlawReiemniets") && (user.Password == "Pw"))
                    return true;

                else
                    return false;
            }

            else
                return false;
        }
    }
}
