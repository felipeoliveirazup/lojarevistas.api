using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RelatorioClienteService.Interfaces;
using RelatorioClienteService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioClienteService.Services
{
    public class DynamicObjeto : DynamicObject { }
    public class RelatorioService : IRelatorioService
    {
        private IConfiguration _configuration;
        private ILogger<RelatorioClienteWorker> _logger;

        public RelatorioService(IConfiguration configuration, ILogger<RelatorioClienteWorker> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        //string dbConn = configuration.GetSection("MySettings").GetSection("DbConnection").Value;
        public async Task<bool> GerarRelatorio()
        {
            List<Cliente> _clientes = new List<Cliente>();
            try
            {
                //ler registros
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                //new SqlConnection(@"Data Source=ZUP-1030BH\SQLEXPRESS01;Initial Catalog=lojarevistas;Integrated Security=true"))
                {
                    string queryString = "SELECT CPF, NOME, IDADE, EMAIL, TELEFONE, ENDERECO FROM CLIENTES";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dynamic cliente = new Cliente();
                            cliente.Cpf = reader.GetString(0);
                            cliente.Nome = reader.GetString(1);
                            cliente.Idade = reader.GetInt32(2);
                            cliente.Email = reader.GetString(3);
                            cliente.Telefone = reader.GetString(4);
                            cliente.Endereco = reader.GetString(5);
                            _clientes.Add(cliente);
                        }
                    }
                    reader.Close();
                }
                //gerar arquivo com registros
                if (_clientes.Count == 0)
                {
                    throw new Exception("Não encontrado registros");
                }
                else
                {
                    return await GerarArquivoCliente(_clientes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar relatório. Mensagem: {ex.Message}");
            }
        }

        private string GetCabecalho()
        {
            return $@"|{"CPF".Centraliza(5)}|{"NOME".Centraliza(18)}| ID. |{"E-MAIL".Centraliza(12)}|{"TELEFONE".Centraliza(6)}|{"ENDERECO".Centraliza(16)}|";
            //       | 01664957600 |
        }

        private string GetFileName()
        {
            return $"RelatorioClientes{DateTime.Now.ToString("ddMMyyyyHHmmssfff")}";
        }

        private async Task<bool> GerarArquivoCliente(List<Cliente> itens)
        {
            _logger.LogInformation("Gerando arquivo");
            string docPath = _configuration.GetSection("Parameters").GetValue<string>("PathFiles");
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"{GetFileName()}.txt")))
            {
                await outputFile.WriteLineAsync(GetCabecalho());
                foreach (var item in itens)
                {
                    await outputFile.WriteLineAsync(
                        $@"|{item.Cpf.AjustaColuna(13)}|{item.Nome.AjustaColuna(40)}|{item.Idade.ToString().AjustaColuna(5)}|{item.Email.AjustaColuna(30)}|{item.Telefone.AjustaColuna(20)}|{item.Endereco.AjustaColuna(40)}|");
                }
            }
            return true;
        }
    }
}
