using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.UserModule.Ports;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RefreshTokenService(IServiceProvider serviceProvider)
        {
            _refreshTokenRepository = serviceProvider.GetRequiredService<IRefreshTokenRepository>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }
    }
}
