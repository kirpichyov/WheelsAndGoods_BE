using Kirpichyov.FriendlyJwt.Contracts;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Core.Exceptions;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenReader _tokenReader;
        private readonly IHashingProvider _hashingProvider;

        public ProfileService(
            IUnitOfWork unitOfWork,
            IJwtTokenReader tokenReader,
            IHashingProvider hashingProvider)
        {
            _unitOfWork = unitOfWork;
            _tokenReader = tokenReader;
            _hashingProvider = hashingProvider;
        }

        public async Task ChangePassword(ChangePasswordRequest request)
        {
            var userId = Guid.Parse(_tokenReader.UserId);
            User user = await _unitOfWork.Users.GetById(userId, true);

            if (!_hashingProvider.Verify(request.CurrentPassword, user.PasswordHash))
            {
                throw new AppValidationException("Password are invalid");
            }

            if (_hashingProvider.Verify(request.Password, user.PasswordHash))
            {
                throw new AppValidationException("Password must be different");
            }

            await _unitOfWork.CommitTransactionAsync(() =>
            {
                user.PasswordHash = _hashingProvider.GetHash(request.Password);
            });
        }
    }
}
