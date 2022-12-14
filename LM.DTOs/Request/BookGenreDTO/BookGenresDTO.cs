using LM.Domain.Common;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;


namespace LM.DTOs.Request.BookGenreDTO
{
    /// <summary>
    /// Classs of BooGenreDTO
    /// </summary>
    public class BookGenresDTO : IRequest<ResultModel<string>>
    {
        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        /// <value>The Name.</value>
        [Required]
        public string Name { get; set; }
        // <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public string Description { get; set; }
    }

    /// <summary>
    /// Class of EditBookGenresDTO
    /// </summary>
    public class EditBookGenresDTO : IRequest<ResultModel<string>>
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public Guid ID { get; set; }
        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }

    }
}
