using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace FormulaIFS.Model
{
    public class FormulaIFSContext : DbContext
    {
        public FormulaIFSContext()
            : base("BancoDados") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var initializer = new CreateDatabaseIfNotExists<FormulaIFSContext>();
            Database.SetInitializer(initializer);

        }

        public DbSet<Campeonato> Campeonatos { get; set; }
        public DbSet<MembroCampeonato> MembrosCampeonatos { get; set; }
        public DbSet<EquipeCampeonato> EquipesCampeonatos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carro> Carros { get; set; }

        public DbSet<CircuitoCampeonatoLabirinto> CircuitosDoCampeonatoTipoLabirinto { get; set; }
        public DbSet<CircuitoCampeonatoVolta> CircuitosDoCampeonatoTipoVolta { get; set; }

        public DbSet<RegistroDisputa> RegistroDisputa { get; set; }

        protected DbSet<UsuarioEquipe> UsuariosEquipes { get; set; }
        public void AdicionarMembroNaEquipe(int equipeId, int usuarioId)
        {
            var equipe = Equipes.Where(p => p.Id == equipeId).First();
            var usuario = Usuarios.Where(p => p.Id == usuarioId).First();

            if (equipe.Situacao == SituacaoEquipe.Bloqueada)
            {
                throw new Exception("A equipe está bloqueada para ajustes");
            }
            var usuarioEquipe = new UsuarioEquipe();
            usuarioEquipe.Equipe = equipe;
            usuarioEquipe.Usuario = usuario;
            UsuariosEquipes.Add(usuarioEquipe);
        }
        public int RemoverMembroDaEquipe(int equipeId, int usuarioId)
        {
            var equipe = Equipes.Where(p => p.Id == equipeId).First();

            if (equipe.Situacao == SituacaoEquipe.Bloqueada)
            {
                throw new Exception("A equipe está bloqueada para ajustes");
            }
            var dados = UsuariosEquipes
                .Include(u => u.Equipe)
                .Include(u => u.Usuario)
                .Where(p => p.Equipe.Id == equipeId && p.Usuario.Id == usuarioId).ToList();
            UsuariosEquipes.RemoveRange(dados);
            return dados.Count;
        }

        public void AdicionarMembroNoCampeonato(int membroId, int campeonatoId)
        {
            var campeonato = Campeonatos.Where(p => p.Id == campeonatoId).First();
            if (campeonato.TipoCampeonato == TipoCampeonato.PorEquipe)
            {
                throw new Exception("A definição do campeonato só permite cadastro de equipes");
            }

            if (campeonato.SituacaoCampeonato != SituacaoCampeonato.NaoInicializado)
            {
                throw new Exception("O campeonato está bloqueada para ajustes");
            }

            var usuario = Usuarios.Where(p => p.Id == membroId).First();
            var membroCamp = new MembroCampeonato();
            membroCamp.Usuario = usuario;
            membroCamp.Campeonato = campeonato;
            MembrosCampeonatos.Add(membroCamp);
        }
        public int RemoverMembroDoCampeonato(int membroId, int campeonatoId)
        {
            var campeonato = Campeonatos.Where(p => p.Id == campeonatoId).First();

            if (campeonato.SituacaoCampeonato != SituacaoCampeonato.NaoInicializado)
            {
                throw new Exception("O campeonato está bloqueada para ajustes");
            }
            var dados = MembrosCampeonatos
                .Include(c => c.Campeonato)
                .Include(u => u.Usuario)
                .Where(p => p.Campeonato.Id == campeonatoId && p.Usuario.Id == membroId).ToList();
            MembrosCampeonatos.RemoveRange(dados);
            return dados.Count;
        }

        public void AdicionarEquipeNoCampeonato(int equipeId, int campeonatoId)
        {
            var equipe = Equipes.Where(p => p.Id == equipeId).First();
            var campeonato = Campeonatos.Where(p => p.Id == campeonatoId).First();
            if (campeonato.TipoCampeonato == TipoCampeonato.Indivudal)
            {
                throw new Exception("A definição do campeonato só permite cadastro de membros");
            }

            if (campeonato.SituacaoCampeonato != SituacaoCampeonato.NaoInicializado)
            {
                throw new Exception("O campeonato está bloqueada para ajustes");

            }
            var equipeCamp = new EquipeCampeonato();
            equipeCamp.Equipe = equipe;
            equipeCamp.Campeonato = campeonato;
            EquipesCampeonatos.Add(equipeCamp);
        }

        public int RemoverEquipeDoCampeonato(int equipeId, int campeonatoId)
        {
            var equipe = Equipes.Where(p => p.Id == equipeId).First();

            if (equipe.Situacao == SituacaoEquipe.Bloqueada)
            {
                throw new Exception("O campeonato está bloqueada para ajustes");
            }
            var dados = EquipesCampeonatos
                .Include(u => u.Equipe)
                .Include(u => u.Campeonato)
                .Where(p => p.Equipe.Id == equipeId && p.Campeonato.Id == campeonatoId).ToList();
            EquipesCampeonatos.RemoveRange(dados);
            return dados.Count;
        }

        public System.Data.Entity.DbSet<FormulaIFS.Model.Circuito> Circuitos { get; set; }
    }
}
