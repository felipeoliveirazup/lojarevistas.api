using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioClienteService.Interfaces
{
    public interface IRelatorioService
    {
        Task<bool> GerarRelatorio();
        //Task DoWork(CancellationToken stoppingToken);
    }
}
