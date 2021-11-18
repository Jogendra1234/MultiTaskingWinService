using C9ISM.Scheduler.Helpers;
using Microsoft.Extensions.Hosting;
using MultiTaskingWinService.Helper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiTaskingWinService.Services
{
    public class TickerTapJob : IHostedService, IDisposable
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
                (e) => UpdateTickerTap(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromHours((int)ServiceInterval.OneHour));

            return Task.CompletedTask;
        }
        public async Task UpdateTickerTap()
        {
            await UpdateTickerTap(new NewsHelper());
        }

        private async Task UpdateTickerTap(NewsHelper _bulkDealServices)
        { 
            await _bulkDealServices.SaveNews();
           // await _bulkDealServices.UpdateTickerTapdetail();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}