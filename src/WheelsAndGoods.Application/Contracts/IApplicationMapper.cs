using WheelsAndGoods.Application.Models.Filtering;
using WheelsAndGoods.Application.Models.Orders;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.Core.Models.Filter;

namespace WheelsAndGoods.Application.Contracts;

public interface IApplicationMapper
{
	public User ToUser(RegisterRequest request, IHashingProvider hashingProvider);
    public UserInfoResponse ToUserInfoResponse(User user);
    public Order ToOrder(CreateOrderRequest createOrderRequest, User customer);
	public OrderResponse ToOrderResponse(Order order);
    public FilterOrderModel ToFilterOrderModel(FilterOrderRequest filterOrderRequest);
    public UserCreatedResponse ToUserCreatedResponse(AuthResponse authResponse, User user);
	public void ToUpdatedOrder(UpdateOrderRequest updateOrderRequest, Order order);
	public TakeOrderResponse ToTakeOrderResponse(OrderRequest orderRequest, User author);

	public IReadOnlyCollection<TDestination> MapCollection<TSource, TDestination>(
		IEnumerable<TSource> sources,
		Func<TSource, TDestination> rule);

	public IReadOnlyCollection<TDestination> MapCollectionOrEmpty<TSource, TDestination>(
		IEnumerable<TSource> sources,
		Func<TSource, TDestination> rule);

	public IEnumerable<TDestination> MapEnumerable<TSource, TDestination>(
		IEnumerable<TSource> sources,
		Func<TSource, TDestination> rule);
}
