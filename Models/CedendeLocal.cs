using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteFatec.Models
{
    public class CedenteLocal : Usuario
    {
        public string nome { get; set; }
        public int telefone { get; set; }
        public string endereco { get; set; }
        public string responsavel { get; set; }
    }
}
