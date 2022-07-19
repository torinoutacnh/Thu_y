using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thu_y.Modules.UserModule.Core;

namespace Thu_y.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public UserEntity UserEntity => (UserEntity)HttpContext.Items["UserEntity"];
    }
}
