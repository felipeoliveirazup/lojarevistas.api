using CadastroCliente.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroCliente.Core.Interfaces.Repository
{
    public interface IClienteRepository
    {
        bool InsereCliente(Cliente cliente);
        bool UpdateCliente(Cliente cliente);
        bool DeleteCliente(Cliente cliente);
        IEnumerable<Cliente> ListarClientes();
        Cliente Get(string cpf);
    }
}
