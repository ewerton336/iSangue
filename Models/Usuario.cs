using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "Senha")]
        public string senha { get; set; }

        [Display(Name = "Tipo de Usuário")]
        public string tipoUsuario { get; set; }
    }
}
