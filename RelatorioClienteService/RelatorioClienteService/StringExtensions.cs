using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioClienteService
{
    public static class StringExtensions
    {
        public static string Centraliza(this string texto, int qtde)
        {
            return $@"{"".PadRight(qtde)}{texto}{"".PadRight(qtde)}";
        }

        public static string AjustaColuna(this string texto, int tamanho)
        {
            if (texto.Length > tamanho)
            {
                texto = texto.Substring(1, tamanho);
            }
            return texto.PadRight(tamanho);
        }
    }
}
