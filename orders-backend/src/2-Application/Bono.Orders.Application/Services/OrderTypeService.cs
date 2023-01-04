
using AutoMapper;
using System;
using System.Collections.Generic;
using Bono.Orders.Application.Interfaces;
using Bono.Orders.Application.ViewModels;
using Bono.Orders.Domain.Entities;
using ValidationResult = Bono.Orders.Domain.Validations.ValidationResult;
using Bono.Orders.Domain.Interfaces.Repository;

namespace Bono.Orders.Application.Services
{
    public class OrderTypeService : IOrderTypeService
    {

        private readonly IOrderTypeRepository _orderTypeRepository;
        private readonly IMapper _mapper;

        public OrderTypeService(IOrderTypeRepository orderTypeRepository,
            IMapper mapper)
        {
            _orderTypeRepository = orderTypeRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrderTypeViewModel> GetAll()
        {
            var OrderTypes = _orderTypeRepository.GetAll();

            List<OrderTypeViewModel> _OrderTypeViewModels = _mapper.Map<List<OrderTypeViewModel>>(OrderTypes);

            return _OrderTypeViewModels;
        }

        public OrderTypeViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid OrderTypeId))
                throw new Exception("ID is not valid");

            OrderType _OrderType = _orderTypeRepository.Find(x => x.Id == OrderTypeId && !x.IsDeleted);
            if (_OrderType == null)
                throw new Exception("OrderType not found");

            return _mapper.Map<OrderTypeViewModel>(_OrderType);
        }
    }
}
