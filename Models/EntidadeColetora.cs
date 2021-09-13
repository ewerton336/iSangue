using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteFatec.Models
{
    public class Entidadecoletora : Usuario
    {
        public string nome { get; set; }
        public string enderecoComercial { get; set; } // não sei se será utilizado
        public int telefone { get; set; }
        public string nomeResponsavel { get; set; }
    }
}
