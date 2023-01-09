using AutoMapper;
using System;
using System.Collections.Generic;
using Bono.Orders.Application.Interfaces;
using Bono.Orders.Application.ViewModels;
using Bono.Orders.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using ValidationResult = Bono.Orders.Domain.Validations.ValidationResult;
using Bono.Orders.Domain.Interfaces.Repository;
using Bono.Orders.Domain.Interfaces.Business;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bono.Orders.Application.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderTypeRepository _orderTypeRepository;
        private readonly IMapper _mapper;
        private readonly ValidationResult _validationResult;

        public OrderService(IOrderRepository orderRepository,
            IUserRepository userRepository,
            IOrderTypeRepository orderTypeRepository,
            IMapper mapper,
            ValidationResult validationResult)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _orderTypeRepository = orderTypeRepository;
            _mapper = mapper;
            _validationResult = validationResult;
        }

        public ValidationResult Post(OrderViewModel OrderViewModel)
        {
            if (OrderViewModel.Id != Guid.Empty)
                _validationResult.Add("OrderID must be empty");

            if (string.IsNullOrEmpty(OrderViewModel.OrderTypeId) || string.IsNullOrEmpty(OrderViewModel.UserId) || string.IsNullOrEmpty(OrderViewModel.CustomerName))
            {
                _validationResult.Add("The sent object was empty.");
                return _validationResult;
            }

            Validator.ValidateObject(OrderViewModel, new ValidationContext(OrderViewModel), true);

            OrderType orderType = _orderTypeRepository.Find(new Guid(OrderViewModel.OrderTypeId));
            User user = _userRepository.Find(new Guid(OrderViewModel.UserId));
            Order Order = new(orderType, OrderViewModel.CustomerName, user);

            _validationResult.Data = _orderRepository.Create(Order);

            return _validationResult;

        }

        public ValidationResult Put(OrderViewModel OrderViewModel)
        {
            if (OrderViewModel.Id == Guid.Empty)
                _validationResult.Add("OrderID is not valid");

            Order Order = _orderRepository.Find(x => x.Id == OrderViewModel.Id && !x.IsDeleted);
            if (Order == null)
                _validationResult.Add("Order not found");

            if (!_validationResult.Errors.Any())
            {
                Order.CustomerName = OrderViewModel.CustomerName;
                Order.Type = _orderTypeRepository.Find(new Guid(OrderViewModel.OrderTypeId));
                Order.DateUpdated = DateTime.Now;

                try
                {
                    bool updated = _orderRepository.Update(Order, x => x.Id == Order.Id);

                    if (updated)
                        _validationResult.Data = Order;
                    else
                        _validationResult.Add("Order not updated");
                }
                catch (Exception ex)
                {
                    _validationResult.Add(ex.Message);
                }
            }

            return _validationResult;
        }

        public ValidationResult Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid OrderId))
                _validationResult.Add("OrderID is not valid");

            Order _Order = _orderRepository.Find(x => x.Id == OrderId && !x.IsDeleted);

            if (_Order == null)
                _validationResult.Add("Order not found");

            try
            {
                _orderRepository.Delete(_Order);
            }
            catch (Exception ex)
            {
                _validationResult.Add(ex.Message);
            }

            return _validationResult;


        }

        public IEnumerable<OrderViewModel> GetAll()
        {
            var Orders = _orderRepository.GetAll();

            List<OrderViewModel> _OrderViewModels = _mapper.Map<List<OrderViewModel>>(Orders);

            return _OrderViewModels;
        }

        public OrderViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid OrderId))
                throw new Exception("OrderID is not valid");

            Order order = _orderRepository.Find(x => x.Id == OrderId && !x.IsDeleted);

            return new OrderViewModel
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                DateCreated = order.DateCreated,
                DateUpdated = order.DateUpdated,
                OrderTypeName = order.Type.Type,
                UserName = order.User.UserName,
                OrderTypeId = order.Type.Id.ToString(),
                UserId = order.User.Id.ToString()
            };
        }

        public IEnumerable<OrderViewModel> GetAll(string userId)
        {
            if (!Guid.TryParse(userId, out Guid UserId))
                throw new Exception("User is not valid");

            var Orders = _orderRepository.Query(x => x.User.Id == UserId && !x.IsDeleted);

            List<OrderViewModel> _OrderViewModels = _mapper.Map<List<OrderViewModel>>(Orders);

            return _OrderViewModels;
        }

        public IEnumerable<OrderViewModel> Filter(FilterViewModel filter)
        {
            var orders = new List<Order>();

            orders = _orderRepository.Query(x => 
                (
                    string.IsNullOrEmpty(filter.search) || 
                    x.CustomerName.Contains(filter.search) || 
                    x.CustomerName == filter.search
                ) && 
                (
                    string.IsNullOrEmpty(filter.type) || 
                    x.Type.Id == new Guid(filter.type)
                ) &&
                !x.IsDeleted
            ).ToList();

            List<OrderViewModel> orderViewModels = orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                OrderTypeName = x.Type.Type,
                UserName = x.User.UserName,
                OrderTypeId = x.Type.Id.ToString(),
                UserId = x.User.Id.ToString()
            }).ToList();

            if (!string.IsNullOrEmpty(filter.order) && filter.order.ToLower() == "desc")
            {
                if (filter.sort == "id")
                    orderViewModels = orderViewModels.OrderByDescending(x => x.Id).ToList();

                if (filter.sort == "customerName")
                    orderViewModels = orderViewModels.OrderByDescending(x => x.CustomerName).ToList();

                if (filter.sort == "dataCreated")
                    orderViewModels = orderViewModels.OrderByDescending(x => x.DateCreated).ToList();

                if (filter.sort == "dateUpdated")
                    orderViewModels = orderViewModels.OrderByDescending(x => x.DateUpdated).ToList();

                if (filter.sort == "orderTypeName")
                    orderViewModels = orderViewModels.OrderByDescending(x => x.OrderTypeName).ToList();
            }
            else
            {
                if (filter.sort == "id")
                    orderViewModels = orderViewModels.OrderBy(x => x.Id).ToList();

                if (filter.sort == "customerName")
                    orderViewModels = orderViewModels.OrderBy(x => x.CustomerName).ToList();

                if (filter.sort == "dateCreated")
                    orderViewModels = orderViewModels.OrderBy(x => x.DateCreated).ToList();

                if (filter.sort == "dateUpdated")
                    orderViewModels = orderViewModels.OrderBy(x => x.DateUpdated).ToList();

                if (filter.sort == "orderTypeName")
                    orderViewModels = orderViewModels.OrderBy(x => x.OrderTypeName).ToList();
            }

            return orderViewModels.Skip(filter.start).Take(filter.size+1).ToList();
        }

        public int Count(FilterViewModel filter)
        {
            return _orderRepository.Query(x => (filter.search == null || x.CustomerName.Contains(filter.search)) && !x.IsDeleted).OrderBy(x => x.CustomerName).Count();
        }
    }
}
