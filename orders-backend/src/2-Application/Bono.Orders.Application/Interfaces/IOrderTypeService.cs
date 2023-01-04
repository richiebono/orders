using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Application.ViewModels;

namespace Bono.Orders.Application.Interfaces
{
    public interface IOrderTypeService
    {
        IEnumerable<OrderTypeViewModel> GetAll();
        OrderTypeViewModel GetById(string id);
    }
}
