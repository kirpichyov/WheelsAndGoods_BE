using Kirpichyov.FriendlyJwt.Contracts;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.Filtering;
using WheelsAndGoods.Application.Models.Orders;
using WheelsAndGoods.Core.Exceptions;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.Core.Models.Enums;
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
                throw new NotFoundException("User not found");
            }

            var order = _applicationMapper.ToOrder(createOrderRequest, user);

            await _unitOfWork.CommitTransactionAsync(() =>
            {
                _unitOfWork.Orders.Add(order);
            });

            var response = _applicationMapper.ToOrderResponse(order);

            return response;
        }

        public async Task<IReadOnlyCollection<OrderResponse>> GetOrders(FilterOrderRequest filterOrderRequest)
        {
            var orders = await _unitOfWork.Orders.GetOrders(_applicationMapper.ToFilterOrderModel(filterOrderRequest));
            return _applicationMapper.MapCollectionOrEmpty(orders, _applicationMapper.ToOrderResponse);
        }

        public async Task<OrderResponse> UpdateOrder(UpdateOrderRequest updateOrderRequest, Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetById(orderId, true);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }
            if (order.Customer.Id != Guid.Parse(_tokenReader.UserId))
            {
                throw new AccessDeniedException("User has no access to this order");
            }
            
            await _unitOfWork.CommitTransactionAsync(() =>
            {
                _applicationMapper.ToUpdatedOrder(updateOrderRequest, order);
            });

            return _applicationMapper.ToOrderResponse(order);
        }

        public async Task<OrderResponse> GetOrderById(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetById(orderId, false);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }

            return _applicationMapper.ToOrderResponse(order);
        }

        public async Task DeleteOrder(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetById(orderId, true);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }
            if (order.Customer.Id != Guid.Parse(_tokenReader.UserId))
            {
                throw new AccessDeniedException("User has no access to this order");
            }

            await _unitOfWork.CommitTransactionAsync(() =>
            {
                order.IsDeleted = true;
            });
        }

        public async Task<TakeOrderResponse> CreateTakeOrderRequest(Guid orderId, TakeOrderRequest request)
        {
            var order = await _unitOfWork.Orders.GetById(orderId, false);
            var userId = Guid.Parse(_tokenReader.UserId);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }
            if (order.Customer.Id == userId)
            {
                throw new AppValidationException("User can't take own order");
            }
            if(await _unitOfWork.OrdersRequests.CheckIfOrderedByUser(orderId, userId))
            {
                throw new AppValidationException("User already take this order");
            }
            if(order.Status != Status.WaitingForContractor)
            {
                throw new AppValidationException($"User can't take order with status {order.Status}");
            }

            var orderRequest = new OrderRequest()
            {
                OrderId = orderId,
                UserId = userId,
                CreatedAtUtc = DateTime.UtcNow,
                Comment = request.Comment
            };

            await _unitOfWork.CommitTransactionAsync(() =>
            {
                _unitOfWork.OrdersRequests.Add(orderRequest);
            });

            var user = await _unitOfWork.Users.GetById(userId, false);

            return _applicationMapper.ToTakeOrderReponse(orderRequest, user);
        }
    }
}
