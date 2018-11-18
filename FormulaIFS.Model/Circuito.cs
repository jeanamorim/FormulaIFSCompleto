using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace FormulaIFS.Model
{
    public class Circuito : Entidade
    {
        [Required]
        [MaxLength(50)]
        public String Nome { get; set; }
        [Required]
        [MaxLength(5)]
        public String Sigla { get; set; }
        public int Tamanho { get; set; }
        [Range(1, 10)]

        public int Complexidade { get; set; }

        public String Imagem { get; set; }

        public List<Campeonato> Campeonatos { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImagemUpload { get; set; }

        public Circuito()
        {
            Imagem = "~/Content/Imagens/default.png";
        }

    }
}
