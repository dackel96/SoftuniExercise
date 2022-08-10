using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.DataProcessor.ExportDto
{
    public class TopTheatresViewModel
    {
        public string Name { get; set; }

        public sbyte Halls { get; set; }

        public decimal TotalIncome { get; set; }

        public IEnumerable<TicketJsonViewModel> Tickets { get; set; }

    }

    public class TicketJsonViewModel
    {
        public decimal Price { get; set; }

        public sbyte RowNumber { get; set; }
    }
}

/*[
  {
    "Name": "Capitol Theatre Building",
    "Halls": 10,
    "TotalIncome": 860.02,
    "Tickets": [
      {
        "Price": 93.48,
        "RowNumber": 3
      },
      {
        "Price": 93.41,
        "RowNumber": 1
      },
      {
        "Price": 86.21,
*/
