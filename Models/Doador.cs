using iSangue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    public class Doador : Usuario
    {
        public int idDoador { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string dataNasc { get; set; }
        public int telefone { get; set; }
        public string cidadeDoacao { get; set; }
        public string tipoSanguineo { get; set; }
    }
}
