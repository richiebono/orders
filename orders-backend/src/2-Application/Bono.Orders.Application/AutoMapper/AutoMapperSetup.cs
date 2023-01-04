using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Application.ViewModels;
using Bono.Orders.Domain.Entities;

namespace Bono.Orders.Application.AutoMapper
{
    public class AutoMapperSetup: Profile
    {
        public AutoMapperSetup()
        {

            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();
            CreateMap<OrderViewModel, Order>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderTypeViewModel, OrderType>();
            CreateMap<OrderType, OrderTypeViewModel>();
        }
    }
}
