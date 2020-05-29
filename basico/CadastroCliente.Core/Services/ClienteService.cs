using CadastroCliente.Core.Interfaces.Repository;
using CadastroCliente.Core.Interfaces.Services;
using CadastroCliente.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroCliente.Core.Services
{
    public class ClienteService : IClienteService
    {
        public IClienteRepository _clienteRepository { get; set; }
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public Cliente Get(string cpf)
        {
            return _clienteRepository.Get(cpf);
        }

        public bool InsereCliente(Cliente cliente)
        {
            var _cliente = Get(cliente.Cpf);
            if (_cliente != null)
            {
                Console.WriteLine("Cpf já existe.");
                return false;
            }
            try
            {
                return _clienteRepository.InsereCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao inserir cliente. Mensagem: {e.Message}");
                return false;
            }
        }

        public bool RemoveCliente(Cliente cliente)
        {
            var _cliente = Get(cliente.Cpf);
            if (_cliente == null)
            {
                Console.WriteLine("Cpf não existe.");
                return false;
            }
            try
            {
                return _clienteRepository.DeleteCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao remover cliente. Mensagem: {e.Message}");
                return false;
            }
        }

        public bool UpdateCliente(Cliente cliente)
        {
            var _cliente = Get(cliente.Cpf);
            if (_cliente == null)
            {
                Console.WriteLine("Cpf não existe.");
                return false;
            }
            try
            {
                return _clienteRepository.UpdateCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao inserir cliente. Mensagem: {e.Message}");
                return false;
            }
        }

        public IEnumerable<Cliente> ListClientes()
        {
            return _clienteRepository.ListarClientes();
        }
    }
}
