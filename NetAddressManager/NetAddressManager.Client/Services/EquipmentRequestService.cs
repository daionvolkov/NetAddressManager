using NetAddressManager.Client.Models;
using NetAddressManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services
{
    public class EquipmentRequestService : CommonRequestService
    {
        private string _equipmtntControllerUrl = HOST + "equipment";

        public List<EquipmentManufacturerModel> GetAllEquipments(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _equipmtntControllerUrl, token);
            List<EquipmentManufacturerModel> equipments = JsonConvert.DeserializeObject<List<EquipmentManufacturerModel>>(response) ?? new List<EquipmentManufacturerModel>();
            return equipments;
        }

        public EquipmentManufacturerModel GetEquipmentById(AuthToken token, int equipmentId)
        {
            var response = GetDataByUrl(HttpMethod.Get, _equipmtntControllerUrl + $"/{equipmentId}", token);
            EquipmentManufacturerModel equipment = JsonConvert.DeserializeObject<EquipmentManufacturerModel>(response) ?? new EquipmentManufacturerModel();
            return equipment;
        }

        public List<EquipmentManufacturerModel> GetEquipmentsByName(AuthToken token, string name)
        {
            string response = GetDataByUrl(HttpMethod.Get, _equipmtntControllerUrl +$"/{name}/equipment", token);
            List<EquipmentManufacturerModel> equipments = JsonConvert.DeserializeObject<List<EquipmentManufacturerModel>>(response) ?? new List<EquipmentManufacturerModel>();
            return equipments;
        }

        public HttpStatusCode CreateEquipment(AuthToken token, EquipmentManufacturerModel equipment)
        {
            string equipmentJson = JsonConvert.SerializeObject(equipment);
            var result = SendDataByUrl(HttpMethod.Post, _equipmtntControllerUrl, token, equipmentJson);
            return result;
        }

        public HttpStatusCode UpdateEquipment(AuthToken token, EquipmentManufacturerModel equipment)
        {
            string equipmentJson = JsonConvert.SerializeObject(equipment);
            var result = SendDataByUrl(HttpMethod.Patch, _equipmtntControllerUrl + $"/{equipment.Id}", token, equipmentJson);
            return result;
        }

        public HttpStatusCode DeleteEquipment(AuthToken token, int equipmentId)
        {
            var result = DeleteDataByUrl(_equipmtntControllerUrl + $"/{equipmentId}", token);
            return result;
        }

    }
}
