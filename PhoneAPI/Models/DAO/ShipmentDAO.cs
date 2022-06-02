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
    public class ShipmentDAO
    {
        private static ShipmentDAO instance;

        public static ShipmentDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShipmentDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities3 db = new PhoneStoreEntities3();

        public async Task<List<ShipmentDTO>> GetAllShipment()
        {
            var resulList = (await db.Shipments
                        .ToListAsync())
                        .Select(b => new ShipmentDTO(b))
                        .ToList();
            resulList = resulList.FindAll(b => b.IsDelete == false);
            return resulList;
        }

        public async Task<List<ShipmentDTO>> GetShipmentByAccountId(int Id)
        {
            var ShipmentList = (await db.Shipments
                .ToListAsync())
                .Select(shipment => new ShipmentDTO(shipment))
                .OrderByDescending(s => s.Id)
                .ToList();
            ShipmentList = ShipmentList.FindAll(f => f.AccountId == Id && f.IsDelete == false);

            return ShipmentList;
        }

        public async Task<ShipmentDTO> GetShipmentById(int Id)
        {
            try
            {
                var ship = await db.Shipments.SingleOrDefaultAsync(shipment => shipment.Id == Id);
                if (ship != null)
                {
                    return new ShipmentDTO(ship);
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }
        public async Task<int> AddShipment(ShipmentDTO shipmentDTO)
        {
            Shipment shipment = new Shipment()
            {
                AccountId = shipmentDTO.AccountId,
                FullName = shipmentDTO.FullName,
                PhoneNumber = shipmentDTO.PhoneNumber,
                Address = shipmentDTO.Address,
                Street = shipmentDTO.Street,
                TypeAddress = shipmentDTO.TypeAddress,
                IsDefault = shipmentDTO.IsDefault,
                IsDelete = false
            };

            /*Account account = db.Accounts.SingleOrDefault(c => c.Id == shipment.AccountId);
            shipment.Account = account;



            db.Entry(shipment).State = EntityState.Added;*/

            try
            {
                db.Shipments.Add(shipment);
                await db.SaveChangesAsync();
                return shipment.Id;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> DeleteShipmentById(int Id)
        {
            var result = db.Shipments.SingleOrDefault(f => f.Id == Id);

            try
            {
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

        public async Task<bool> UpdateShipment(ShipmentDTO shipmentDTO)
        {
            var result = db.Shipments.SingleOrDefault(p => p.Id == shipmentDTO.Id);
            try
            {
                result.FullName = shipmentDTO.FullName;
                result.PhoneNumber = shipmentDTO.PhoneNumber;
                result.Address = shipmentDTO.Address;
                result.Street = shipmentDTO.Street;
                result.TypeAddress = shipmentDTO.TypeAddress;
                result.IsDefault = shipmentDTO.IsDefault;

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