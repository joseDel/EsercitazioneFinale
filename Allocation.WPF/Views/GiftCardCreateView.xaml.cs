using Allocation.WPF.Messaging.Card;
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
    /// Interaction logic for GiftCardCreateView.xaml
    /// </summary>
    public partial class GiftCardCreateView : Window
    {
        public GiftCardCreateView()
        {
            InitializeComponent();

            //registrazione al messaggio di chiusura
            Messenger.Default.Register<CloseCreateGiftCardMessage>(this, _ => Close());

            Closing += (s, e) =>
            {
                //deregistrazione
                Messenger.Default.Unregister(this);
                Messenger.Default.Unregister(DataContext);
            };
        }
    }
}
