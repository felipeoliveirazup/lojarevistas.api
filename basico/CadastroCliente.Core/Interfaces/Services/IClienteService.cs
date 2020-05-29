using CadastroCliente.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroCliente.Core.Interfaces.Services
{
    public interface IClienteService
    {
        Cliente Get(string cpf);
        bool InsereCliente(Cliente cliente);
        bool RemoveCliente(Cliente cliente);
        bool UpdateCliente(Cliente cliente);
        IEnumerable<Cliente> ListClientes();

    }
}
