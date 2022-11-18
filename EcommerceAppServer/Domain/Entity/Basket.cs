using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Basket
    {
        public Basket()
        {
        }

        public Basket(string basketId)
        {
            BasketId = basketId;
        }

        public string BasketId { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
