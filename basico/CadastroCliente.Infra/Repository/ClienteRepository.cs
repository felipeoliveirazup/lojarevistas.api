using CadastroCliente.Core.Interfaces.DataAccess;
using CadastroCliente.Core.Interfaces.Repository;
using CadastroCliente.Domain.Entidades;
using CadastroCliente.Infra.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroCliente.Infra.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Context _context;
        public ClienteRepository(IContext context)
        {
            _context = (Context)context;
        }
        public bool DeleteCliente(Cliente cliente)
        {
            try
            {
                return _context.Clientes.Remove(cliente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Cliente Get(string cpf)
        {
            return _context.Clientes.Where(c => c.Cpf == cpf).FirstOrDefault();
        }

        public bool InsereCliente(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Cliente> ListarClientes()
        {
            return _context.Clientes.ToList();
        }

        public bool UpdateCliente(Cliente cliente)
        {
            try
            {
                var cli = _context.Clientes.Where(c => c.Cpf.Equals(cliente.Cpf)).FirstOrDefault();
                cli = cliente;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
