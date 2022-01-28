using Allocation.Core.Mock.Storage;
using Allocation.WPF.Messaging.Misc;
using Allocation.WPF.ViewModels;
using Allocation.WPF.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Allocation.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Messenger.Default.Register<DialogMessage>(this, OnDialogMessageReceived);
            Messenger.Default.Register<ShutDownApplicationMessage>(this, _ => Current.Shutdown());

            //database 
            AllocationMockStorage.Initialize();

            //Home View
            HomeView view = new HomeView();

            //view model associato alla Home
            HomeViewModel vm = new HomeViewModel();

            //Collego il vm alla view
            view.DataContext = vm;

            //Mostro la view
            view.Show();

            base.OnStartup(e);
        }

        private void OnDialogMessageReceived(DialogMessage mex)
        {
            MessageBoxResult result = MessageBox.Show(
                mex.Content,
                mex.Title,
                mex.Buttons, mex.Icon);

            //Qui ho il risultato della selezione del pulsante
            //cliccato dall'utente => avvio la funzione di Callback
            //SOLAMENTE se è stata specificata (il default è che non 
            //è stata specificata, quindi è nulla!!!)
            if (mex.Callback != null)
                mex.Callback(result);
        }
    }
}
