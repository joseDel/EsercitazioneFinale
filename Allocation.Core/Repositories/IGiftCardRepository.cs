using Allocation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allocation.Core.Repositories
{
    public interface IGiftCardRepository
    {
        IList<GiftCard> FetchAll();

        void Create(GiftCard card);

        void Update(GiftCard card);

        void Delete(GiftCard card);
    }
}
