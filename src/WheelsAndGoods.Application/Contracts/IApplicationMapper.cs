﻿using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.Application.Contracts;

public interface IApplicationMapper
{
	public User ToUser(RegisterRequest request, IHashingProvider hashingProvider);

	public UserInfoResponse ToUserInfoResponse(User user);
	public UserResponse ToUserResponse(JwtResponse jwtResponse, User user);
	public IReadOnlyCollection<TDestination>? MapCollection<TSource, TDestination>(
		IEnumerable<TSource>? sources,
		Func<TSource, TDestination> rule);

	public IReadOnlyCollection<TDestination> MapCollectionOrEmpty<TSource, TDestination>(
		IEnumerable<TSource>? sources,
		Func<TSource, TDestination> rule);

	public IEnumerable<TDestination>? MapEnumerable<TSource, TDestination>(
		IEnumerable<TSource>? sources,
		Func<TSource, TDestination> rule);
}
