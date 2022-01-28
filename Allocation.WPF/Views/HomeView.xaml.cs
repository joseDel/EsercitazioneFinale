using Allocation.WPF.Messaging.Card;
using Allocation.WPF.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Allocation.WPF.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Window
    {
        public HomeView()
        {
            InitializeComponent();

            Messenger.Default.Register<ShowCreateGiftCardMessage>(this, OnShowCreateGiftCardExecuted);
            //TODO
            //Messenger.Default.Register<ShowUpdateGiftCardMessage>(this, OnShowUpdateEmployeeMessageReceived);
        }

        private void OnShowCreateGiftCardExecuted(ShowCreateGiftCardMessage obj)
        {
            GiftCardCreateView view = new GiftCardCreateView();
            GiftCardCreateViewModel vm = new GiftCardCreateViewModel();
            view.DataContext = vm;
            view.ShowDialog();
        }
    }
}
