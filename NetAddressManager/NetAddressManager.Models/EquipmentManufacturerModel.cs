﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Models
{
    public class EquipmentManufacturerModel
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public EquipmentManufacturerModel()
        {
        }

        public EquipmentManufacturerModel(string manufacturer, string model)
        {
            Manufacturer = manufacturer;
            Model = model;
        }
    }
}
