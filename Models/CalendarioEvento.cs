using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{

    public class CalendarioEvento
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nome do Evento")]
        [DataType(DataType.Text)]
        public string nomeEvento { get; set; }

        [Display(Name = "Data do Evento")]
        [DataType(DataType.DateTime)]
        public DateTime dataEvento { get; set; }

        [Display(Name = "Quantidade de interessados")]
        public int quantidadeInteressados { get; set; }

        [Display(Name = "ID Entidade Coletora")]
        public int entidadeColetoraID { get; set; }

        [Display(Name = "ID Cedente Local")]
        public int cedenteLocalID { get; set; }

        [Display(Name = "Entidade Coletora")]
       public EntidadeColetora entidadeColetora { get; set; }

        [Display(Name = "Local")]
        public CedenteLocal cedenteLocal { get; set; }

    }
}
