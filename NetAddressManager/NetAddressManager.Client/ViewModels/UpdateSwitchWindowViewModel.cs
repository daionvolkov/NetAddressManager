using GalaSoft.MvvmLight.Command;
using NetAddressManager.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NetAddressManager.Client.ViewModels
{
    public class UpdateSwitchWindowViewModel
    {

        #region COMMANDS
        

        public UpdateSwitchWindowViewModel()
        {
            
            
        }
        #endregion



        #region PROPERTIES
       

        #endregion

        #region METHODS
        public void OpenUpdateGateway()
        {
            MessageBox.Show(nameof(OpenUpdateGateway));
        }

        public void OpenUpdateAddress()
        {
            MessageBox.Show(nameof(OpenUpdateAddress));
        }

        public void OpenUpdateEquipment()
        {
            MessageBox.Show(nameof(OpenUpdateEquipment));
        }
        #endregion

    }
}
