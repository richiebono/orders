
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
        private readonly IUserService _userService;        
        private readonly IOrderTypeService _orderTypeService;
        private readonly IMapper _mapper;
        private readonly ValidationResult _validationResult;

        public OrderService(IOrderRepository orderRepository,
            IUserService userService,
            IOrderTypeService orderTypeService,
            IMapper mapper, 
            ValidationResult validationResult)
        {
            _orderRepository = orderRepository;
            _userService = userService;
            _orderTypeService = orderTypeService;
            _mapper = mapper;
            _validationResult = validationResult;
        }

        public ValidationResult Post(OrderViewModel OrderViewModel)
        {
            if (OrderViewModel.Id != Guid.Empty)
                _validationResult.Add("ID must be empty");

            Validator.ValidateObject(OrderViewModel, new ValidationContext(OrderViewModel), true);

            Order Order = _mapper.Map<Order>(OrderViewModel);
            
            _validationResult.Entity = _orderRepository.Create(Order);

            if (_validationResult.Entity == null)
            {
                _validationResult.Add("The Entity you are trying to record is null, please try again!");
            }
            
            return _validationResult;
            
        }

        public ValidationResult Put(OrderViewModel OrderViewModel)
        {
            if (OrderViewModel.Id == Guid.Empty)
                _validationResult.Add("ID is invalid");

            Order Order = _orderRepository.Find(x => x.Id == OrderViewModel.Id && !x.IsDeleted);
            if (Order == null)
                _validationResult.Add("Order not found");

            Order = _mapper.Map<Order>(OrderViewModel);

            try
            {
                _orderRepository.Update(Order);
            }
            catch (Exception ex)
            {
                _validationResult.Add(ex.Message);
            }

            return _validationResult;
        }

        public ValidationResult Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid OrderId))
                _validationResult.Add("ID is not valid");

            Order _Order = _orderRepository.Find(x => x.Id == OrderId && !x.IsDeleted);

            if (_Order == null)
                throw new Exception("Order not found");

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
                throw new Exception("ID is not valid");

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
