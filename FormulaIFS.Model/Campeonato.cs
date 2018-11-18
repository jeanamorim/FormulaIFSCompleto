using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormulaIFS.Model
{
    public class Campeonato : Entidade
    {
        [Required]
        [MaxLength(50)]
        public String Nome { get; set; }
        [Required]
        [MaxLength(500)]
        public String Descricao { get; set; }
        [Required]
        public TipoCampeonato TipoCampeonato { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int QtdVencedor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool EhPublico { get; set; }
        public List<Circuito> Circuitos { get; set; }
        public IReadOnlyList<MembroCampeonato> Membros{ get; set; }
        public List<EquipeCampeonato> Equipes{ get; set; }
        public Categoria Categoria { get; set; }

        public SituacaoCampeonato SituacaoCampeonato { get; set; }
        public Campeonato()
        {
            SituacaoCampeonato = SituacaoCampeonato.NaoInicializado;
        }

    }
}
