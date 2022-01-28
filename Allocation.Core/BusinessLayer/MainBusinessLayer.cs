using Allocation.Core.Entities;
using Allocation.Core.Repositories;
using Allocation.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allocation.Core.BusinessLayer
{
    public class MainBusinessLayer
    {
        private IGiftCardRepository _giftCardRepository;

        public MainBusinessLayer(IGiftCardRepository repoCards)
        {
            _giftCardRepository = repoCards;
        }

        public IList<GiftCard> FetchAllCards()
        {
            return _giftCardRepository.FetchAll();  
        }

        public Response CreateGiftcard(GiftCard entity)
        {
            //validazione arg
            if (entity == null)
                return new Response { Success = false, Message = "Invalid entity" };

            if (entity.Importo < 0.0)
                return new Response { Success = false, Message = "Amount must be positive" };

            _giftCardRepository.Create(entity);
            
            return new Response
            {
                Success = true,
                Message = $"Gift card by {entity.Mittente} to {entity.Destinatario} created"
            };
        }

        public Response DeleteGiftCard(GiftCard entity)
        {
            if (entity == null)
                return new Response { Success = false, Message = "Invalid entity" };
            if (entity.Id < 0)
                return new Response { Success = false, Message = "Invalid ID" };
            var cardToDelete = FetchAllCards().FirstOrDefault(x => x.Id == entity.Id);
            if (cardToDelete == null)
                return new Response
                {
                    Success = false,
                    Message = $"No gift card with ID: {entity.Id}"
                };
            _giftCardRepository.Delete(cardToDelete);

            return new Response { Success = true, Message = $"Gift card deleted" };
        }

        public Response UpdateGiftCard(GiftCard entity)
        {
            throw new NotImplementedException();
        }
    }
}
