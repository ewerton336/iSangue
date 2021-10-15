using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    public class CalendarioEvento
    {
        public int id { get; set; }

        [Display(Name = "Nome do Evento")]
        [DataType(DataType.Text)]
        public string nomeEvento { get; set; }

        [Display(Name = "Data do Evento")]
        [DataType(DataType.DateTime)]
        public DateTime dataEvento { get; set; }

        [Display(Name = "Quantidade de interessados")]
        public int quantidadeInteressados { get; set; }

        [Display(Name = "Endereço do evento")]
        public string enderecoLocalColeta { get; set; }

        [Display(Name = "Numero do endereço")]
        public int numeroEnderecoLocalColeta { get; set; }

        [Display(Name = "Entidade Coletora")]
        public string entidadeColetora { get; set; }
    }
}
