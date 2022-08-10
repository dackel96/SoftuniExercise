namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            var output = new StringBuilder();

            var playXmlInsert = XmlConverter.Deserializer<PlayXmlImportModel>(xmlString, "Plays");

            var validPlays = new List<Play>();

            foreach (var currPlay in playXmlInsert)
            {
                TimeSpan duration = TimeSpan.ParseExact(currPlay.Duration, "c", CultureInfo.InvariantCulture);
                if (!IsValid(currPlay) ||
                    duration.Hours < 1 ||
                    !Enum.TryParse(typeof(Genre), currPlay.Genre, out var genre))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var validPlay = new Play
                {
                    Title = currPlay.Title,
                    Duration = duration,
                    Rating = currPlay.Rating,
                    Genre = (Genre)genre,
                    Description = currPlay.Description,
                    Screenwriter = currPlay.Screenwriter
                };

                validPlays.Add(validPlay);

                output.AppendLine(String.Format(SuccessfulImportPlay, validPlay.Title, validPlay.Genre.ToString(), validPlay.Rating));

            }
            context.AddRange(validPlays);

            context.SaveChanges();

            return output.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            var output = new StringBuilder();

            var castXmlInsert = XmlConverter.Deserializer<CastXmlImportModel>(xmlString, "Casts");

            var validCasts = new List<Cast>();

            foreach (var currCast in castXmlInsert)
            {
                if (!IsValid(currCast))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var validCast = new Cast
                {
                    FullName = currCast.FullName,
                    IsMainCharacter = currCast.IsMainCharacter,
                    PhoneNumber = currCast.PhoneNumber,
                    PlayId = currCast.PlayId
                };
                validCasts.Add(validCast);
                output.AppendLine(String.Format(SuccessfulImportActor, validCast.FullName, validCast.IsMainCharacter ? "main" : "lesser"));

            }
            context.AddRange(validCasts);
            context.SaveChanges();
            return output.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            var output = new StringBuilder();

            var theatersJsonInsert = JsonConvert.DeserializeObject<IEnumerable<TheatresTicketsJsonImportModel>>(jsonString);

            var validTheaters = new List<Theatre>();

            foreach (var currTheater in theatersJsonInsert)
            {
                if (!IsValid(currTheater))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var validTickets = new List<Ticket>();

                foreach (var ticket in currTheater.Tickets)
                {
                    if (!IsValid(ticket))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }
                    var validTicket = new Ticket
                    {
                        Price = ticket.Price,
                        RowNumber = ticket.RowNumber,
                        PlayId = ticket.PlayId
                    };

                    validTickets.Add(validTicket);

                }

                var validTheater = new Theatre
                {
                    Name = currTheater.Name,
                    NumberOfHalls = currTheater.NumberOfHalls,
                    Director = currTheater.Director,
                    Tickets = validTickets
                };
                validTheaters.Add(validTheater);
                output.AppendLine(String.Format(SuccessfulImportTheatre, validTheater.Name, validTheater.Tickets.Count()));

            }
            context.AddRange(validTheaters);
            context.SaveChanges();
            return output.ToString().TrimEnd();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
