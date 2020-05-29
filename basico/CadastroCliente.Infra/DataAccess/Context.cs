using CadastroCliente.Core.Interfaces.DataAccess;
using CadastroCliente.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroCliente.Infra.DataAccess
{
    public class Context: IContext
    {
        public List<Cliente> Clientes { get; set; }
        public Context()
        {
            Clientes = new List<Cliente>();
        }
    }
}
