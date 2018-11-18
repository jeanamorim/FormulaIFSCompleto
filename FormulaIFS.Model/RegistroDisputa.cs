using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaIFS.Model
{
    public class RegistroDisputa: Entidade
    {

        [Required]
        public DateTime DataRegistro { get; set; } = DateTime.Now;
        [Required]
        [Index("UK_RegistroDisputa", 1, IsUnique = true)]
        public CircuitoCampeonato CircuitoCampeonato { get; set; }
        [Index("UK_RegistroDisputa", 2, IsUnique = true)]
        public EquipeCampeonato EquipeCampeonato { get; set; }
        [Index("UK_RegistroDisputa", 3, IsUnique = true)]
        public Usuario Usuario { get; set; }
        [Required]
        [Index("UK_RegistroDisputa", 4, IsUnique = true)]
        public bool EhTreino { get; set; } = true;
        [Required]
        [Index("UK_RegistroDisputa", 5, IsUnique = true)]
        public int Sequencial { get; set; }
        [Required]
        public TimeSpan Tempo { get; set; }
    }
}
