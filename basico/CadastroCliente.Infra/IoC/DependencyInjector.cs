using CadastroCliente.Core.Interfaces.DataAccess;
using CadastroCliente.Core.Interfaces.Repository;
using CadastroCliente.Core.Interfaces.Services;
using CadastroCliente.Core.Services;
using CadastroCliente.Infra.DataAccess;
using CadastroCliente.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroCliente.Infra.IoC
{
    public static class DependencyInjector
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //RegisterMappings.Register();
            //services
            services.AddSingleton<IContext, Context>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IClienteService, ClienteService>();            
            return services;
        }
    }
}
