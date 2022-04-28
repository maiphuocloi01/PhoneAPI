using PhoneAPI.Models.DTO;
using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PhoneAPI.Models.DAO
{
    public class BankDAO
    {
        private static BankDAO instance;

        public static BankDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BankDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities2 db = new PhoneStoreEntities2();

        public async Task<List<BankDTO>> GetAllBank()
        {
            var resulList = (await db.Banks
                        .ToListAsync())
                        .Select(b => new BankDTO(b))
                        .ToList();
            //resulList = resulList.FindAll(b => b.IsDelete == false);
            return resulList;
        }

        public async Task<int> CreateBank(BankDTO bankDTO)
        {
            Bank bank = new Bank()
            {
                FullName = bankDTO.FullName,
                CardNumber = bankDTO.CardNumber,
                ExpiredDate = bankDTO.ExpiredDate,
                AccountId = bankDTO.AccountId,
                CVV = bankDTO.CVV,
                Brand = bankDTO.Brand
            };

            Account account = db.Accounts.SingleOrDefault(c => c.Id == bank.AccountId);
            bank.Account = account;

            db.Entry(bank).State = EntityState.Added;

            try
            {
                db.Banks.Add(bank);
                await db.SaveChangesAsync();
                return bank.Id;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> DeleteBank(int Id)
        {
            var result = db.Banks.SingleOrDefault(b => b.Id == Id);
            try
            {
                db.Banks.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
        public async Task<List<BankDTO>> GetAllBankByAccountId(int Id)
        {
            var resultList = (await db.Banks
                .ToListAsync())
                .Select(b => new BankDTO(b))
                .ToList();
            resultList = resultList.FindAll(b => b.AccountId == Id);
            return resultList;
        }
    }
}