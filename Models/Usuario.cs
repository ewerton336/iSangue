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
        [Display(Name = "ID de usuário")]
        public int id { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        [Display(Name = "Tipo de Usuário")]
        public string tipoUsuario { get; set; }
    }
}
