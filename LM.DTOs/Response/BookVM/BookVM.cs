using LM.Domain.Entities;
using LM.Domain.Enums;
using LM.Domain.Enums.EnumExtension;
using LM.DTOs.Response.BookGenresVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response.BookVM
{
    /// <summary>
    /// Class of BookVM
    /// </summary>
    public class BookVM
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public Guid ID { get; set; }
        /// <summary>
        /// Gets or sets the ISBN.
        /// </summary>
        /// <value>The ISBN.</value>
        public string ISBN { get; set; }
        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        /// <value>The Title.</value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the Author
        /// </summary>
        /// <value>The Author.</value>
        public string Author { get; set; }
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }
        // <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the Available Status.
        /// </summary>
        /// <value>The Available Status.</value>
        public string AvailabilityStatus { get; set; }

        /// <summary>
        /// Converts Book to BookVM
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BookVM.</returns>
        public static implicit operator BookVM(Book model)
        {
            return model == null
                ? null
                : new BookVM
                {
                    ID = model.ID,
                    Author = model.Author,
                    Title = model.Title,
                    ISBN = model.ISBN,
                    Description = model.Description,
                    Quantity = model.Quantity, 
                    AvailabilityStatus = (model.Quantity > 0) ? BookStatus.InStock.GetDescription() : BookStatus.OutOfStock.GetDescription()
                };
        }
    }
}
