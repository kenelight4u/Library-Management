using LM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LM.Persistence.DataInitializer.Mapping
{
    public class BookInventoryMap : IEntityTypeConfiguration<BookInventory>
    {
        public void Configure(EntityTypeBuilder<BookInventory> builder)
        {
            builder.ToTable(nameof(BookInventory));

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<BookInventory> builder)
        {
            var countries = new List<BookInventory>
            {
                new BookInventory
                {
                    ID = Guid.Parse("8ec05cfa-11e5-4f32-b732-dbd3a953b20b"),
                    BookId = Guid.Parse("5ee5841676d44bad852edc2ed486e482"),
                    Quantity = 4,
                    CreationTime = DateTime.Now,
                    CreatorUserId = "473c9df0-2634-4506-ad6c-9b98336082f2"
                },

                new BookInventory
                {
                    ID = Guid.Parse("536a1a94-c132-4163-bee5-2d0e1986882a"),
                    BookId = Guid.Parse("7969a9ac-c081-4b75-a7fc-51e64c79c8dc"),
                    Quantity = 2,
                    CreationTime = DateTime.Now,
                    CreatorUserId = "473c9df0-2634-4506-ad6c-9b98336082f2"
                },

                new BookInventory
                {
                    ID = Guid.Parse("13b5ce77-b382-45b6-bff4-300bb8b93388"),
                    BookId= Guid.Parse("13088fc2-c0ee-487e-91a1-2b977214d0a1"),
                    Quantity = 3,
                    CreationTime = DateTime.Now,
                    CreatorUserId = "473c9df0-2634-4506-ad6c-9b98336082f2"
                }
            };

            builder.HasData(countries);
        }
    }
}
