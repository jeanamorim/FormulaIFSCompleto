using System;

namespace FormulaIFS.Model
{
    public class UsuarioEquipe: Entidade
    {
        public UsuarioEquipe() : base()
        {
            DataEntrada = DateTime.Now;
        }
        public Usuario Usuario { get; set; }
        public Equipe Equipe { get; set; }
        public DateTime DataEntrada { get; protected set; }
    }
}