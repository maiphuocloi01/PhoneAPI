using Microsoft.Owin.Security.OAuth;
using PhoneAPI.Assets.Contain;
using PhoneAPI.Models.DTO;
using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PhoneAPI.Models.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        private static OAuthGrantResourceOwnerCredentialsContext context;

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        PhoneStoreEntities3 db = new PhoneStoreEntities3();

        public async Task<List<AccountDTO>> GetAllAccount()
        {
            var AccountList = (await db.Accounts
                        .ToListAsync())
                        .Select(account => new AccountDTO(account))
                        .ToList();
            return AccountList;
        }

        public async Task<AccountDTO> AdminLogin(AccountDTO accountDTO)
        {
            var userName = accountDTO.UserName;
            var passWord = accountDTO.PassWord;

            passWord = Const.CreateMD5(passWord);

            try
            {
                var myAccount = await db.Accounts.SingleOrDefaultAsync(account => account.UserName == userName && account.PassWord == passWord);
                if (myAccount != null && myAccount.Role.Equals("Admin"))
                {

                    return new AccountDTO(myAccount);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<AccountDTO> GetAccountById(int Id)
        {
            try
            {
                var myAccount = await db.Accounts.SingleOrDefaultAsync(account => account.Id == Id);
                if (myAccount != null)
                {
                    return new AccountDTO(myAccount);
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<AccountDTO> GetAccountByUserName(string userName)
        {
            try
            {
                var myAccount = await db.Accounts.SingleOrDefaultAsync(account => account.UserName == userName);
                if (myAccount != null)
                {
                    
                    return new AccountDTO(myAccount);
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<int> IsRegisterAble(AccountDTO accountDTO)
        {
            if (await db.Accounts.SingleOrDefaultAsync(c => c.UserName == accountDTO.UserName || c.Email == accountDTO.Email) != null)
            {
                return -1; //Tên đăng nhập hoặc email đã tồn tại
            }
            return await Const.VerifyEmail(accountDTO.Email) ? 1 : -2; //1: có thể đăng ký; -2: email không hợp lệ
        }

        public async Task<int> Register(AccountDTO accountDTO)
        {
            try
            {
                string passWord = Const.CreateMD5(accountDTO.PassWord);

                Account account = new Account()
                {
                    UserName = accountDTO.UserName,
                    PassWord = passWord,
                    FullName = accountDTO.FullName,
                    PhoneNumber = accountDTO.PhoneNumber,
                    Email = accountDTO.Email,
                    Birthday = accountDTO.Birthday,
                    Gender = accountDTO.Gender,
                    Role = "User",
                    //IsAdmin = false,
                    IsDelete = false
                };
                db.Entry(account).State = EntityState.Added;

                if (string.IsNullOrEmpty(accountDTO.Avatar))
                {
                    account.Avatar = "Default.png";
                }
                else
                {
                    account.Avatar = accountDTO.Avatar;
                }

                db.Accounts.Add(account);

                await db.SaveChangesAsync();

                return account.Id;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<string> SendOTP(AccountDTO accountDTO)
        {
            Task<string> task = new Task<string>(new Func<string>(() =>
            {
                try
                {
                    string OTP = new Random().Next(1000, 10000).ToString();
                    var body = "";

                    body += "<hr/>";
                    body += "Xin chào <b>" + accountDTO.FullName + "</b>,<br/><br/>";

                    body += "Mã xác thực OTP của bạn là: <b>" + OTP + "</b><br/><br/>";
                    body += "Vui lòng không cung cấp mã OTP cho bất kì ai khác.<br/><br/>";
                    //body += "Cảm ơn đã đăng ký,<br/><br/>";
                    body += "-----------------------------<br/>";
                    body += "<b>Cửa hàng Mobile Store</b><br/>";
                    body += "<b>Phone:</b> (84) 918 475 646<br/>";                
                    body += "<hr/>";

                    var result = Const.SendMail(accountDTO.Email, "Xác thực Email ", body);
                    if (result)
                        return OTP;
                    else
                        return "";

                }
                catch (Exception e)
                {
                    return "";
                    throw e;
                }
            }));

            task.Start();

            return await task;
        }

        public async Task<bool> DeleteAccount(int Id)
        {
            var result = db.Accounts.SingleOrDefault(b => b.Id == Id);

            try
            {
                //db.Accounts.Remove(result);
                result.IsDelete = true;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> RestoreAccount(int Id)
        {
            var result = db.Accounts.SingleOrDefault(b => b.Id == Id);

            try
            {
                //db.Accounts.Remove(result);
                result.IsDelete = false;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> UpdateAccount(AccountDTO accountDTO)
        {
            var result = db.Accounts.SingleOrDefault(c => c.Email == accountDTO.Email);
            if (accountDTO.Email == null)
            {
                result = db.Accounts.SingleOrDefault(c => c.UserName == accountDTO.UserName);
                if (accountDTO.UserName == null)
                {
                    result = db.Accounts.SingleOrDefault(c => c.Id == accountDTO.Id);
                }
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(accountDTO.FullName))
                    result.FullName = accountDTO.FullName;
                if (!string.IsNullOrWhiteSpace(accountDTO.Email))
                    result.Email = accountDTO.Email;
                if (!string.IsNullOrWhiteSpace(accountDTO.UserName))
                    result.UserName = accountDTO.UserName;
                if (!string.IsNullOrWhiteSpace(accountDTO.PhoneNumber))
                    result.PhoneNumber = accountDTO.PhoneNumber;
                if (!string.IsNullOrWhiteSpace(accountDTO.Avatar))
                    result.Avatar = accountDTO.Avatar;
                if (!string.IsNullOrWhiteSpace(accountDTO.Birthday))
                    result.Birthday = accountDTO.Birthday;
                if (accountDTO.Gender != null)
                    result.Gender = accountDTO.Gender;


                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> ResetPassword(AccountDTO accountDTO)
        {
            var result = db.Accounts.SingleOrDefault(c => c.Email == accountDTO.Email);
            
            try
            {
                if (!string.IsNullOrWhiteSpace(accountDTO.PassWord))
                    result.PassWord = Const.CreateMD5(accountDTO.PassWord);

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> ChangeAccountRole(AccountDTO accountDTO)
        {
            
            var result = db.Accounts.SingleOrDefault(c => c.Email == accountDTO.Email);
            if (accountDTO.Email == null)
            {
                result = db.Accounts.SingleOrDefault(c => c.UserName == accountDTO.UserName);
                if (accountDTO.UserName == null)
                {
                    result = db.Accounts.SingleOrDefault(c => c.Id == accountDTO.Id);
                }
            }
            try
            {
                
                if (!string.IsNullOrWhiteSpace(accountDTO.Role))
                    result.Role = accountDTO.Role;

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        /* public async Task<bool> ChangeCustomerRole(int ID)
         {
             var result = db.Customers.SingleOrDefault(c => c.ID == ID);
             try
             {
                 result.IsAdmin = !result.IsAdmin;
                 await db.SaveChangesAsync();
                 return true;
             }
             catch (Exception e)
             {
                 return false;
                 throw e;
             }
         }*/

        /*public async Task<int> RecycleAppPool()
        {
            try
            {
                //var yourAppPool = new ServerManager().ApplicationPools["WebAPI"];
                //if (yourAppPool != null)
                //{
                //    yourAppPool.Recycle();
                //    return 1;
                //}
                System.Web.HttpRuntime.UnloadAppDomain();
                return 1;
            }
            catch (Exception e)
            {
                return -2;
                throw e;
            }
        }*/
    }
}