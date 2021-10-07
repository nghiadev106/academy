using Academy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repositories
{
    public interface IAccountRepository
    {
        int Login(string userName, string passWord);
        AspNetUsers GetUserDetail(string email, string passWord);
    }
    public class AccountRepository : IAccountRepository
    {
        AcademyEntities db = new AcademyEntities();

        public AspNetUsers GetUserDetail(string email, string passWord)
        {
            return db.AspNetUsers.SingleOrDefault(x => x.Email == email && x.PasswordHash == passWord);
        }

        public int Login(string email, string passWord)
        {
            var result = db.AspNetUsers.SingleOrDefault(x => x.Email == email);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.LockoutEnabled == true)
                {
                    return -1;
                }
                else
                {
                    if (result.PasswordHash == passWord)
                        return 1;
                    else
                        return -2;
                }

            }
        }
    }
}
