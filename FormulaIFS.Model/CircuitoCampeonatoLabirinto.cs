using System.ComponentModel.DataAnnotations;

namespace FormulaIFS.Model
{
    public class CircuitoCampeonatoLabirinto : CircuitoCampeonato
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TentativasTreino { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int TentativasDisputa { get; set; }
        
    }
}
