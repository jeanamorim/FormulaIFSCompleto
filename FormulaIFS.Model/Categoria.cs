using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaIFS.Model
{
    public class Categoria: Entidade
    {
        [Required]
        [MaxLength(50)]
        public String Nome { get; set; }
        [Required]
        [MaxLength(500)]
        public String Descricao { get; set; }

        public bool Ativa { get; set; }

        public Categoria()
        {
            Ativa = true;
        }

    }
}
