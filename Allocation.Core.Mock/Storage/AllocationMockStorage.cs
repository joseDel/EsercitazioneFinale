using Allocation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allocation.Core.Mock.Storage
{
    public static class AllocationMockStorage
    {
        public static IList<GiftCard> GiftCards { get; set; }

        public static void Initialize()
        {
            GiftCards = new List<GiftCard>();
            GiftCards.Add(new GiftCard
            {
                Id = 1,
                Mittente = "Mario Gatto",
                Destinatario = "Francesco Pandolfi",
                Importo = 100.0,
                Messaggio = "Zippi",
                DataScadenza = new DateTime(2022, 12, 09)
            });
            GiftCards.Add(new GiftCard
            {
                Id = 1,
                Mittente = "Antonello Lai",
                Destinatario = "Sonia 2 Euro",
                Importo = 2.0,
                Messaggio = "Sa puliresa",
                DataScadenza = new DateTime(2022, 10, 09)
            });
        }
    }
}
