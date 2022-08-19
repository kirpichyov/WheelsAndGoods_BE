using Kirpichyov.FriendlyJwt;
using Microsoft.Extensions.Options;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;
using WheelsAndGoods.Core.Exceptions;
using WheelsAndGoods.Application.Options;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Contracts;
using WheelsAndGoods.Application.Models.Auth;
using WheelsAndGoods.Application.Models.ValidationMessages;

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

	public async Task<AuthResponse> CreateUser(RegisterRequest request)
	{
		bool emailInUse = await _unitOfWork.Users.IsEmailExists(request.Email);

		if (emailInUse)
		{
			throw new AppValidationException("Email is already in use");
		}

		bool phoneInUse = await _unitOfWork.Users.IsPhoneExists(request.Phone);

		if (phoneInUse)
		{
			throw new AppValidationException("Phone is already in use");
		}

		User user = _mapper.ToUser(request, _hashingProvider);

		await _unitOfWork.CommitTransactionAsync(() =>
		{
			_unitOfWork.Users.Add(user);
		});

        return await GenerateAuthResponse(user);
	}

    public async Task<AuthResponse> CreateUserSession(SignInRequest request)
	{
		var user = await _unitOfWork.Users.GetByEmail(request.Email);

		if (user is null || !_hashingProvider.Verify(request.Password, user.PasswordHash))
		{
			throw new AppValidationException(CredentialsMessages.CredentialsIsInvalidError);
		}

        return await GenerateAuthResponse(user);
    }
    
    private async Task<Guid> GenerateRefreshToken(Guid jwtid, Guid userId)
    {
        var token = new RefreshToken()
        {
            CreatedAtUtc = DateTime.UtcNow,
            IsInvalidated = false,
            AccessTokenId = jwtid,
            UserId = userId
        };

        await _unitOfWork.CommitTransactionAsync(() =>
        {
            _unitOfWork.RefreshTokens.Add(token);
        });

        return token.Id;
    }

	private GeneratedTokenInfo GenerateAccessToken(User user)
	{
		TimeSpan lifeTime = TimeSpan.FromMinutes(_authOptions.AccessTokenTTLMinutes);

		return new JwtTokenBuilder(lifeTime, _authOptions.Secret)
			.WithAudience(_authOptions.Audience)
			.WithIssuer(_authOptions.Issuer)
			.WithUserEmailPayloadData(user.Email)
			.WithUserIdPayloadData(user.Id.ToString())
			.WithUserRolePayloadData(user.Role.ToString())
			.Build();
	}

    private async Task<AuthResponse> GenerateAuthResponse(User user)
    {
        var tokenResult = GenerateAccessToken(user);
        var newRefreshToken = await GenerateRefreshToken(Guid.Parse(tokenResult.TokenId), user.Id);

        return new AuthResponse()
        {
            Jwt = new JwtResponse()
            {
                AccessToken = tokenResult.Token,
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_authOptions.AccessTokenTTLMinutes)
            },
            RefreshToken = new RefreshTokenRespone()
            {
                Token = newRefreshToken.ToString(),
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_authOptions.RefreshTokenTTLMinutes)
            },
            User = new UserInfoResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.Phone,
                FirstName = user.Firstname,
                LastName = user.Lastname
            }
        };
    }
}
