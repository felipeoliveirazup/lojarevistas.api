using CadastroCliente.Core.Extension;
using CadastroCliente.Core.Interfaces.Services;
using CadastroCliente.Domain.Entidades;
using CadastroCliente.Infra.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace CadastroCliente
{
    class Program
    {
        private static IClienteService _clienteService;
        static void Main(string[] args)
        {
            IServiceCollection collection = new ServiceCollection();
            collection.RegisterServices();
            IServiceProvider serviceProvider = collection.BuildServiceProvider();
            _clienteService = serviceProvider.GetService<IClienteService>();
            Console.WriteLine("=========== Cadastro Cliente ===========");
            string sOpcao = string.Empty;
            do
            {
                sOpcao = GetOpcaoMenu();
                int iOpcao = 0;
                int.TryParse(sOpcao, out iOpcao);
                if ((iOpcao <= 0) || (iOpcao > 4))
                {
                    Console.WriteLine("Opção inválida");
                }
                CallOpcao(iOpcao).Wait();

            } while (sOpcao != "s");
        }

        static string GetOpcaoMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("1. Create cliente");
            Console.WriteLine("2. Read cliente");
            Console.WriteLine("3. Update cliente");
            Console.WriteLine("4. Delete cliente");
            Console.WriteLine("========================================");
            Console.Write("Digite a opção(s para sair): ");
            var opcao = Console.ReadLine();
            return opcao;
        }

        async static Task CallOpcao(int opcao)
        {//static async Task MainAsync()
            switch (opcao)
            {
                case 1:
                    await InsereCliente();
                    break;
                case 2:
                    await ReadCliente();
                    break;
                case 3:
                    await AtualizaCliente();
                    break;
                case 4:
                    await RemoveCliente();
                    break;
                default:
                    break;
            }
        }

        static string LerString(bool podeservazia = false)
        {
            while (true)
            {
                var texto = Console.ReadLine();
                if (string.IsNullOrEmpty(texto) && !podeservazia)
                {
                    Console.WriteLine("Conteúdo inválido");
                }
                return texto;
            }
        }

        static int? LerInt(bool podeservazia = false)
        {
            while (true)
            {
                var texto = Console.ReadLine();
                if (string.IsNullOrEmpty(texto) && podeservazia) { return null; }
                int i = 0;
                try
                {
                    i = int.Parse(texto);
                    return i;
                }
                catch (Exception)
                {
                    Console.WriteLine("Conteúdo inválido");
                }
            }
        }
        
        async static Task AtualizaCliente()
        {
            Console.WriteLine("======== Atualizar Cliente ========");
            Console.WriteLine("Informe o cpf:");
            var cpf = LerString();
            var cliente = await _clienteService.Get(cpf);
            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado!");
                return;
            }
            Console.WriteLine($"Informe o nome({cliente.Nome.ShowOnlyEndValue(4)}):");
            var newNome = LerString(true);
            Console.WriteLine($"Informe a idade({cliente.Idade.ToString().ShowOnlyEndValue(1)}): ");
            var newIdade = LerInt(true);
            Console.WriteLine($"Informe o e-mail({cliente.Email.ShowOnlyEndValue(4)}): ");
            var newEmail = LerString(true);
            Console.WriteLine($"Informe o telefone({cliente.Telefone.ShowOnlyEndValue(4)}): ");
            var newTelefone = LerString(true);
            Console.WriteLine($"Informe o endereço({cliente.Endereco.ShowOnlyEndValue(4)}): ");
            var newEndereco = LerString(true);
            cliente.Nome = string.IsNullOrEmpty(newNome) ? cliente.Nome : newNome;
            cliente.Email = string.IsNullOrEmpty(newEmail) ? cliente.Email : newEmail;
            cliente.Telefone = string.IsNullOrEmpty(newTelefone) ? cliente.Telefone : newTelefone;
            cliente.Endereco = string.IsNullOrEmpty(newEndereco) ? cliente.Endereco : newEndereco;
            cliente.Idade = ((newIdade.HasValue) && (newIdade > 0)) ? newIdade.Value : cliente.Idade;

            var result = await _clienteService.UpdateCliente(cliente);
            var mensagem_resultado = result ? "atualizado com sucesso" : "não atualizado";
            Console.WriteLine($"Cliente {mensagem_resultado}");

        }

        async static Task RemoveCliente()
        {
            Console.WriteLine("======== Remover Cliente ========");
            Console.WriteLine("Informe o cpf:");
            var cpf = LerString();
            var cliente = await _clienteService.Get(cpf);
            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado!");
                return;
            }
            Console.WriteLine($"Deseja realmente excluir o cliente {cliente.Nome} - {cliente.Cpf} ?(S-Sim/N-Não)");
            var opcao = string.Empty;
            while (true)
            {
                opcao = LerString();
                if (opcao.ToUpper().Equals("S") || opcao.ToUpper().Equals("N")) { break; }
            }
            if (opcao.ToUpper().Equals("S"))
            {
                var result = await _clienteService.RemoveCliente(cliente);
                var mensagem_resultado = result ? "removido com sucesso" : "não removido";
                Console.WriteLine($"Cliente {mensagem_resultado}");
            }

        }

        async static Task InsereCliente()
        {
            Console.WriteLine("======== Inserir Cliente ========");
            //nome, idade, CPF, e-mail, telefone e endereço
            Console.WriteLine("Informe o cpf:");
            var cpf = LerString();
            Console.WriteLine("Informe o nome:");
            var nome = LerString();
            Console.WriteLine("Informe a idade:");
            var idade = LerInt();
            Console.WriteLine("Informe o e-mail:");
            var email = LerString();
            Console.WriteLine("Informe o telefone:");
            var telefone = LerString();
            Console.WriteLine("Informe o endereço:");
            var endereco = LerString();
            var result = await _clienteService.InsereCliente(new Cliente(nome, idade.GetValueOrDefault(), cpf, email, telefone, endereco));
            var mensagem_resultado = result ? "inserido com sucesso" : "não inserido";
            Console.WriteLine($"Cliente {mensagem_resultado}");
        }

         async static Task ReadCliente()
        {
            Console.WriteLine("======== Listar Cliente ========");
            //nome, idade, CPF, e-mail, telefone e endereço
            Console.WriteLine("Informe o cpf:");
            var cpf = LerString(true);
            if (string.IsNullOrEmpty(cpf))
            {
                var result = await _clienteService.ListClientes();
                foreach (var cliente in result)
                {
                    cliente.ToConsole();
                }
                Console.WriteLine("================");
            }
            else
            {
                var result = await _clienteService.Get(cpf);
                result.ToConsole();
            }
        }
    }
}
