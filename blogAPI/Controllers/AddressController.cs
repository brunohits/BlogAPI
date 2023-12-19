using blogAPI.Data.Entities;
using blogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public List<AddressElementModel> SearchAddress(long parentObjectId, string query = "")
        {
            return _addressService.SearchAddress(parentObjectId, query);
        }

        [HttpGet]
        public List<AddressElementModel> GetChain(Guid objectGuid)
        {        
            return _addressService.GetChain(objectGuid);
        }
    }
}