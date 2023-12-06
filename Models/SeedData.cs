using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MvcSongs.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcSongsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcSongsContext>>()))
            {
                // Look for any movies.
                if (context.Songs.Any())
                {
                    return;   // DB has been seeded
                }

                context.Songs.AddRange(
                    new Songs
                    {
                        Title = "Shape of You",
                        SingerName = "Ed Sheeran",
                        ReleaseDate = DateTime.Parse("2017-01-06"),
                        Genre = "Pop",
                        Price = 7.99M,
                        Production = "Atlantic Records",
                        Rating = 4.8M
                    },
new Songs
{
    Title = "Uptown Funk",
    SingerName = "Mark Ronson ft. Bruno Mars",
    ReleaseDate = DateTime.Parse("2014-11-10"),
    Genre = "Funk",
    Price = 8.99M,
    Production = "Columbia Records",
    Rating = 4.2M
},
new Songs
{
    Title = "Someone Like You",
    SingerName = "Adele",
    ReleaseDate = DateTime.Parse("2011-01-24"),
    Genre = "Pop",
    Price = 9.99M,
    Production = "XL Recordings",
    Rating = 4.9M
},
new Songs
{
    Title = "Perfect",
    SingerName = "Ed Sheeran",
    ReleaseDate = DateTime.Parse("2017-09-26"),
    Genre = "Pop",
    Price = 3.99M,
    Production = "Atlantic Records",
    Rating = 4.1M
}

                );
                context.SaveChanges();
            }
        }
    }
}