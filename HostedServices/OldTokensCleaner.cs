using Noted.Data;
using Noted.Repositories;
using Serilog;

namespace Noted.HostedServices {
    /// <summary>
    /// A hosted service that cleans up old tokens
    /// </summary>
    public class OldTokensCleaner : IHostedService, IDisposable {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer? _timer;
        private readonly CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Constructor for OldTokensCleaner
        /// </summary>
        /// <param name="serviceScopeFactory"> The service scope factory from the DI container </param>
        public OldTokensCleaner(IServiceScopeFactory serviceScopeFactory) {
            _serviceScopeFactory = serviceScopeFactory;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Start the hosted service
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token </param>
        /// <returns> A completed task </returns>
        public Task StartAsync(CancellationToken cancellationToken) {
            Log.Information("Starting old tokens cleaner");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop the hosted service
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token </param>
        /// <returns> A completed task </returns>
        public Task StopAsync(CancellationToken cancellationToken) {
            Log.Information("Stopping old tokens cleaner");
            _timer?.Change(Timeout.Infinite, 0);
            _cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Do the work of the hosted service
        /// </summary>
        /// <param name="state"> The state </param>
        private async void DoWork(object? state) {
            using var scope = _serviceScopeFactory.CreateScope();
            var tokenRepository = scope.ServiceProvider.GetRequiredService<ITokenRepository>();
            var tokens = await tokenRepository.GetAllAsync();

            foreach (var token in tokens) {
                if (token.ExpireDate < DateTime.Now) {
                    await DeleteTokenAsync(tokenRepository, token);
                }
            }
        }

        /// <summary>
        /// Delete a token asynchronously without disposing the context prematurely
        /// </summary>
        /// <param name="tokenRepository">The token repository</param>
        /// <param name="token">The token to delete</param>
        /// <returns>A task representing the deletion operation</returns>
        private async Task DeleteTokenAsync(ITokenRepository tokenRepository, Token token) {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NotedContext>();
            tokenRepository.SetContext(context);
            await tokenRepository.DeleteAsync(token);
        }

        /// <summary>
        /// Dispose of the hosted service
        /// </summary>
        public void Dispose() {
            _cancellationTokenSource.Cancel();
            _timer?.Dispose();
            _cancellationTokenSource.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}