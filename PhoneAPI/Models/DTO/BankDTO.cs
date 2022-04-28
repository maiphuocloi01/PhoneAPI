using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class BankDTO
    {      
            public int? Id { get; set; }

            public int? AccountId { get; set; }

            public string FullName { get; set; }

            public string CardNumber { get; set; }

            public string ExpiredDate { get; set; }

            public int? CVV { get; set; }

            public string Brand { get; set; }

            //public bool? IsDelete { get; set; }         

            public BankDTO()
            {

            }

            public BankDTO(Bank bank)
            {
                Id = bank.Id;
                AccountId = bank.AccountId;
                FullName = bank.FullName;
                CardNumber = bank.CardNumber;
                ExpiredDate = bank.ExpiredDate;
                CVV = bank.CVV;
                Brand = bank.Brand;
            }
    }
}