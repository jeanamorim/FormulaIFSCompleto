namespace FormulaIFS.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FormulaIFS.Model.FormulaIFSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FormulaIFS.Model.FormulaIFSContext contexto)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            AddUsuarios(contexto);
            AddCategorias(contexto);
            AddEquipe(contexto);
            AddCampeonatos(contexto);
            contexto.SaveChanges();

        }

        private Campeonato campIniciante;
        private Campeonato campProfissional;
        private Campeonato campAmador;

        void AddCampeonatos(FormulaIFSContext contexto)
        {

            campIniciante = new Campeonato
            {
                Categoria = iniciante,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddYears(1),
                Nome = "Campeonato Iniciante",
                EhPublico = false,
                Descricao = "Campeonato Iniciante",
                QtdVencedor = 1,
                TipoCampeonato = TipoCampeonato.Indivudal
            };
            contexto.AdicionarMembroNoCampeonato(usuarios[0].Id, campIniciante.Id);
            contexto.AdicionarMembroNoCampeonato(usuarios[1].Id, campIniciante.Id);
            contexto.AdicionarMembroNoCampeonato(usuarios[2].Id, campIniciante.Id);

            campAmador = new Campeonato
            {
                Categoria = amador,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddYears(1),
                Nome = "Campeonato Amador",
                EhPublico = true,
                Descricao = "Campeonato Amador Por Equipe Publico",
                QtdVencedor = 1,
                TipoCampeonato = TipoCampeonato.Indivudal
            };

            contexto.AdicionarMembroNoCampeonato(usuarios[1].Id, campAmador.Id);
            contexto.AdicionarMembroNoCampeonato(usuarios[2].Id, campAmador.Id);
            contexto.AdicionarMembroNoCampeonato(usuarios[3].Id, campAmador.Id);

            campProfissional = new Campeonato
            {
                Categoria = profissional,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddYears(1),
                Nome = "Campeonato Profissional",
                EhPublico = true,
                Descricao = "Campeonato Profissional Por Equipe P�blico",
                QtdVencedor = 1,
                TipoCampeonato = TipoCampeonato.PorEquipe
            };

            contexto.AdicionarEquipeNoCampeonato(equipes[0].Id, campProfissional.Id);

            contexto.AdicionarMembroNaEquipe(equipes[0].Id, usuarios[1].Id);
            contexto.AdicionarMembroNaEquipe(equipes[0].Id, usuarios[2].Id);

            contexto.AdicionarEquipeNoCampeonato(equipes[1].Id, campProfissional.Id);
            contexto.AdicionarMembroNaEquipe(equipes[1].Id, usuarios[1].Id);
            contexto.AdicionarMembroNaEquipe(equipes[1].Id, usuarios[3].Id);

            contexto.AdicionarEquipeNoCampeonato(equipes[2].Id, campProfissional.Id);

            contexto.AdicionarMembroNaEquipe(equipes[1].Id, usuarios[2].Id);
            contexto.AdicionarMembroNaEquipe(equipes[1].Id, usuarios[3].Id);


            contexto.Campeonatos.AddOrUpdate(new Campeonato[]{
                campIniciante, campAmador, campProfissional
            });
        }
        Categoria iniciante, amador, profissional;
        void AddCategorias(FormulaIFSContext contexto)
        {
            iniciante = new Categoria { Nome = "Iniciante", Descricao = "E obrigado o uso do conjunto de sensores" };
            amador = new Categoria
            {
                Nome = "Amador",
                Descricao = "Pode utilizar um conjunto previamente definido de senores"
            };
            profissional = new Categoria { Nome = "Profissional", Descricao = "Nao ha restricoes quanto ao uso de sensores" };
            contexto.Categorias.AddOrUpdate(new Categoria[] { iniciante, amador, profissional });
        }
        Equipe[] equipes;
        void AddEquipe(FormulaIFSContext contexto)
        {
            equipes = new Equipe[]
                {
                    new Equipe{ Nome = "Equipe Ativa", Sigla = "EQA", Situacao = SituacaoEquipe.Ativa},
                    new Equipe{ Nome = "Equipe Bloqueada", Sigla = "EQBlo", Situacao = SituacaoEquipe.Bloqueada},
                    new Equipe{ Nome = "Equipe Inativa", Sigla = "EQIna", Situacao = SituacaoEquipe.Inativa}
                };
            contexto.Equipes.AddOrUpdate(equipes);
        }
        Usuario[] usuarios;
        void AddUsuarios(FormulaIFSContext contexto)
        {
            usuarios =
                new Usuario[]{
                    new Usuario { Email = "u1@email.com", Nome = "Anonimo", Login = "anonimo", Vinculo = VinculoUsuario.Anonimo },
                    new Usuario { Email = "Discente@email.com", Nome = "Discente", Login = "discente", Vinculo = VinculoUsuario.Discente },
                    new Usuario { Email = "Docente@email.com", Nome = "Docente", Login = "docente", Vinculo = VinculoUsuario.Docente },
                    new Usuario { Email = "admin@email.com", Nome = "Adminstarador", Login = "admin", Vinculo = VinculoUsuario.Adminstarador }
                };
            contexto.Usuarios.AddOrUpdate(usuarios);
        }
    }
}