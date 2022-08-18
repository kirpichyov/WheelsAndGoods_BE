using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kirpichyov.FriendlyJwt.Contracts;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.Orders;
using WheelsAndGoods.Core.Exceptions;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenReader _tokenReader;
        private readonly IApplicationMapper _applicationMapper;

        public OrdersService(IUnitOfWork unitOfWork, IJwtTokenReader jwtTokenReader, IApplicationMapper applicationMapper)
        {
            _unitOfWork = unitOfWork;
            _tokenReader = jwtTokenReader;
            _applicationMapper = applicationMapper;
        }

        public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            var user = await _unitOfWork.Users.GetById(Guid.Parse(_tokenReader.UserId), true);

            if (user is null)
            {
                throw new NotFoundException("Customer not found");
            }

            var order = _applicationMapper.ToOrder(createOrderRequest, user);
            await _unitOfWork.CommitTransactionAsync(() =>
            {
                _unitOfWork.Orders.Add(order);
            });

            var responce = _applicationMapper.ToCreateOrderResponse(order, user);

            return responce;
        }
    }
}
