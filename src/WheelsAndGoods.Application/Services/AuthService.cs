using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;
using WheelsAndGoods.Core.Exceptions;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.Application.Services;

public class AuthService : IAuthService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IHashingProvider _hashingProvider;
	private readonly IApplicationMapper _mapper;

	public AuthService(IUnitOfWork unitOfWork, IHashingProvider hashingProvider, IApplicationMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_hashingProvider = hashingProvider;
		_mapper = mapper;
	}

	public async Task<UserInfoResponse> CreateUser(RegisterRequest request)
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
		
		// TODO: Add creation JWT for user
		
		return _mapper.ToUserInfoResponse(user);
	}
}
