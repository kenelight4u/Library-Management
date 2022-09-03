﻿using LM.Domain.Entities;
using LM.Domain.Enums;
using LM.DTOs.Response.BookGenresVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response.BookVM
{
    public class BookVM
    {
        public string ISBN { get; set; }

        public string Title { get; set; }
        
        public string Author { get; set; }
        
        public string Description { get; set; }

        public int Quantity { get; set; }

        public BookStatus AvailabilityStatus { get; set; }

        public static implicit operator BookVM(Book model)
        {
            return model == null
                ? null
                : new BookVM
                {
                    Author = model.Author,
                    Title = model.Title,
                    ISBN = model.ISBN,
                    Description = model.Description,
                    Quantity = model.Quantity, 
                    AvailabilityStatus = (model.Quantity > 0) ? BookStatus.InStock : BookStatus.OutOfStock
                };
        }
    }
}