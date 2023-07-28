using NetAddressManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Models
{
    public class SwitchModel<T> where T : SwitchDataModel
    {
        public T? Model { get; set; }

        public SwitchModel(T model)
        {
            Model = model;
        }
    }
}
