using blogAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Services.Interfaces
{
    public interface IAddressService
    {
        List<AddressElementModel> SearchAddress(long parentObjectId, string query = "");
        List<AddressElementModel> GetChain(Guid objectGuid);

    }
}
