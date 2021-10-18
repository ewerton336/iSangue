using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    [Table ("calendario_evento")]
    public class CalendarioEvento
    {
        [Required, Key]
        public int id { get; set; }

        [Column("nm_evento")]
        [Display(Name = "Nome do Evento")]
        [DataType(DataType.Text)]
        public string nomeEvento { get; set; }

        [Column ("dt_evento")]
        [Display(Name = "Data do Evento")]
        [DataType(DataType.DateTime)]
        public DateTime dataEvento { get; set; }

        [Column("qt_interessado")]
        [Display(Name = "Quantidade de interessados")]
        public int quantidadeInteressados { get; set; }

        [Column("endereco_evento")]
        [Display(Name = "Endereço do evento")]
        public string enderecoLocalColeta { get; set; }

        [Column("num_endereco_evento")]
        [Display(Name = "Numero do endereço")]
        public int numeroEnderecoLocalColeta { get; set; }

        [Column("entidade_coletora")]
        [Display(Name = "Entidade Coletora")]
        public string entidadeColetora { get; set; }
    }
}
