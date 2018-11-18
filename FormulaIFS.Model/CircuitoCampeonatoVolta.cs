using System.ComponentModel.DataAnnotations;

namespace FormulaIFS.Model
{
    public class CircuitoCampeonatoVolta: CircuitoCampeonato
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int NumeroVoltasTreino { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int NumeroVoltasDisputa { get; set; }
    }
}