namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            var result = ExportSongsAboveDuration(context, 4);
            Console.WriteLine(result);
            //Test your solutions here
        }
        // You need to write method string ExportAlbumsInfo(MusicHubDbContext context,
        // int producerId) in the StartUp class that receives a Producer Id.
        // Export all albums which are produced by the provided Producer Id.
        // For each Album, get the Name, Release date in format the "MM/dd/yyyy",
        // Producer Name, the Album Songs with each Song Name,
        // Price (formatted to the second digit) and the Song Writer Name.
        // Sort the Songs by Song Name(descending) and by Writer(ascending).
        // At the end export the Total Album Price with exactly two digits after the decimal place.
        // Sort the Albums by their Total Price (descending).

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumInfo = context.Producers
                .FirstOrDefault(x => x.Id == producerId)
                .Albums
                .Select(album => new
                {
                    album.Name,
                    album.ReleaseDate,
                    ProducerName = album.Producer.Name,
                    Songs = album.Songs
                    .Select(song => new
                    {
                        SongName = song.Name,
                        song.Price,
                        Writer = song.Writer.Name
                    })
                    .OrderByDescending(x => x.SongName)
                    .ThenBy(x => x.Writer),
                    AlbumPrice = album.Price
                })
                .OrderByDescending(x => x.AlbumPrice)
                .ToList();
            StringBuilder result = new StringBuilder();
            foreach (var album in albumInfo)
            {
                result.AppendLine($"-AlbumName: {album.Name}");
                result.AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}");
                result.AppendLine($"-ProducerName: {album.ProducerName}");
                result.AppendLine($"-Songs:");
                int counter = 1;
                foreach (var song in album.Songs)
                {
                    result.AppendLine($"---#{counter++}");
                    result.AppendLine($"---SongName: {song.SongName}");
                    result.AppendLine($"---Price: {song.Price:f2}");
                    result.AppendLine($"---Writer: {song.Writer}");
                }
                result.AppendLine($"-AlbumPrice: {album.AlbumPrice:f2}");
            }
            return result.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            /*export its Name, Performer Full Name, Writer Name, Album Producer and Duration (in format("c")).
             * Sort the Songs by their Name (ascending), by Writer (ascending) and by Performer (ascending).*/
            var songs = context.Songs
                .ToList()
                .Where(x => x.Duration.TotalSeconds > duration)
                .Select(song => new
                {
                    SongName = song.Name,
                    Performer = song.SongPerformers
                    .Select(x => x.Performer.FirstName + " " + x.Performer.LastName)
                    .FirstOrDefault(),
                    Writer = song.Writer.Name,
                    Producer = song.Album.Producer.Name,
                    song.Duration
                })
                .OrderBy(x => x.SongName)
                .ThenBy(x => x.Writer)
                .ThenBy(x => x.Performer);
            StringBuilder result = new StringBuilder();
            int counter = 1;
            foreach (var song in songs)
            {
                /*-Song #1
                ---SongName: Away
                ---Writer: Norina Renihan
                ---Performer: Lula Zuan
                ---AlbumProducer: Georgi Milkov
                ---Duration: 00:05:35 */
                result.AppendLine($"-Song #{counter++}");
                result.AppendLine($"---SongName: {song.SongName}");
                result.AppendLine($"---Writer: {song.Writer}");
                result.AppendLine($"---Performer: {song.Performer}");
                result.AppendLine($"---AlbumProducer: {song.Producer}");
                result.AppendLine($"---Duration: {song.Duration.ToString("c")}");
            }
            return result.ToString().TrimEnd();
        }
    }
}
