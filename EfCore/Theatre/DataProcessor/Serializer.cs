namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            /*Export all theaters where the hall's count is bigger or equal to the given and have 20 or more tickets available.
             * For each theater, export its Name, Halls, TotalIncome of tickets which are between the first and fifth row inclusively,
             * and Tickets. For each ticket (between first and fifth row inclusively), export its price, and the row number.
             * Order the theaters by the number of halls descending, then by name (ascending). Order the tickets by price descending.*/
            var topTheaters = context.Theatres
                .ToList()
                .Where(x => x.NumberOfHalls >= numbersOfHalls && x.Tickets.Count >= 20)
                .Select(x => new TopTheatresViewModel
                {
                    Name = x.Name,
                    Halls = x.NumberOfHalls,
                    TotalIncome = x.Tickets.Where(x => x.RowNumber >= 1 && x.RowNumber <= 5).Sum(x => x.Price),
                    Tickets = x.Tickets
                    .ToList()
                    .Where(x => x.RowNumber >= 1 && x.RowNumber <= 5)
                    .Select(t => new TicketJsonViewModel
                    {
                        Price = t.Price,
                        RowNumber = t.RowNumber
                    })
                    .OrderByDescending(o => o.Price)
                })
                .OrderByDescending(o => o.Halls)
                .ThenBy(o => o.Name);
            var jsonView = JsonConvert.SerializeObject(topTheaters, Formatting.Indented);
            return jsonView;
        }

        public static string ExportPlays(TheatreContext context, double rating)
        {
            /*Export all plays with a rating equal or smaller to the given. For each play, export Title, Duration (in the format: "c"), Rating,
             * Genre, and Actors which play the main character only. 
Keep in mind:
⦁	If the rating is 0, you should print "Premier". 
⦁	For each actor display:
⦁	FullName 
⦁	MainCharacter in the format: "Plays main character in '{playTitle}'."
Order the result by play title (ascending), then by genre (descending). Order actors by their full name descending.
*/
            var playXmlExport = context.Plays
                .ToArray()
                .Where(x => x.Rating <= rating)
                .Select(x => new PlayViewModel
                {
                    Title = x.Title,
                    Duration = x.Duration.ToString("c", CultureInfo.InvariantCulture),
                    Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),
                    Genre = x.Genre.ToString(),
                    Actors = x.Casts.Where(a => a.IsMainCharacter == true).Select(c => new ActorViewXmlModel
                    {
                        FullName = c.FullName,
                        MainCharacter = $"Plays main character in '{x.Title}'."
                    })
                    .OrderByDescending(o => o.FullName)
                    .ToArray()
                })
                .OrderBy(o => o.Title)
                .ThenBy(o => o.Genre)
                .ToArray();

            var xmlView = XmlConverter.Serialize<PlayViewModel[]>(playXmlExport, "Plays");

            return xmlView;
        }
    }
}
