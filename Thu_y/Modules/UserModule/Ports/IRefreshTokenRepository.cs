﻿using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.UserModule.Core;

namespace Thu_y.Modules.UserModule.Ports
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        void Delete(RefreshToken entity);
    }
}