using PhoneAPI.Assets.Contain;
using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models
{
    public class UserMasterRepository : IDisposable
    {
        // SECURITY_DBEntities it is your context class
        PhoneStoreEntities3 context = new PhoneStoreEntities3();
        //This method is used to check and validate the user credentials
        public Account ValidateUser(string username, string password)
        {
            var passWord = Const.CreateMD5(password);
            return context.Accounts.FirstOrDefault(user =>
            user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.PassWord == passWord);
        }

        public bool CheckWrongUserName(string username, string password)
        {
            var passWord = Const.CreateMD5(password);
            Account accountUsernameCheck = context.Accounts.FirstOrDefault(user =>
            user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            if(accountUsernameCheck == null)
            {
                return true; //Sai tài khoản
            }
            return false; //Sai mật khẩu
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}