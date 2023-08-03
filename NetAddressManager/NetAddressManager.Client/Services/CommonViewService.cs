using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetAddressManager.Client.Services
{
    public class CommonViewService
    {
        public Window CurrentOpenedWindow { get; private set; }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowActionRelust(System.Net.HttpStatusCode code, string message)
        {

            if (code == System.Net.HttpStatusCode.OK)
            {
                ShowMessage(code.ToString() + $"\n{message}");
            }
            else
            {
                ShowMessage(code.ToString() + $"\nError");
            }

        }

        public void OpenWindow(Window wnd, BindableBase viewModel)
        {
            CurrentOpenedWindow = wnd;
            wnd.DataContext = viewModel;
            wnd.Show();
        }




    }
}
