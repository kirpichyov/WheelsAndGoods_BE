﻿using WheelsAndGoods.Application.Contracts;
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

	public AuthResponse ToUserResponse(JwtResponse jwtResponse, User user)
	{
		return new AuthResponse()
		{
			Jwt = jwtResponse,
			User = ToUserInfoResponse(user),
		};
	}

	public Order ToOrder(CreateOrderRequest createOrderRequest)
    {
		return new Order()
		{
			Title = createOrderRequest.Title,
			Cargo = createOrderRequest.Cargo,
			DeliveryDeadlinAtUtc = createOrderRequest.DeliveryDeadlinAtUtc,
			Description = createOrderRequest.Description,
			From = createOrderRequest.From,
			To = createOrderRequest.To,
			Price = createOrderRequest.Price
		};
    }

	public CreateOrderResponce ToCreateOrderResponce(Order order)
	{
		return new CreateOrderResponce()
		{
			Title = order.Title,
			Cargo = order.Cargo,
			DeliveryDeadlinAtUtc = order.DeliveryDeadlinAtUtc,
			Description = order.Description,
			From = order.From,
			To = order.To,
			Price = order.Price
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
