using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace FormaIFSForWeb
{
    public class Contexto : DbContext
    {
        public Contexto()
            : base("Contexto") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<Contexto>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
