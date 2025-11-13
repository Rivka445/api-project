using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;
using Zxcvbn;

namespace Services
{
    public class UserPasswordService
    {
        public UserPasswordRipository userPasswordRipo = new UserPasswordRipository();
        public int checkPassword(string password)
        {
          return Zxcvbn.Core.EvaluatePassword(password).Score;

        }
    }
}
