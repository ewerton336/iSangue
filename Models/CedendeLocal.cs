using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    public class CedenteLocal : Usuario
    {
        public int IDCedente { get; set; }
        public string nome { get; set; }
        public int telefone { get; set; }
        public string endereco { get; set; }
        public string responsavel { get; set; }
    }
}
 