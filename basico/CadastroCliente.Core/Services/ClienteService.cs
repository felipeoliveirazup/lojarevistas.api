using CadastroCliente.Core.Interfaces.Repository;
using CadastroCliente.Core.Interfaces.Services;
using CadastroCliente.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Core.Services
{
    public class ClienteService : IClienteService
    {
        public IClienteRepository _clienteRepository { get; set; }
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<Cliente> Get(string cpf)
        {
            return await _clienteRepository.Get(cpf);
        }

        public async Task<bool> InsereCliente(Cliente cliente)
        {
            var _cliente = await Get(cliente.Cpf);
            if (_cliente != null)
            {
                Console.WriteLine("Cpf já existe.");
                return false;
            }
            try
            {
                return await _clienteRepository.InsereCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao inserir cliente. Mensagem: {e.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveCliente(Cliente cliente)
        {
            var _cliente = Get(cliente.Cpf);
            if (_cliente == null)
            {
                Console.WriteLine("Cpf não existe.");
                return false;
            }
            try
            {
                return await _clienteRepository.DeleteCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao remover cliente. Mensagem: {e.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            var _cliente = Get(cliente.Cpf);
            if (_cliente == null)
            {
                Console.WriteLine("Cpf não existe.");
                return false;
            }
            try
            {
                return await _clienteRepository.UpdateCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao inserir cliente. Mensagem: {e.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Cliente>> ListClientes()
        {
            return await _clienteRepository.ListarClientes();
        }
    }
}
