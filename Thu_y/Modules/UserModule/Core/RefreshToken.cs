using Microsoft.EntityFrameworkCore;
using Thu_y.Infrastructure.Model;
using Thu_y.Utils.Infrastructure.Application;

namespace Thu_y.Modules.UserModule.Core
{
    [Owned]
    public class RefreshToken : Entity
    {
        public string? Token { get; set; }
        public DateTimeOffset Expires { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsExpired => SystemHelper.SystemTimeNow >= Expires;
        public bool IsActive => !IsExpired;
    }
}
