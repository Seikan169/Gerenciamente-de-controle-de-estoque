using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamente_de_controle_de_estoque
{
    internal class Produto
    {
        public string nome { get; set; }
        public int quantidade { get; set; }
        public decimal valor { get; set; }
        public  DateTime data_venc {  get; set; }
        
    }
}
