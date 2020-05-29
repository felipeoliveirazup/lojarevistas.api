using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroCliente.Core.Extension
{
    public static class StringExtension
    {
        public static string ShowOnlyEndValue(this string value, int quant)
        {
            var final = value.Substring(value.Length - quant, quant);
            return final.PadLeft((2 * quant), 'X');
        }
    }
}
