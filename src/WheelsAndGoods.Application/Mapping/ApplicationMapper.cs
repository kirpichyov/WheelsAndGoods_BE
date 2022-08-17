using WheelsAndGoods.Application.Contracts;
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

	public UserResponse ToUserResponse(JwtResponse jwtResponse, User user)
	{
		return new UserResponse()
		{
			Jwt = jwtResponse,
			User = ToUserInfoResponse(user),
		};
	}

	public IReadOnlyCollection<TDestination>? MapCollection<TSource, TDestination>(IEnumerable<TSource>? sources, Func<TSource, TDestination> rule)
	{
		return sources?.Select(rule).ToArray();
	}

	public IReadOnlyCollection<TDestination> MapCollectionOrEmpty<TSource, TDestination>(IEnumerable<TSource>? sources, Func<TSource, TDestination> rule)
	{
		return MapCollection(sources, rule) ?? Array.Empty<TDestination>();
	}
	
	public IEnumerable<TDestination>? MapEnumerable<TSource, TDestination>(IEnumerable<TSource>? sources, Func<TSource, TDestination> rule)
	{
		return sources?.Select(rule).ToArray();
	}
}
