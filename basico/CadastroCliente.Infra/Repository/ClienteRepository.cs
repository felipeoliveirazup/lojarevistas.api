using CadastroCliente.Core.Interfaces.DataAccess;
using CadastroCliente.Core.Interfaces.Repository;
using CadastroCliente.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CadastroCliente.Infra.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IContext _context;
        //private readonly string _connectionstring = @"Data Source=ZUP-1030BH\SQLEXPRESS01;Initial Catalog=lojarevistas;Integrated Security=true";
        private string _connectionstring { get; set; }
        public ClienteRepository(IContext context)
        {
            _context = context;
            _connectionstring = _context.GetConnectionString();
        }
        public async Task<bool> DeleteCliente(Cliente cliente)
        {
            try
            {
                //return _context.Clientes.Remove(cliente);
                using (SqlConnection connection = new SqlConnection(_connectionstring))
                {
                    string queryString = "DELETE FROM CLIENTES WHERE CPF = @CPF";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@CPF", cliente.Cpf);
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cliente> Get(string cpf)
        {
            try
            {
                Cliente result = null;
                //return _context.Clientes.Where(c => c.Cpf == cpf).FirstOrDefault();
                using (SqlConnection connection = new SqlConnection(_connectionstring))
                {
                    
                    string queryString = "SELECT CPF, NOME, IDADE, EMAIL, TELEFONE, ENDERECO FROM CLIENTES WHERE CPF = @CPF";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@CPF", cpf);
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {  //(string nome, int idade, string cpf, string email, string telefone, string endereco)
                            result = new Cliente(reader.GetString(1), reader.GetInt32(2), reader.GetString(0), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                        }
                    }
                    reader.Close();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> InsereCliente(Cliente cliente)
        {
            try
            {
                //_context.Clientes.Add(cliente);
                using (SqlConnection connection = new SqlConnection(_connectionstring))
                {
                    string queryString = "INSERT INTO CLIENTES VALUES(@CPF, @NOME, @IDADE, @EMAIL, @TELEFONE, @ENDERECO)";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@CPF", cliente.Cpf);
                    command.Parameters.AddWithValue("@NOME", cliente.Nome);
                    command.Parameters.AddWithValue("@IDADE", cliente.Idade);
                    command.Parameters.AddWithValue("@EMAIL", cliente.Email);
                    command.Parameters.AddWithValue("@TELEFONE", cliente.Telefone);
                    command.Parameters.AddWithValue("@ENDERECO", cliente.Endereco);
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Cliente>> ListarClientes()
        {
            try
            {
                List<Cliente> result = new List<Cliente>();
                //return _context.Clientes.Where(c => c.Cpf == cpf).FirstOrDefault();
                using (SqlConnection connection = new SqlConnection(_connectionstring))
                {

                    string queryString = "SELECT CPF, NOME, IDADE, EMAIL, TELEFONE, ENDERECO FROM CLIENTES ORDER BY NOME";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {  //(string nome, int idade, string cpf, string email, string telefone, string endereco)
                            result.Add(new Cliente(reader.GetString(1), reader.GetInt32(2), reader.GetString(0), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                        }
                    }
                    reader.Close();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            try
            {
                //_context.Clientes.Add(cliente);
                using (SqlConnection connection = new SqlConnection(_connectionstring))
                {
                    string queryString = "UPDATE CLIENTES SET NOME=@NOME, IDADE = @IDADE, EMAIL = @EMAIL, TELEFONE = @TELEFONE, ENDERECO = @ENDERECO WHERE CPF=@CPF";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@CPF", cliente.Cpf);
                    command.Parameters.AddWithValue("@NOME", cliente.Nome);
                    command.Parameters.AddWithValue("@IDADE", cliente.Idade);
                    command.Parameters.AddWithValue("@EMAIL", cliente.Email);
                    command.Parameters.AddWithValue("@TELEFONE", cliente.Telefone);
                    command.Parameters.AddWithValue("@ENDERECO", cliente.Endereco);
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
