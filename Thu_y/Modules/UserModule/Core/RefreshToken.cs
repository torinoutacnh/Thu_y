using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;
using Thu_y.Utils.Infrastructure.Application;

namespace Thu_y.Modules.UserModule.Core
{
    [Table("RefreshToken")]
    public class RefreshToken : Entity
    {
        [ForeignKey("UserId")]
        public UserEntity? User { get; set; }
        public string UserId { get; set; }
        public string? Token { get; set; }
        public DateTimeOffset Expires { get; set; }
        public bool IsExpired => SystemHelper.SystemTimeNow >= Expires;
        public bool IsActive => !IsExpired;
    }
}
