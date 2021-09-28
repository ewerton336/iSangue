using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    public class CalendarioEvento
    {
        public int id { get; set; }
        public string nomeEvento { get; set; }
        public DateTime dataEvento { get; set; }
        public int quantidadeInteressados { get; set; }
        public string enderecoLocalColeta { get; set; }
        public int numeroEnderecoLocalColeta { get; set; }
        public string entidadeColetora { get; set; }
    }
}
