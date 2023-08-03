using NetAddressManager.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Windows;

namespace NetAddressManager.Client.ViewModels
{
    public class UpdateEquipmentWindowViewModel :BindableBase
    {
        private readonly UpdateSwitchWindowViewModel updateSwitchWindowViewModel;

        // ... Other properties and commands ...

        public UpdateEquipmentWindowViewModel(UpdateSwitchWindowViewModel updateSwitchWindowViewModel)
        {
            this.updateSwitchWindowViewModel = updateSwitchWindowViewModel;
        }

        private void SearchEquipment()
        {

            updateSwitchWindowViewModel.SearchEquipment();
        }
    }
}
