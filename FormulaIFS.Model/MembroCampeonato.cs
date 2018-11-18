using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaIFS.Model
{
    
    public class MembroCampeonato: Entidade
    {
        public Campeonato Campeonato { get; set; }
        public Usuario Usuario { get; set; }
    }
}
