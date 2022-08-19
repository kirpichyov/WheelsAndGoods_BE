using Kirpichyov.FriendlyJwt;
using Kirpichyov.FriendlyJwt.Contracts;
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

namespace WheelsAndGoods.Application.Services;

public class AuthService : IAuthService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly AuthOptions _authOptions;
	private readonly IHashingProvider _hashingProvider;
	private readonly IApplicationMapper _mapper;
    private readonly IJwtTokenVerifier _jwtTokenVerifier;

	public AuthService(
		IUnitOfWork unitOfWork, 
		IHashingProvider hashingProvider, 
		IApplicationMapper mapper, 
		IOptions<AuthOptions> options,
        IJwtTokenVerifier jwtTokenVerifier)
	{
		_unitOfWork = unitOfWork;
		_hashingProvider = hashingProvider;
		_mapper = mapper;
        _jwtTokenVerifier = jwtTokenVerifier;
        _authOptions = options.Value;
	}

	public async Task<UserCreatedResponse> CreateUser(RegisterRequest request)
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

		var authResponse = await _unitOfWork.CommitTransactionAsync(() =>
		{
			_unitOfWork.Users.Add(user);
            return GenerateAuthResponse(user);
        });

        return _mapper.ToUserCreatedResponse(authResponse, user);
    }

    public async Task<AuthResponse> CreateUserSession(SignInRequest request)
	{
		var user = await _unitOfWork.Users.GetByEmail(request.Email);

		if (user is null || !_hashingProvider.Verify(request.Password, user.PasswordHash))
		{
			throw new AppValidationException("Credentials is invalid");
		}

        var authResponse = await _unitOfWork.CommitTransactionAsync(() => GenerateAuthResponse(user));
        return authResponse;
    }

    public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request)
    {
        var jwtVerificationResult = _jwtTokenVerifier.Verify(request.AccessToken);

        if (!jwtVerificationResult.IsValid)
        {
            throw new AppValidationException("Access token is invalid");
        }
        
        var accessTokenId = Guid.Parse(jwtVerificationResult.TokenId);
        var userId = Guid.Parse(jwtVerificationResult.UserId);

        var refreshToken = await _unitOfWork.RefreshTokens.GetById(request.RefreshToken, false);

        if (refreshToken is null || refreshToken.AccessTokenId != accessTokenId)
        {
            throw new AppValidationException("Refresh token is not found");
        }

        if (refreshToken.IsInvalidated)
        {
            throw new AppValidationException("Refresh token is invalidated");
        }

        if (IsRefreshTokenExpired(refreshToken, _authOptions.RefreshTokenTTLMinutes))
        {
            throw new AppValidationException("Refresh token is expired");
        }

        var user = await _unitOfWork.Users.GetById(userId, false);

        var authResponse = await _unitOfWork.CommitTransactionAsync(() =>
        {
            _unitOfWork.RefreshTokens.Remove(refreshToken);
            return GenerateAuthResponse(user);
        });

        return authResponse;
    }

    private AuthResponse GenerateAuthResponse(User user)
    {
        var tokenResult = GenerateAccessToken(user);
        var newRefreshToken = GenerateRefreshToken(Guid.Parse(tokenResult.TokenId), user.Id);

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
            }
        };
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
    
    private Guid GenerateRefreshToken(Guid jwtId, Guid userId)
    {
        var token = new RefreshToken()
        {
            CreatedAtUtc = DateTime.UtcNow,
            IsInvalidated = false,
            AccessTokenId = jwtId,
            UserId = userId,
        };

        _unitOfWork.RefreshTokens.Add(token);
        return token.Id;
    }

    private static bool IsRefreshTokenExpired(RefreshToken refreshToken, int lifeTimeMinutes)
    {
        return DateTime.UtcNow >= refreshToken.CreatedAtUtc.AddMinutes(lifeTimeMinutes);
    }
}
