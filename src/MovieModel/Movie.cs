using System;

namespace MovieModel
{
    public class Movie : IStorageKey
    {
        public int Id { get; set; }

        public string ImdbId { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        public double Popularity { get; set; }

        public string TagLine { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }

        public string OriginalLanguage { get; set; }

        public string OriginalTitle { get; set; }

        public string Title { get; set; }

        public string Overview { get; set; }

        public DateTime? ReleaseDateTime { get; set; }

        public string State { get; set; }

        public string Key { get => Id.ToString(); }
    }
}
