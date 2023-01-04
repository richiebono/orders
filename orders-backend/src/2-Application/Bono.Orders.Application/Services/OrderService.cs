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

            if (string.IsNullOrEmpty(OrderViewModel.OrderTypeId) || string.IsNullOrEmpty(OrderViewModel.UserId) || string.IsNullOrEmpty(OrderViewModel.Customername))
            {
                _validationResult.Add("The sent object was empty.");
                return _validationResult;
            }

            Validator.ValidateObject(OrderViewModel, new ValidationContext(OrderViewModel), true);

            OrderType orderType = _orderTypeRepository.Find(new Guid(OrderViewModel.OrderTypeId));
            User user = _userRepository.Find(new Guid(OrderViewModel.UserId));
            Order Order = new(orderType, OrderViewModel.Customername, user);
            
            _validationResult.Entity = _orderRepository.Create(Order);
            
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
                Order = _mapper.Map<Order>(OrderViewModel);

                try
                {
                    _orderRepository.Update(Order);
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

            Order _Order = _orderRepository.Find(x => x.Id == OrderId && !x.IsDeleted);
            
            return _mapper.Map<OrderViewModel>(_Order);
        }

        public IEnumerable<OrderViewModel> GetAll(string userId)
        {
            if (!Guid.TryParse(userId, out Guid UserId))
                throw new Exception("User is not valid");
            
            var Orders = _orderRepository.Query(x=> x.User.Id == UserId && !x.IsDeleted);

            List<OrderViewModel> _OrderViewModels = _mapper.Map<List<OrderViewModel>>(Orders);

            return _OrderViewModels;
        }
    }
}
