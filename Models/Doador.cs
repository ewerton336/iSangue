using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteFatec.Models
{
    public class Doador : Usuario
    {
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string endereco { get; set; }
        public int numeroResidencia { get; set; }
        public string complemento { get; set; }
        public string cidadeResidencia { get; set; }
        public string estadoResidencia { get; set; }
        public string dataNasc { get; set; }
        public int telefone { get; set; }
        public string cidadeDoacao { get; set; }
        public string tipoSanguineo { get; set; }
    }
}
