using Kirpichyov.FriendlyJwt.Contracts;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.User.Responses;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.Application.Services;

public class UsersService : IUsersService
{
    private readonly IJwtTokenReader _tokenReader;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationMapper _mapper;

    public UsersService(IJwtTokenReader tokenReader, IUnitOfWork unitOfWork, IApplicationMapper mapper)
    {
        _tokenReader = tokenReader;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserInfoResponse> GetUserInfo()
    {
        var userId = Guid.Parse(_tokenReader.UserId);

        var user = await _unitOfWork.Users.GetById(userId, useTracking: false)
            ?? throw new InvalidOperationException("Can't retrieve user info for unauthorized user.");

        return _mapper.ToUserInfoResponse(user);
    }
}
