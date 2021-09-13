namespace VideoClubProject1.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VideoClubProject1.Core.Entities;
    using VideoClubProject1.Core.Enumerations;

    internal sealed class Configuration : DbMigrationsConfiguration<VideoClubProject1.Infrastructure.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VideoClubProject1.Infrastructure.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Movies.AddOrUpdate(x => x.Id,
                new Movie() { Id = 1, Title = "Revenant", Description = "None", Type = MovieType.Action },
                new Movie() { Id = 2, Title = "The Game", Description = "None", Type = MovieType.Mystery },
                new Movie() { Id = 3, Title = "Prisoners", Description = "Release date: 2013", Type = MovieType.Action },
                new Movie() { Id = 4, Title = "The Invisible Man", Description = "In Progress...", Type = MovieType.Thriller },
                new Movie() { Id = 5, Title = "Godfather", Description = "Part1", Type = MovieType.Drama },
                new Movie() { Id = 6, Title = "Titanic", Description = "Release date: 1997", Type = MovieType.Drama },
                new Movie() { Id = 7, Title = "Hangover", Description = "Release date: 2008", Type = MovieType.Comedy },
                new Movie() { Id = 8, Title = "The Conjuring", Description = "Release on 2013", Type = MovieType.Horror }
                );

            context.PhysicalCopies.AddOrUpdate(x => x.Id,
                new PhysicalCopy() { Id = 1, MovieId = 1, Availability = true },
                new PhysicalCopy() { Id = 2, MovieId = 1, Availability = true },
                new PhysicalCopy() { Id = 3, MovieId = 1, Availability = false },

                new PhysicalCopy() { Id = 4, MovieId = 2, Availability = true },
                new PhysicalCopy() { Id = 5, MovieId = 2, Availability = false },
                new PhysicalCopy() { Id = 6, MovieId = 2, Availability = false },

                new PhysicalCopy() { Id = 7, MovieId = 3, Availability = true },
                new PhysicalCopy() { Id = 8, MovieId = 3, Availability = true },
                new PhysicalCopy() { Id = 9, MovieId = 3, Availability = true },

                new PhysicalCopy() { Id = 10, MovieId = 4, Availability = true },
                new PhysicalCopy() { Id = 11, MovieId = 4, Availability = true },
                new PhysicalCopy() { Id = 12, MovieId = 4, Availability = false },

                new PhysicalCopy() { Id = 13, MovieId = 5, Availability = true },
                new PhysicalCopy() { Id = 14, MovieId = 5, Availability = false },
                new PhysicalCopy() { Id = 15, MovieId = 5, Availability = false },

                new PhysicalCopy() { Id = 16, MovieId = 6, Availability = true },
                new PhysicalCopy() { Id = 17, MovieId = 6, Availability = true },
                new PhysicalCopy() { Id = 18, MovieId = 6, Availability = true },

                new PhysicalCopy() { Id = 19, MovieId = 7, Availability = true },
                new PhysicalCopy() { Id = 20, MovieId = 7, Availability = false },
                new PhysicalCopy() { Id = 21, MovieId = 7, Availability = true },

                new PhysicalCopy() { Id = 22, MovieId = 8, Availability = true },
                new PhysicalCopy() { Id = 23, MovieId = 8, Availability = true },
                new PhysicalCopy() { Id = 24, MovieId = 8, Availability = false }
            );

            context.Histories.AddOrUpdate(x => x.Id,
                 new History()
                 {
                     Id = 1,
                     RentalDate = DateTime.Now,
                     ExpectedDate = DateTime.Now.AddDays(7),
                     ReturnDate = ReturnType.InProgess,
                     PhysicalCopyId = 3,
                     MovieId = context.Movies.FirstOrDefault(r => r.Id == 1),
                     UserId = context.Users.FirstOrDefault(r => r.UserName == "BWade"),
                     RenterFullName = "Ben Wade"
                 });
        }
    }
}
