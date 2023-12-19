using AutoMapper;
using blogAPI.Data;
using blogAPI.Data.Entities;
using blogAPI.Services.Interfaces;

namespace blogAPI.Services
{
    public class AddressService : IAddressService
    {
        private readonly AddressDbContext _context;
        public AddressService(AddressDbContext context)
        {
            _context = context;
        }
        public List<AddressElementModel> GetChain(Guid objectGuid)
        {
            List<AddressElementModel> chain = new List<AddressElementModel>();
            AddressElementModel addressElement = new AddressElementModel();

            var address = _context.AsAddrObjs.FirstOrDefault(obj => obj.Objectguid == objectGuid);
            long objId = 0;

            if (address != null)
            {
                addressElement = new AddressElementModel()
                {
                    Id = address.Id,
                    ObjectGuid = address.Objectguid,
                    Text = address.Name,
                    Level = address.Level
                };
                objId = address.Objectid;
            }
            else
            {
                var house = _context.AsHouses.FirstOrDefault(obj => obj.Objectguid == objectGuid);
                if (house != null)
                {
                    addressElement = new AddressElementModel()
                    {
                        Id = house.Id,
                        ObjectGuid = house.Objectguid,
                        Text = house.Housenum,
                        Level = "Building"
                    };
                    objId = house.Objectid;
                }
                else
                {
                    var ex = new Exception();
                        ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                        "Address not found");
                }
            }

            chain.Add(addressElement);

            while (objId != 1281271)
            {
                var parentId = _context.AsAdmHierarchies.First(obj => obj.Objectid == objId).Parentobjid;

                address = _context.AsAddrObjs.First(obj => obj.Objectid == parentId);
                objId = address.Objectid;

                addressElement = new AddressElementModel()
                {
                    Id = address.Id,
                    ObjectGuid = address.Objectguid,
                    Text = address.Name,
                    Level = address.Level
                };

                chain.Insert(0, addressElement);
            }

            return chain;
        }

        public List<AddressElementModel> SearchAddress(long parentObjectId, string query = "")
        {
            var ids = _context.AsAdmHierarchies
                .Where(address => address.Parentobjid == parentObjectId)
                .Select(address => address.Objectid).ToList();

            var address = _context.AsAddrObjs.
                Where(obj => ids.Contains(obj.Objectid) && (obj.Name == query || query == "")).
                Select(obj => new AddressElementModel
                {
                    Id = obj.Objectid,
                    ObjectGuid = obj.Objectguid,
                    Text = obj.Name,
                    Level = obj.Level
                })
                .ToList();

            var house = _context.AsHouses.
                Where(obj => ids.Contains(obj.Objectid) && (obj.Housenum == query || query == "")).
                Select(obj => new AddressElementModel
                {
                    Id = obj.Objectid,
                    ObjectGuid = obj.Objectguid,
                    Text = obj.Housenum,
                    Level = "Building"
                })
                .ToList();

            return address.Concat(house).ToList();
        }
    }
}
