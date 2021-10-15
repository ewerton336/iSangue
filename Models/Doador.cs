using iSangue.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    public class Doador : Usuario
    {
        public int idDoador { get; set; }

        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Display(Name = "Sobrenome")]
        public string sobrenome { get; set; }

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.DateTime)]
        public string dataNasc { get; set; }

        [Display(Name = "Telefone")]
        public int telefone { get; set; }

        [Display(Name = "Cidade de Doação")]
        public string cidadeDoacao { get; set; }

        [Display(Name = "Tipo Sanguíneo")]
        public string tipoSanguineo { get; set; }
    }
}
