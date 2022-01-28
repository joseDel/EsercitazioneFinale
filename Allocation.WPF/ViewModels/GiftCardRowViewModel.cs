using Allocation.Core.BusinessLayer;
using Allocation.Core.Entities;
using Allocation.Core.Mock.Repositories;
using Allocation.WPF.Messaging.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Allocation.WPF.ViewModels
{
    public class GiftCardRowViewModel : ViewModelBase
    {

        private GiftCard item;

        private string _Destinatario;
        public string Destinatario
        {
            get { return _Destinatario; }
            set { _Destinatario = value; RaisePropertyChanged(); }
        }

        private double _Importo;
        public double Importo
        {
            get { return _Importo; }
            set { _Importo = value; RaisePropertyChanged(); }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        

        public GiftCardRowViewModel()
        {
            UpdateCommand = new RelayCommand(() => ExecuteUpdate());
            DeleteCommand = new RelayCommand(() => ExecuteDelete());
        }

        

        public GiftCardRowViewModel(GiftCard item) : this()
        {
            this.item = item;
            Destinatario = $"Destinatario e valore: {item.Destinatario},";
            Importo = item.Importo;
            Messaggio = $"Messaggio per il destinatario: {item.Messaggio}";
        }

        private void ExecuteDelete()
        {
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Confirm delete",
                Content = "Are you sure?",
                Icon = MessageBoxImage.Question,
                Buttons = MessageBoxButton.YesNo,
                Callback = OnMessageBoxResultReceived
            });
        }

        private void OnMessageBoxResultReceived(MessageBoxResult result)
        {
            if (result == MessageBoxResult.Yes)
            {
                var layer = new MainBusinessLayer(new GiftCardRepositoryMock());

                var response = layer.DeleteGiftCard(item);

                if (!response.Success)
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Errore",
                        Content = response.Message,
                        Icon = MessageBoxImage.Error,
                        Buttons = MessageBoxButton.OK
                    });
                    return;
                }
                else
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Deletion Confirmed",
                        Content = response.Message,
                        Icon = MessageBoxImage.Information
                    });
                }
            }
        }

        private void ExecuteUpdate()
        {
            throw new NotImplementedException();
        }

        private bool _showDetails = false;
        public bool ShowDetails
        {
            get { return _showDetails; }    
            set { _showDetails = value; RaisePropertyChanged(); }   
        }

        private string _Messaggio;
        public string Messaggio
        {
            get { return _Messaggio; }
            set
            {
                _Messaggio = value;
                RaisePropertyChanged();
            }
        }
    }
}
