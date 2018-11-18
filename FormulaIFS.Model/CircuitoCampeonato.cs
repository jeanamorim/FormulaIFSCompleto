using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaIFS.Model
{
    public class CircuitoCampeonato: Entidade
    {
        [Required]
        [Range(0, long.MaxValue)]
        public long TempoMaximoParaConclusao { get; set; }
        public Campeonato Campeonato { get; set; }
        public Circuito Circuito { get; set; }
    }
}
