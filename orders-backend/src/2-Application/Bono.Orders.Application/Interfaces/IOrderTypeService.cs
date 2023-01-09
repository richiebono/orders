using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Application.ViewModels;

namespace Bono.Orders.Application.Interfaces
{
    public interface IOrderTypeService
    {
        int Count(FilterViewModel filter);
        IEnumerable<OrderTypeViewModel> Filter(FilterViewModel filter);
        OrderTypeViewModel GetById(string id);
        
    }
}
