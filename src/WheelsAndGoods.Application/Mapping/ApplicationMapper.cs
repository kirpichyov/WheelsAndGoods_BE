using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Models.Filtering;
using WheelsAndGoods.Application.Models.Orders;
using WheelsAndGoods.Application.Models.Paginations;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.Core.Models.Filters;
using WheelsAndGoods.Core.Models.Paginations;

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
			Phone = user.Phone,
			AvatarUrl = user.AvatarUrl
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
			UpdatedAtUtc = order.UpdatedAtUtc,
            Description = order.Description,
            From = order.From,
            To = order.To,
            Price = order.Price,
            Customer = new Customer
            {
                Id = order.Customer.Id,
                FirstName = order.Customer.Firstname,
                LastName = order.Customer.Lastname,
                Phone = order.Customer.Phone
            }
        };
    }

    public FilterOrderModel ToFilterOrderModel(FilterOrderRequest filterOrderRequest)
    {
        if (filterOrderRequest is null)
        {
            throw new ArgumentNullException(nameof(filterOrderRequest));
        }

        return new FilterOrderModel()
        {
            CustomerFullName = filterOrderRequest.CustomerFullName,
            From = filterOrderRequest.From,
            Price = filterOrderRequest.Price,
            Title = filterOrderRequest.Title,
            To = filterOrderRequest.To
        };
    }
    
    public PaginationOrderModel ToPaginationOrderModel(PaginationRequest paginationRequest)
    {
        if (paginationRequest is null)
        {
            throw new ArgumentNullException(nameof(paginationRequest));
        }

        return new PaginationOrderModel()
        {
            PageNumber = paginationRequest.PageNumber,
            PageSize = paginationRequest.PageSize
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

	public void ToUpdatedOrder(UpdateOrderRequest updateOrderRequest, Order order)
	{
		order.Title = updateOrderRequest.Title;
		order.Cargo = updateOrderRequest.Cargo;
		order.DeliveryDeadlineAtUtc = updateOrderRequest.DeliveryDeadlineAtUtc;
		order.Description = updateOrderRequest.Description;
		order.From = updateOrderRequest.From;
		order.To = updateOrderRequest.To;
		order.Price = updateOrderRequest.Price;
		order.UpdatedAtUtc = DateTime.UtcNow;
	}

	public TakeOrderResponse ToTakeOrderResponse(OrderRequest orderRequest, User author)
    {
		return new TakeOrderResponse()
		{
			Id = orderRequest.OrderId,
			Comment = orderRequest.Comment,
			CreaterAtUtc = orderRequest.CreatedAtUtc,
			Author = new TakeOrderRequestAuthor()
			{
				FirstName = author.Firstname,
				LastName = author.Lastname,
				Phone = author.Phone,
				AvatarUrl = author.AvatarUrl
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
