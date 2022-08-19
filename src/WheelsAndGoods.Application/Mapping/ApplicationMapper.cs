using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Models.Orders;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.Application.Mapping;

public class ApplicationMapper : IApplicationMapper
{
	public User ToUser(RegisterRequest request, IHashingProvider hashingProvider)
	{
		if (request is null)
		{
			throw new ArgumentNullException(nameof(request));
		}
        
		return User.CreateUser(request.Email, hashingProvider.GetHash(request.Password), 
			request.FirstName, request.LastName, request.Phone);
	}

	public UserInfoResponse ToUserInfoResponse(User user)
	{
		if (user is null)
		{
			throw new ArgumentNullException(nameof(user));
		}

		return new UserInfoResponse()
		{
			Id = user.Id,
			Email = user.Email,
			FirstName = user.Firstname,
			LastName = user.Lastname,
			Phone = user.Phone
		};
	}

    public Order ToOrder(CreateOrderRequest createOrderRequest, User customer)
    {
		return new Order()
		{
			Title = createOrderRequest.Title,
			Cargo = createOrderRequest.Cargo,
			DeliveryDeadlineAtUtc = createOrderRequest.DeliveryDeadlineAtUtc,
            CreatedAtUtc = DateTime.UtcNow,
            Description = createOrderRequest.Description,
			From = createOrderRequest.From,
			To = createOrderRequest.To,
			Price = createOrderRequest.Price,
			Customer = customer
		};
    }

	public OrderResponse ToOrderResponse(Order order)
	{
		return new OrderResponse()
		{
			Id = order.Id,
			Title = order.Title,
			Cargo = order.Cargo,
			DeliveryDeadlineAtUtc = order.DeliveryDeadlineAtUtc,
            CreatedAtUtc = order.CreatedAtUtc,
			Description = order.Description,
			From = order.From,
			To = order.To,
			Price = order.Price,
			Customer = new Customer
            {
				FirstName = order.Customer.Firstname,
				LastName = order.Customer.Lastname,
				Phone = order.Customer.Phone
            }
		};
	}
    
    public UserCreatedResponse ToUserCreatedResponse(AuthResponse authResponse, User user)
    {
        return new UserCreatedResponse()
        {
            Jwt = authResponse.Jwt,
            RefreshToken = authResponse.RefreshToken,
            User = ToUserInfoResponse(user),
        };
    }

	public Order ToUpdatedOrder(UpdateOrderRequest updateOrderRequest, Guid orderId, User customer)
	{
		return new Order(orderId)
		{
			Title = updateOrderRequest.Title,
			Cargo = updateOrderRequest.Cargo,
			DeliveryDeadlineAtUtc = updateOrderRequest.DeliveryDeadlineAtUtc,
			Description = updateOrderRequest.Description,
			From = updateOrderRequest.From,
			To = updateOrderRequest.To,
			Price = updateOrderRequest.Price,
			Customer = customer
		};
	}

	public OrderResponse ToOrderResponse(Order order, User user)
	{
		return new OrderResponse()
		{
			Id = order.Id,
			Title = order.Title,
			Cargo = order.Cargo,
			DeliveryDeadlineAtUtc = order.DeliveryDeadlineAtUtc,
			Description = order.Description,
			From = order.From,
			To = order.To,
			Price = order.Price,
			Customer = new Customer
			{
				FirstName = user.Firstname,
				LastName = user.Lastname,
				Phone = user.Phone
			}
		};
	}


	public IReadOnlyCollection<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> sources, Func<TSource, TDestination> rule)
	{
		return sources?.Select(rule).ToArray();
	}

	public IReadOnlyCollection<TDestination> MapCollectionOrEmpty<TSource, TDestination>(IEnumerable<TSource> sources, Func<TSource, TDestination> rule)
	{
		return MapCollection(sources, rule) ?? Array.Empty<TDestination>();
	}
	
	public IEnumerable<TDestination> MapEnumerable<TSource, TDestination>(IEnumerable<TSource> sources, Func<TSource, TDestination> rule)
	{
		return sources?.Select(rule).ToArray();
	}
}
