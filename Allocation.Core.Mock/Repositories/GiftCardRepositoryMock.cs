using Allocation.Core.Entities;
using Allocation.Core.Mock.Storage;
using Allocation.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allocation.Core.Mock.Repositories
{
    public class GiftCardRepositoryMock : IGiftCardRepository
    {
        public void Create(GiftCard card)
        {
            var newId = AllocationMockStorage.GiftCards.Max(x => x.Id) + 1;
            card.Id = newId;
            AllocationMockStorage.GiftCards.Add(card);
        }

        public void Delete(GiftCard card)
        {
            var existingCard = AllocationMockStorage.GiftCards.FirstOrDefault(x => x.Id == card.Id);
            AllocationMockStorage.GiftCards.Remove(existingCard);
        }

        public IList<GiftCard> FetchAll()
        {
            return AllocationMockStorage.GiftCards.ToList();
        }

        public void Update(GiftCard card)
        {
            throw new NotImplementedException();
        }
    }
}
