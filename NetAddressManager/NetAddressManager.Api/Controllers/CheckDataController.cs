using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetAddressManager.Api.Models;
using NetAddressManager.Api.Models.Services;
using NetAddressManager.Models;
using System.IO;

namespace NetAddressManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckDataController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly CheckDataService _checkDataService;

        public CheckDataController(ApplicationContext db)
        {
            _db = db;
            _checkDataService = new CheckDataService(db);
        }

        [HttpGet("{address}/address")]
        public ActionResult<PostalAddressModel> IsPostalAddressExists(string address)
        {
            var postalAddress = address.Split(',');
            var result = _checkDataService.GetPostalAddressModel(postalAddress[0].Trim(), postalAddress[1].Trim(), postalAddress[2].Trim());
            
            return result == null ? NotFound() : Ok(result);

        }

        [HttpGet("{equipment}/equipment")]
        public ActionResult<EquipmentManufacturerModel> IsEquipmentExists(string equipment)
        {
            var equipmentManufacturer = equipment.Split(',');
            var result = _checkDataService.GetEquipmentModel(equipmentManufacturer[0].Trim(), equipmentManufacturer[1].Trim());

            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("{ipAddress}/core")]
        public ActionResult<CoreSwitchModel> IsCoreSwitchIpAddressExists(string ipAddress)
        {
            var result = _checkDataService.GetCoreSwitchByIPAddress(ipAddress);

            return result == null ? NotFound() : Ok(result);
        }


        [HttpGet("{ipAddress}/aggregation")]
        public ActionResult<AggregationSwitchModel> IsAggregationSwitchIpAddressExists(string ipAddress)
        {
            var result = _checkDataService.GetAggregationSwitchByIPAddress(ipAddress);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("{ipAddress}/access")]
        public ActionResult<AccessSwitchModel> IsAccessSwitchIpAddressExists(string ipAddress)
        {
            var result = _checkDataService.GetAccessSwitchByIPAddress(ipAddress);

            return result == null ? NotFound() : Ok(result);
        }
    }
}
