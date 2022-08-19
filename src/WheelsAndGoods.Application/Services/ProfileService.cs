using Kirpichyov.FriendlyJwt.Contracts;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.Blob;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;
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
        private readonly IBlobService _blobService;

        public ProfileService(
            IUnitOfWork unitOfWork,
            IJwtTokenReader tokenReader,
            IHashingProvider hashingProvider,
            IBlobService blobService)
        {
            _unitOfWork = unitOfWork;
            _tokenReader = tokenReader;
            _hashingProvider = hashingProvider;
            _blobService = blobService;
        }

        public async Task ChangePassword(ChangePasswordRequest request)
        {
            var userId = Guid.Parse(_tokenReader.UserId);
            User user = await _unitOfWork.Users.GetById(userId, true);

            if (!_hashingProvider.Verify(request.CurrentPassword, user.PasswordHash))
            {
                throw new AppValidationException("Password is invalid");
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

        public async Task<UpdateAvatarResponse> UpdateAvatar()
        {
            var userId = Guid.Parse(_tokenReader.UserId);
            User user = await _unitOfWork.Users.GetById(userId, true);

            var uploadLink = _blobService.CreateUploadLink(BlobContainer.UserAvatars);
            var readonlyLink = _blobService.CreateReadonlyLink(BlobContainer.UserAvatars, uploadLink.BlobName);

            await _unitOfWork.CommitTransactionAsync(() =>
            {
                user.AvatarBlobName = uploadLink.BlobName;
                user.AvatarUrl = readonlyLink;
            });

            return new UpdateAvatarResponse
            {
                AvatarUrl = readonlyLink,
                UploadAvatarUrl = uploadLink.Uri,
            };
        }
    }
}
