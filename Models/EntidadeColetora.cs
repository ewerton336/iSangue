using iSangue.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    public class EntidadeColetora : Usuario
    {
        public int idEntidade { get; set; }

        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Display(Name = "Endereço Comercial")]
        public string enderecoComercial { get; set; } // não sei se será utilizado

        [Display(Name = "Telefone")]
        public int telefone { get; set; }

        [Display(Name = "Nome do Responsável")]
        public string nomeResponsavel { get; set; }
    }
}
