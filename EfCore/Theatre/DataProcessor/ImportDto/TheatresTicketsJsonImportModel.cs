using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theatre.DataProcessor.ImportDto
{
    public class TheatresTicketsJsonImportModel
    {
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }
        [Range(1, 10)]
        public sbyte NumberOfHalls { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Director { get; set; }

        public IEnumerable<TicketJsonInputModel> Tickets { get; set; }
    }

    public class TicketJsonInputModel
    {
        [Range(1.00, 100.00)]
        public decimal Price { get; set; }
        [Range(1, 10)]
        public sbyte RowNumber { get; set; }

        public int PlayId { get; set; }
    }
}


/* {
    "Name": "",
    "NumberOfHalls": 7,
    "Director": "Ulwin Mabosty",
    "Tickets": [
      {
        "Price": 7.63,
        "RowNumber": 5,
        "PlayId": 4
      },
      {
        "Price": 47.96,
        "RowNumber": 9,
        "PlayId": 3
      }
    ]
  },
*/