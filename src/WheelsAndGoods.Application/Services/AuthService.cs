using Kirpichyov.FriendlyJwt;
using Microsoft.Extensions.Options;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;
using WheelsAndGoods.Application.Options;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Contracts;
using WheelsAndGoods.Application.Models.Auth;
using WheelsAndGoods.Core.Exceptions;

namespace WheelsAndGoods.Application.Services;

public class AuthService : IAuthService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly AuthOptions _authOptions;
	private readonly IHashingProvider _hashingProvider;
	private readonly IApplicationMapper _mapper;

	public AuthService(
		IUnitOfWork unitOfWork, 
		IHashingProvider hashingProvider, 
		IApplicationMapper mapper, 
		IOptions<AuthOptions> options)
	{
		_unitOfWork = unitOfWork;
		_hashingProvider = hashingProvider;
		_mapper = mapper;
		_authOptions = options.Value;
	}

	public async Task<UserResponse> CreateUser(RegisterRequest request)
	{
		bool emailInUse = await _unitOfWork.Users.IsEmailExists(request.Email);

		if (emailInUse)
		{
			throw new ApplicationException("Email is already in use");
		}

		bool phoneInUse = await _unitOfWork.Users.IsPhoneExists(request.Phone);

		if (phoneInUse)
		{
			throw new ApplicationException("Phone is already in use");
		}

		User user = _mapper.ToUser(request, _hashingProvider);

		await _unitOfWork.CommitTransactionAsync(() =>
		{
			_unitOfWork.Users.Add(user);
		});
		
		// TODO: Add creation JWT for user
		
		return _mapper.ToUserInfoResponse(user);
	}
	public async Task CreateUserSession(SignInRequest request)
	{
		User? user = await _unitOfWork.Users.GetByEmail(request.Email);

		if (user is null || !_hashingProvider.Verify(request.Password, user.PasswordHash))
		{
			throw new AppValidationException("Credentials are invalid");
		}
	}
}
