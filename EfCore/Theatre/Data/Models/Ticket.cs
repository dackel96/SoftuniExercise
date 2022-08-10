using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.Data.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public decimal Price { get; set; } //decimal in the range [1.00….100.00] (required)

        public sbyte RowNumber { get; set; } //sbyte in range [1….10] (required)

        public int PlayId { get; set; }
        public Play Play { get; set; }

        public int TheatreId { get; set; }
        public Theatre Theatre { get; set; }
    }
}