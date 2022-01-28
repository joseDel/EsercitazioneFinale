using Allocation.Core.BusinessLayer;
using Allocation.Core.Entities;
using Allocation.Core.Mock.Repositories;
using Allocation.Core.Repositories;
using Allocation.WPF.Messaging.Card;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Allocation.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand CreateGiftCard { get; set; }

        public ObservableCollection<GiftCardRowViewModel> _GiftCardsSource;

        private ICollectionView _GiftCards;
        public ICollectionView GiftCards
        {
            get { return _GiftCards; }
            set { _GiftCards = value; RaisePropertyChanged(); }
        }

        public ICommand LoadGiftCardsCommand { get; set; }

        public HomeViewModel()
        {
            CreateGiftCard = new RelayCommand(() => ExecuteShowCreateGiftCard());
            LoadGiftCardsCommand = new RelayCommand(() => ExecuteLoadGiftCards());

            //inizializzazione delle liste
            _GiftCardsSource = new ObservableCollection<GiftCardRowViewModel>();
            _GiftCards = new CollectionView(_GiftCardsSource);

            LoadGiftCardsCommand.Execute(null);
        }

        private void ExecuteLoadGiftCards()
        {
            IGiftCardRepository repo = new GiftCardRepositoryMock();
            MainBusinessLayer layer = new MainBusinessLayer(repo);

            var cards = layer.FetchAllCards();

            _GiftCardsSource.Clear();

            // creazione lista gift card row view model
            foreach (GiftCard item in cards)
            {
                var vmGCRow = new GiftCardRowViewModel(item);
                _GiftCardsSource.Add(vmGCRow);
            }
        }

        private void ExecuteShowCreateGiftCard()
        {
            Messenger.Default.Send(new ShowCreateGiftCardMessage());
        }
    }
}
