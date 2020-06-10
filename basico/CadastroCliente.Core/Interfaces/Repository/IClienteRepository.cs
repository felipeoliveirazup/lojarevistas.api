using CadastroCliente.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Core.Interfaces.Repository
{
    public interface IClienteRepository
    {
        Task<bool> InsereCliente(Cliente cliente);
        Task<bool> UpdateCliente(Cliente cliente);
        Task<bool> DeleteCliente(Cliente cliente);
        Task<IEnumerable<Cliente>> ListarClientes();
        Task<Cliente> Get(string cpf);
    }
}
