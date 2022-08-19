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

        public async Task<OrderResponse> CreateOrder(CreateOrderRequest createOrderRequest)
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

            var response = _applicationMapper.ToOrderResponse(order);

            return response;
        }

        public async Task<IReadOnlyCollection<OrderResponse>> GetOrders()
        {
            var orders = await _unitOfWork.Orders.GetOrders();
            return _applicationMapper.MapCollectionOrEmpty(orders, _applicationMapper.ToOrderResponse);
        }

        public async Task<OrderResponse> UpdateOrder(UpdateOrderRequest updateOrderRequest, Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetById(orderId, false);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }
            if (order.Customer.Id != Guid.Parse(_tokenReader.UserId))
            {
                throw new AccessDeniedException("User has no access to this order");
            }

            var updatedOrder = _applicationMapper.ToUpdatedOrder(updateOrderRequest, orderId, order.Customer);
            await _unitOfWork.CommitTransactionAsync(() =>
            {
                _unitOfWork.Orders.Update(updatedOrder);
            });

            return _applicationMapper.ToOrderResponse(updatedOrder, order.Customer);
        }
    }
}
