using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormulaIFS.Model
{
    public class Usuario : Entidade
    {
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        public VinculoUsuario Vinculo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        public string  Login { get; set; }

    }
}