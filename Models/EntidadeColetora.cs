using iSangue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    public class Entidadecoletora : Usuario
    {
        public string nome { get; set; }
        public string enderecoComercial { get; set; } // não sei se será utilizado
        public int telefone { get; set; }
        public string nomeResponsavel { get; set; }
    }
}
