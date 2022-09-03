using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LM.Domain.Entities.Mapping
{
    public class BookGenreMap : IEntityTypeConfiguration<BookGenres>
    {
        public void Configure(EntityTypeBuilder<BookGenres> builder)
        {
            builder.ToTable(nameof(BookGenres));

            // builder.HasIndex(x => x.Name).IsUnique();
            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<BookGenres> builder)
        {
            var countries = new List<BookGenres>
            {
                new BookGenres
                {
                    ID = Guid.Parse("d6b44d21-d94b-4716-acaa-f26a6328fa0b"),
                    Name = "Historical Fiction",
                    Description = "These books are based in a time period set in the past decades, often against the backdrop of significant (real) historical events.",
                    CreationTime = DateTime.Now,
                    CreatorUserId = "473c9df0-2634-4506-ad6c-9b98336082f2"
                },

                new BookGenres
                {
                    ID = Guid.Parse("8bea3613-18a2-4bd6-a8a3-6e34ce4881f3"),
                    Name = "Detective and Mystery",
                    Description = "The plot always revolves around a crime of sorts that must be solved—or foiled—by the protagonists.",
                    CreationTime = DateTime.Now,
                    CreatorUserId = "473c9df0-2634-4506-ad6c-9b98336082f2"
                },

                new BookGenres
                {
                    ID = Guid.Parse("f07f8895-b747-459d-ceb1-85f77c5e7d67"),
                    Name = "Comic Book or Graphic Novel",
                    Description = "The stories in comic books and graphic novels are presented to the reader through engaging, sequential narrative art (illustrations and typography) that's either presented in a specific design or the traditional panel layout you find in comics. With both, you'll often find the dialogue presented in the tell-tale \"word balloons\" next to the respective characters.",
                    CreationTime = DateTime.Now,
                    CreatorUserId = "473c9df0-2634-4506-ad6c-9b98336082f2"
                }
            };

            builder.HasData(countries);
        }
    }
}
