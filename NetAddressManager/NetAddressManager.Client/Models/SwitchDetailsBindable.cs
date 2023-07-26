using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Models
{
    public class SwitchDetailsBindable<T> : BindableBase
    {
        private T _switchData;
        public T SwitchData
        {
            get => _switchData;
            set => SetProperty(ref _switchData, value);
        }

        private string? _ipGateway;
        public string? IPGateway
        {
            get => _ipGateway;
            set => SetProperty(ref _ipGateway, value);
        }

        private SwitchType _switchType;
        public SwitchType SwitchType
        {
            get => _switchType;
            set => SetProperty(ref _switchType, value);
        }

        private string? _postalAddress;
        public string? PostalAddress
        {
            get => _postalAddress;
            set => SetProperty(ref _postalAddress, value);
        }

        private string? _equipment;
        public string? Equipment
        {
            get => _equipment;
            set => SetProperty(ref _equipment, value);
        }

        private List<SwitchPortModel>? _port;
        public List<SwitchPortModel>? Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

    }
}
