using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FormulaIFS.Model
{
    public class Equipe : Entidade
    {
        public Carro Carro { get; set; }
        public IReadOnlyList<UsuarioEquipe> Membros { get; set; }

        [Required]
        [MaxLength(10)]
        public String Sigla { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        public SituacaoEquipe Situacao { get; set; }
        public String Imagem { get; set; }
        public Equipe() : base()
        {
            Situacao = SituacaoEquipe.Ativa;
            Membros = new List<UsuarioEquipe>();
            Imagem = "~/Content/Imagens/default.png";
        }
        [NotMapped]
        public HttpPostedFileBase ImagemUpload { get; set; }
    }
}
