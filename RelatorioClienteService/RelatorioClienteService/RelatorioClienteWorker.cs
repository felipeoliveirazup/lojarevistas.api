using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RelatorioClienteService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RelatorioClienteService
{
    public class RelatorioClienteWorker : BackgroundService
    {
        private readonly ILogger<RelatorioClienteWorker> _logger;

        public RelatorioClienteWorker(IServiceProvider services, ILogger<RelatorioClienteWorker> logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Iniciando geração do relatório.");

            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var relatorioService =
                    scope.ServiceProvider
                        .GetRequiredService<IRelatorioService>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = await relatorioService.GerarRelatorio();
                    await Task.Delay(3000);
                    //if (result) { await StopAsync(new CancellationToken(true)); }
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Finalizando geração do relatório.");

            await Task.CompletedTask;
        }
    }
}
