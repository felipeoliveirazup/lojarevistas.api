using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CadastroCliente.Domain.Entidades
{
    public class Cliente
    {
        public Cliente(string nome, int idade, string cpf, string email, string telefone, string endereco)
        {
            this.Nome = nome;
            this.Idade = idade;
            this.Cpf = cpf;
            this.Email = email;
            this.Telefone = telefone;
            this.Endereco = endereco;
        }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public void ToConsole()
        {
            Console.WriteLine($"{Nome} - {Cpf} - {Idade} - {Email} - {Telefone} - {Endereco}");
        }
    }
}
