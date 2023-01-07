using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Application.ViewModels;
using Bono.Orders.Domain.Validations;

namespace Bono.Orders.Application.Interfaces
{
    public interface IOrderService : IService<OrderViewModel>
    {
        int Count(FilterViewModel filter);
        IEnumerable<OrderViewModel> Filter(FilterViewModel filter);
        IEnumerable<OrderViewModel> GetAll(string userId);        
    }
}
