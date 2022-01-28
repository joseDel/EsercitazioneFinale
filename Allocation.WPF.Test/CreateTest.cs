using Allocation.Core.BusinessLayer;
using Allocation.Core.Entities;
using Allocation.Core.Mock.Repositories;
using Allocation.Core.Mock.Storage;
using Allocation.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Allocation.WPF.Test
{
    public class CreateTest
    {
        [Fact]
        public void ShouldCreateOk()
        {
            //ARRANGE
            AllocationMockStorage.Initialize();
            // repository
            IGiftCardRepository repo = new GiftCardRepositoryMock();
            // BL
            MainBusinessLayer bl = new MainBusinessLayer(repo);
            //Creo Gift Card da inserire
            GiftCard card = new GiftCard
            {
                Mittente = "Massimo Medda",
                Destinatario = "Genny Savastano",
                Messaggio = "Ci ripijiamo tutto chill che è nuostro",
                Importo = 1000,
                DataScadenza = new DateTime(2022, 10, 20)
            }; 

            //ACT
            var result = bl.CreateGiftcard(card);

            //ASSERT
            Assert.True(result.Success);
        }
    }
}
