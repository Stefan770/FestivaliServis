namespace FestivaliServis.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FestivaliServis.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FestivaliServis.Models.ApplicationDbContext context)
        {
            context.Mesta.AddOrUpdate(x => x.Id,
                new Models.Mesto { Id = 1, Naziv = "Novi Sad", Zip = 21000 },
                new Models.Mesto { Id = 2, Naziv = "Beograd", Zip = 11000 },
                new Models.Mesto { Id = 3, Naziv = "Zajecar", Zip = 18000 }
                );

            context.Festivali.AddOrUpdate(x => x.Id,
                new Models.Festival { Id = 1, Naziv = "Exit", Godina = 2000, Cena = 12000, MestoId = 1 },
                new Models.Festival { Id = 2, Naziv = "Beer Fest", Godina = 2005, Cena = 2000, MestoId = 2 },
                new Models.Festival { Id = 3, Naziv = "Zajecarska Gitarijada", Godina = 2007, Cena = 3400, MestoId = 3 }
                );
        }
    }
}
