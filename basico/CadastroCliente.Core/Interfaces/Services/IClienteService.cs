using CadastroCliente.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Core.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Cliente> Get(string cpf);
        Task<bool> InsereCliente(Cliente cliente);
        Task<bool> RemoveCliente(Cliente cliente);
        Task<bool> UpdateCliente(Cliente cliente);
        Task<IEnumerable<Cliente>> ListClientes();

    }
}
