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
        public string email { get; set; }
        public string senha { get; set; }
        public string tipoUsuario { get; set; }
    }
}
