using LM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LM.Persistence.DataInitializer.Mapping
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book));

            builder.HasIndex(x => x.ISBN).IsUnique();
            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<Book> builder)
        {
            var countries = new List<Book>
            {
                new Book
                {
                    BookGenresId = Guid.Parse("d6b44d21-d94b-4716-acaa-f26a6328fa0b"),
                    ID = Guid.Parse("5ee5841676d44bad852edc2ed486e482"),
                    ISBN = "978-3-16-148410-0",
                    Title = "One Hundred Years of Solitude",
                    Author = " Gabriel Garcia Marquez ",
                    Description = "One Hundred Years of Solitude is the first piece of literature since the Book of Genesis that should be required reading for the entire human race. . . . Mr. Garcia Marquez has done nothing less than to create in the reader a sense of all that is profound, meaningful, and meaningless in life.",
                    Quantity = 4,
                    CreationTime = DateTime.Now,
                    CreatorUserId = "473c9df0-2634-4506-ad6c-9b98336082f2"
                },

                new Book
                {
                    BookGenresId = Guid.Parse("8bea3613-18a2-4bd6-a8a3-6e34ce4881f3"),
                    ID = Guid.Parse("7969a9ac-c081-4b75-a7fc-51e64c79c8dc"),
                    ISBN = "978-3-16-148410-1",
                    Title = "Watchmen ",
                    Author = "Alan Moore",
                    Description = "A hit HBO original series, Watchmen, the groundbreaking series from award-winning author Alan Moore, presents a world where the mere presence of American superheroes changed history--the U.S. won the Vietnam War, Nixon is still president, and the Cold War is in full effect.",
                    Quantity = 2,
                    CreationTime = DateTime.Now,
                    CreatorUserId = "473c9df0-2634-4506-ad6c-9b98336082f2"
                },

                new Book
                {
                    BookGenresId = Guid.Parse("f07f8895-b747-459d-ceb1-85f77c5e7d67"),
                    ID = Guid.Parse("13088fc2-c0ee-487e-91a1-2b977214d0a1"),
                    ISBN = "978-3-16-148410-2",
                    Title = "Life Of Pi Paperback",
                    Author = "Alan Moore",
                    Description = "After the sinking of a cargo ship, a solitary lifeboat remains bobbing on the wild blue Pacific. The only survivors from the wreck are a sixteen-year-old boy named Pi, a hyena, a wounded zebra, an orangutan—and a 450-pound Royal Bengal tiger.\r\n\r\nSoon the tiger has dispatched all but Pi Patel, whose fear, knowledge, and cunning allow him to coexist with the tiger, Richard Parker, for 227 days while lost at sea. When they finally reach the coast of Mexico, Richard Parker flees to the jungle, never to be seen again. The Japanese authorities who interrogate Pi refuse to believe his story and press him to tell them \"the truth.\" After hours of coercion, Pi tells a second story, a story much less fantastical, much more conventional—but is it more true?\r\n\r\n\"A story to make you believe in the soul-sustaining power of fiction.\"—Los Angeles Times Book Review\r\n",
                    Quantity = 3,
                    CreationTime = DateTime.Now,
                    CreatorUserId = "473c9df0-2634-4506-ad6c-9b98336082f2"
                }
            };

            builder.HasData(countries);
        }
    }
}
