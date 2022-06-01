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
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities3 db = new PhoneStoreEntities3();

        public async Task<List<BillDTO>> GetAllBill()
        {
            var resulList = (await db.Bills
                        .ToListAsync())
                        .Select(b => new BillDTO(b))
                        .OrderByDescending(s => s.Id)
                        .ToList();
            return resulList;
        }

        public async Task<List<BillDTO>> GetBillAccountId(int Id)
        {
            var Bill = (await db.Bills
                .ToListAsync())
                .Select(bill => new BillDTO(bill))
                .OrderByDescending(s => s.Id)
                .ToList();
            Bill = Bill.FindAll(f => f.AccountId == Id);
            return Bill;
        }

        public async Task<BillDTO> GetBillById(int Id)
        {
            try
            {
                var bill = await db.Bills.SingleOrDefaultAsync(Bills => Bills.Id == Id);
                if (bill != null)
                {
                    return new BillDTO(bill);
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<List<BillDTO>> GetBillByDay(String date)
        {
            try
            {
                var billtList = (await db.Bills
                        .ToListAsync())
                        .Select(product => new BillDTO(product))
                        .OrderBy(Bills => Bills.CreateAt.Equals(date))
                        .ToList();
                billtList = billtList.FindAll(b => b.Status == 1 || b.Status == 2);
                return billtList;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<List<BillDTO>> GetBillByMonth(String date)
        {
            try
            {

                var billtList = (await db.Bills
                        .ToListAsync())
                        .Select(product => new BillDTO(product))
                        .OrderBy(Bills => Bills.CreateAt.Substring(Bills.CreateAt.Length - 7).Equals(date))
                        .ToList();
                billtList = billtList.FindAll(b => b.Status == 1 || b.Status == 2);
                return billtList;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<List<BillDTO>> GetBillByYear(int year)
        {
            String date = Convert.ToString(year);
            try
            {

                var billtList = (await db.Bills
                       .ToListAsync())
                       .Select(product => new BillDTO(product))
                       .OrderBy(Bills => Bills.CreateAt.Substring(Bills.CreateAt.Length - 4).Equals(date))
                       .ToList();
                billtList = billtList.FindAll(b => b.Status == 1 || b.Status == 2);
                return billtList;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<int> AddBill(BillDTO billDTO)
        {
            Bill bill = new Bill()
            {
                ProductId = billDTO.ProductId,
                AccountId = billDTO.AccountId,
                ShipmentId = billDTO.ShipmentId,
                CreateAt = billDTO.CreateAt,
                Quantity = billDTO.Quantity,
                Status = billDTO.Status,
                TotalPrice = billDTO.TotalPrice,
                TypeProduct = billDTO.TypeProduct,
                ShipCost = billDTO.ShipCost,
                Reason = billDTO.Reason,
                isDelete = false
            };

            try
            {
                //var Bill = await db.Bills.SingleOrDefaultAsync(f => f.AccountId == bill.AccountId && f.ShipmentId == bill.ShipmentId);

                //if (Bill == null)
                //{
                    db.Bills.Add(bill);
                    await db.SaveChangesAsync();
                    return bill.Id;
               // }
                //else
                //{
                   // return -1;
                //}
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> UpdateBill(BillDTO billDTO)
        {
            var result = db.Bills.SingleOrDefault(p => p.Id == billDTO.Id);

            try
            {
               
                if ((billDTO.Status != null))
                    result.Status = billDTO.Status;
                if (!string.IsNullOrWhiteSpace(billDTO.CreateAt))
                    result.CreateAt = billDTO.CreateAt;
                if (!string.IsNullOrWhiteSpace(billDTO.TypeProduct))
                    result.TypeProduct = billDTO.TypeProduct;
                if (!string.IsNullOrWhiteSpace(billDTO.Reason))
                    result.Reason = billDTO.Reason;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> ChangeBillStatus(int id, int status)
        {
            var result = db.Bills.SingleOrDefault(c => c.Id == id);
            try
            {

                result.Status = status;

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteBillById(int Id)
        {
            var result = db.Bills.SingleOrDefault(f => f.Id == Id);

            try
            {
                db.Bills.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
    }
}