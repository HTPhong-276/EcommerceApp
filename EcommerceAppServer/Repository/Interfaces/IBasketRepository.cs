using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IBasketRepository
    {
        public Task<Basket> GetBasketAsync(string basketId);
        public Task<Basket> UpdateBasketAsync(Basket basket);
        public Task<bool> DeleteBasketAsync(string basketId);
    }
}
