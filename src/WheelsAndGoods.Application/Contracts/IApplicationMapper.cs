﻿using WheelsAndGoods.Application.Models.Orders;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.Application.Contracts;

public interface IApplicationMapper
{
	public User ToUser(RegisterRequest request, IHashingProvider hashingProvider);
    public UserInfoResponse ToUserInfoResponse(User user);
    public Order ToOrder(CreateOrderRequest createOrderRequest, User customer);
	public OrderResponse ToOrderResponse(Order order);
    public UserCreatedResponse ToUserCreatedResponse(AuthResponse authResponse, User user);
	public OrderResponse ToOrderResponse(Order order, User user);
	public Order ToUpdatedOrder(UpdateOrderRequest updateOrderRequest, Guid orderId, User customer);

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
