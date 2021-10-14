using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    public class CedenteLocal : Usuario
    {
        public int IDCedente { get; set; }

        [Display (Name = "Nome")]
        public string nome { get; set; }

        [Display(Name = "Telefone")]
        public int telefone { get; set; }

        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        [Display(Name = "Responsável")]
        public string responsavel { get; set; }
    }
}
 