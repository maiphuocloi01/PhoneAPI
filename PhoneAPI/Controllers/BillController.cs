using PhoneAPI.Models;
using PhoneAPI.Models.DAO;
using PhoneAPI.Models.DTO;
using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhoneAPI.Controllers
{
    public class BillController : ApiController
    {
        PhoneStoreEntities3 db = new PhoneStoreEntities3();


        [Route("Api/BillController/GetAllBill")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBill()
        {
            return Ok(await BillDAO.Instance.GetAllBill());
        }

        [Route("Api/BillController/GetBillAccountId/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillAccountId(int Id)
        {
            return Ok(await BillDAO.Instance.GetBillAccountId(Id));
        }

        [Route("Api/BillController/GetBillById/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillById(int Id)
        {
            return Ok(await BillDAO.Instance.GetBillById(Id));
        }

        [Route("Api/BillController/GetBillByDay/{day}/{month}/{year}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetBillByDay(int day, int month, int year)
        {
            String date = "";
            if (month < 10)
            {
                if (day < 10)
                {
                    date = "0" + day + "/0" + month + "/" + year;
                }
                else
                {
                    date = day + "/0" + month + "/" + year;
                }
            }
            else
            {
                if (day < 10)
                {
                    date = "0" + day + "/" + month + "/" + year;
                }
                else
                {
                    date = day + "/" + month + "/" + year;
                }
            }
            var checkBill = db.Bills.Where(s => s.CreateAt.Equals(date) && (s.Status == 1 || s.Status == 2));
            if (checkBill == null)
            {
                return NotFound();
            }
            var bills = from s in db.Bills.Where(s => s.CreateAt.Equals(date) && (s.Status == 1 || s.Status == 2))
                                    join p in db.Products on s.ProductId equals p.Id

                                    select new
                                    {
                                        Id = s.Id,
                                        TotalPrice = s.TotalPrice,
                                        Date = s.CreateAt,
                                        Quantity = s.Quantity
                                    };
            //return Ok(await ShoppingCartItemDAO.Instance.GetAllShoppingCartItems(ID));
            return Ok(bills);
        }

        [Route("Api/BillController/GetBillByMonth/{month}/{year}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetBillByMonth(int month, int year)
        {
            String date = "";
            if (month < 10)
            {
                date = "0" + month + "/" + year;
            }
            else
            {
                date = month + "/" + year;
            }
            var checkBill = db.Bills.Where(s => s.CreateAt.Substring(3).Equals(date) && (s.Status == 1 || s.Status == 2));
            if (checkBill == null)
            {
                return NotFound();
            }
            var bills = from s in db.Bills.Where(s => s.CreateAt.Substring(3).Equals(date) && (s.Status == 1 || s.Status == 2))
                        join p in db.Products on s.ProductId equals p.Id

                        select new
                        {
                            Id = s.Id,
                            TotalPrice = s.TotalPrice,
                            Date = s.CreateAt,
                            Quantity = s.Quantity
                        };
            //return Ok(await ShoppingCartItemDAO.Instance.GetAllShoppingCartItems(ID));
            return Ok(bills);
        }

        [Route("Api/BillController/GetBillByYear/{year}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetBillByYear(int year)
        {
            String date = "" + year;
            
            var checkBill = db.Bills.Where(s => s.CreateAt.Substring(6).Equals(date) && (s.Status == 1 || s.Status == 2));
            if (checkBill == null)
            {
                return NotFound();
            }
            var bills = from s in db.Bills.Where(s => s.CreateAt.Substring(6).Equals(date) && (s.Status == 1 || s.Status == 2))
                        join p in db.Products on s.ProductId equals p.Id

                        select new
                        {
                            Id = s.Id,
                            TotalPrice = s.TotalPrice,
                            Date = s.CreateAt,
                            Quantity = s.Quantity
                        };

          
            //return Ok(await ShoppingCartItemDAO.Instance.GetAllShoppingCartItems(ID));
            return Ok(bills);
        }

        [Route("Api/BillController/GetStatisticByYear/{year}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetStatisticByYear(int year)
        {
            String date = "" + year;

            var totalPriceBillJan = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("01/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillFeb = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("02/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillMar = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("03/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillApr = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("04/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();
            var totalPriceBillMay = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("05/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillJun = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("06/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillJul = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("07/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillAug = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("08/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillSep = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("09/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillOct = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("10/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillNov = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("11/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            var totalPriceBillDec = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("12/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.TotalPrice).Sum();

            /*var bills = from s in db.Bills.Where(s => s.CreateAt.Substring(6).Equals(date) && s.Status == 1 || s.Status == 2)
                        join p in db.Products on s.ProductId equals p.Id

                        select new
                        {
                            Id = s.Id,
                            TotalPrice = s.TotalPrice,
                            Date = s.CreateAt
                        };*/


            //return Ok(await ShoppingCartItemDAO.Instance.GetAllShoppingCartItems(ID));
            return Ok(new 
            { 
                Jan = totalPriceBillJan == null ? 0 : totalPriceBillJan,
                Feb = totalPriceBillFeb == null ? 0 : totalPriceBillFeb,
                Mar = totalPriceBillMar == null ? 0 : totalPriceBillMar,
                Apr = totalPriceBillApr == null ? 0 : totalPriceBillApr,
                May = totalPriceBillMay == null ? 0 : totalPriceBillMay,
                Jun = totalPriceBillJun == null ? 0 : totalPriceBillJun,
                Jul = totalPriceBillJul == null ? 0 : totalPriceBillJul,
                Aug = totalPriceBillAug == null ? 0 : totalPriceBillAug,
                Sep = totalPriceBillSep == null ? 0 : totalPriceBillSep,
                Oct = totalPriceBillOct == null ? 0 : totalPriceBillOct,
                Nov = totalPriceBillNov == null ? 0 : totalPriceBillNov,
                Dec = totalPriceBillDec == null ? 0 : totalPriceBillDec

            });
        }

        [Route("Api/BillController/CountProductByYear/{year}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult CountProductByYear(int year)
        {
            String date = "" + year;

            var totalPriceBillJan = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("01/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillFeb = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("02/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillMar = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("03/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillApr = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("04/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();
            var totalPriceBillMay = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("05/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillJun = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("06/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillJul = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("07/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillAug = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("08/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillSep = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("09/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillOct = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("10/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillNov = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("11/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            var totalPriceBillDec = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("12/" + date) && (b.Status == 1 || b.Status == 2)
                                     select b.Quantity).Sum();

            /*var bills = from s in db.Bills.Where(s => s.CreateAt.Substring(6).Equals(date) && s.Status == 1 || s.Status == 2)
                        join p in db.Products on s.ProductId equals p.Id

                        select new
                        {
                            Id = s.Id,
                            TotalPrice = s.TotalPrice,
                            Date = s.CreateAt
                        };*/


            //return Ok(await ShoppingCartItemDAO.Instance.GetAllShoppingCartItems(ID));
            return Ok(new
            {
                Jan = totalPriceBillJan == null ? 0 : totalPriceBillJan,
                Feb = totalPriceBillFeb == null ? 0 : totalPriceBillFeb,
                Mar = totalPriceBillMar == null ? 0 : totalPriceBillMar,
                Apr = totalPriceBillApr == null ? 0 : totalPriceBillApr,
                May = totalPriceBillMay == null ? 0 : totalPriceBillMay,
                Jun = totalPriceBillJun == null ? 0 : totalPriceBillJun,
                Jul = totalPriceBillJul == null ? 0 : totalPriceBillJul,
                Aug = totalPriceBillAug == null ? 0 : totalPriceBillAug,
                Sep = totalPriceBillSep == null ? 0 : totalPriceBillSep,
                Oct = totalPriceBillOct == null ? 0 : totalPriceBillOct,
                Nov = totalPriceBillNov == null ? 0 : totalPriceBillNov,
                Dec = totalPriceBillDec == null ? 0 : totalPriceBillDec

            });
        }

        [Route("Api/BillController/GetStatusAllBill")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetStatusAllBill()
        {
            //String date = "" + year;

            var totalBillDelivering = (from b in db.Bills
                                     where b.Status == 0
                                     select b.Quantity).Count();

            var totalBillDelivered = (from b in db.Bills
                                       where b.Status == 1
                                       select b.Quantity).Count();

            var totalBillCompleted = (from b in db.Bills
                                      where b.Status == 2
                                      select b.Quantity).Count();

            var totalBillCancel = (from b in db.Bills
                                      where b.Status == 3
                                      select b.Quantity).Count();

            /*var totalPriceBillFeb = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("02/" + date) && b.Status == 1 || b.Status == 2
                                     select b.Quantity).Sum();*/


            return Ok(new
            {
                Delivering = totalBillDelivering == null ? 0 : totalBillDelivering,
                Delivered = totalBillDelivered == null ? 0 : totalBillDelivered,
                Completed = totalBillCompleted == null ? 0 : totalBillCompleted,
                Cancel = totalBillCancel == null ? 0 : totalBillCancel

            });
        }

        [Route("Api/BillController/GetStatusAllBillByYear/{year}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetStatusAllBillByYear(int year)
        {
            String date = "" + year;

            var totalBillDelivering = (from b in db.Bills
                                       where b.CreateAt.Substring(6).Equals(date) && b.Status == 0
                                       select b.Quantity).Count();

            var totalBillDelivered = (from b in db.Bills
                                      where b.CreateAt.Substring(6).Equals(date) && b.Status == 1
                                      select b.Quantity).Count();

            var totalBillCompleted = (from b in db.Bills
                                      where b.CreateAt.Substring(6).Equals(date) && b.Status == 2
                                      select b.Quantity).Count();

            var totalBillCancel = (from b in db.Bills
                                   where b.CreateAt.Substring(6).Equals(date) && b.Status == 3
                                   select b.Quantity).Count();

            /*var totalPriceBillFeb = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("02/" + date) && b.Status == 1 || b.Status == 2
                                     select b.Quantity).Sum();*/


            return Ok(new
            {
                Delivering = totalBillDelivering == null ? 0 : totalBillDelivering,
                Delivered = totalBillDelivered == null ? 0 : totalBillDelivered,
                Completed = totalBillCompleted == null ? 0 : totalBillCompleted,
                Cancel = totalBillCancel == null ? 0 : totalBillCancel

            });
        }

        [Route("Api/BillController/GetCountInfo")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetCountInfo()
        {
            //String date = "" + year;

            var countUser = (from b in db.Accounts
                                       where b.IsDelete == false
                                       select b.Id).Count() - 1;

            var countProduct = (from b in db.Products
                                      where b.IsDelete == false
                                      select b.Id).Count();

            var countBill = (from b in db.Bills
                                      where b.isDelete == false
                                      select b.Id).Count();

            var countComment = (from b in db.Comments
                                   where b.IsDelete == false
                                   select b.Id).Count();

            /*var totalPriceBillFeb = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("02/" + date) && b.Status == 1 || b.Status == 2
                                     select b.Quantity).Sum();*/


            return Ok(new
            {
                User = countUser == null ? 0 : countUser,
                Product = countProduct == null ? 0 : countProduct,
                Bill = countBill == null ? 0 : countBill,
                Comment = countComment == null ? 0 : countComment

            });
        }

        [Route("Api/BillController/GetStatisticCategoryByYear/{year}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetStatisticCategoryByYear(int year)
        {
            String date = "" + year;

            var totalCostPhone = (from b in db.Bills
                                  join p in db.Products on b.ProductId equals p.Id
                                  where b.CreateAt.Substring(6).Equals(date) && p.Category == false && (b.Status == 1 || b.Status == 2)
                                  select b.TotalPrice).Sum();

            var countBillPhone = (from b in db.Bills
                                  join p in db.Products on b.ProductId equals p.Id
                                  where b.CreateAt.Substring(6).Equals(date) && p.Category == false && (b.Status == 1 || b.Status == 2)
                                  select b.Id).Count();

            var totalCostTablet = (from b in db.Bills
                                  join p in db.Products on b.ProductId equals p.Id
                                  where b.CreateAt.Substring(6).Equals(date) && p.Category == true && (b.Status == 1 || b.Status == 2)
                                   select b.TotalPrice).Sum();

            var countBillTablet = (from b in db.Bills
                                  join p in db.Products on b.ProductId equals p.Id
                                  where b.CreateAt.Substring(6).Equals(date) && p.Category == true && (b.Status == 1 || b.Status == 2)
                                   select b.Id).Count();

            /*var totalPriceBillFeb = (from b in db.Bills
                                     where b.CreateAt.Substring(3).Equals("02/" + date) && b.Status == 1 || b.Status == 2
                                     select b.Quantity).Sum();*/


            return Ok(new
            {
                TotalCostPhone = totalCostPhone == null ? 0 : totalCostPhone,
                TotalPhoneSale = countBillPhone == null ? 0 : countBillPhone,
                TotalCostTablet = totalCostTablet == null ? 0 : totalCostTablet,
                TotalTabletSale = countBillTablet == null ? 0 : countBillTablet

            });
        }

        /*[Route("Api/BillController/GetBillByDay/{date}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillByDay1(String date)
        {
            return Ok(await BillDAO.Instance.GetBillByDay(date));
        }*/

        /*[Route("Api/BillController/GetBillByMonth/{date}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillByMonth(String date)
        {
            return Ok(await BillDAO.Instance.GetBillByMonth(date));
        }

        [Route("Api/BillController/GetBillByYear/{year}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillByYear(int year)
        {
            return Ok(await BillDAO.Instance.GetBillByYear(year));
        }*/


        [Route("Api/BillController/DeleteBillById/{Id}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteBillById(int Id)
        {
            return Ok(await BillDAO.Instance.DeleteBillById(Id));
        }

        [Route("Api/BillController/AddBill")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddBill(BillDTO billDTO)
        {
            return Ok(await BillDAO.Instance.AddBill(billDTO));
        }

        [Route("Api/BillController/UpdateBill")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateBill(BillDTO billDTO)
        {
            return Ok(await BillDAO.Instance.UpdateBill(billDTO));
        }

        [Route("Api/BillController/ChangeBillStatus/{id}/{status}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> ChangeBillStatus(int id, int status)
        {
            return Ok(await BillDAO.Instance.ChangeBillStatus(id, status));
        }
    }
}
