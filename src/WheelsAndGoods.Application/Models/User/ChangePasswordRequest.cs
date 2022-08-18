using WheelsAndGoods.Application.Models.Internal;

namespace WheelsAndGoods.Application.Models.User
{
    public class ChangePasswordRequest : IContainsPassword
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
    }
}
