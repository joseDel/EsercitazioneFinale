using Allocation.Core.BusinessLayer;
using Allocation.Core.Entities;
using Allocation.Core.Mock.Repositories;
using Allocation.WPF.Messaging.Card;
using Allocation.WPF.Messaging.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Allocation.WPF.ViewModels
{
    public class GiftCardCreateViewModel : ViewModelBase
    {
        public ICommand CreateCommand { get; set; }

        private string _Mittente;
        public string Mittente
        {
            get { return _Mittente; }   
            set { _Mittente = value; RaisePropertyChanged(); }
        }

        private string _Destinatario;
        public string Destinatario
        {
            get { return _Destinatario; }
            set { _Destinatario = value; RaisePropertyChanged(); }
        }

        private string _Messaggio;
        public string Messaggio
        {
            get { return _Messaggio; }
            set { _Messaggio = value; RaisePropertyChanged(); }
        }

        private double _Importo;
        public double Importo
        {
            get { return _Importo; }
            set { _Importo = value; RaisePropertyChanged(); }
        }

        private DateTime _DataScadenza;
        public DateTime DataScadenza
        {
            get { return _DataScadenza; }
            set { _DataScadenza = value; RaisePropertyChanged(); }
        }

        public GiftCardCreateViewModel()
        {
            CreateCommand = new RelayCommand(() => ExecuteCreate(), 
                () => CanExecuteCreate());

            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (CreateCommand as RelayCommand).RaiseCanExecuteChanged();
                };
            }

        }

        private bool CanExecuteCreate()
        {
            return !string.IsNullOrEmpty(Mittente) &&
                !string.IsNullOrEmpty(Destinatario) &&
                !string.IsNullOrEmpty(Messaggio) &&
                !string.IsNullOrEmpty(Importo.ToString()) &&
                !string.IsNullOrEmpty(DataScadenza.ToString());
        }

        private void ExecuteCreate()
        {
            var entity = new GiftCard
            {
                Mittente = Mittente,
                Destinatario = Destinatario,
                Messaggio = Messaggio,  
                Importo = Importo,
                DataScadenza= DataScadenza,
            };

            //business layer
            var layer = new MainBusinessLayer(new GiftCardRepositoryMock());
            //Richiamo il create
            var response = layer.CreateGiftcard(entity);

            if (!response.Success)
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Something went wrong",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Warning
                });
                return;
            }
            else
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Gift Card creata",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Information
                });
            }
            Messenger.Default.Send(new CloseCreateGiftCardMessage());
        }
    }
}
