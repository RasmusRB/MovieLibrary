using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Movie
    {
        private int _id;
        private string _title;
        private string _director;
        private int _releaseYear;
        private string _genre;
        private double _imdbRating;
        private int _runTime;
        private string _note;

        public Movie()
        {

        }

        public Movie(int id, string title, string director, int releaseYear, string genre, double imdbRating, int runTime, string note)
        {
            _id = id;
            _title = title;
            _director = director;
            _releaseYear = releaseYear;
            _genre = genre;
            _imdbRating = imdbRating;
            _runTime = runTime;
            _note = note;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Director
        {
            get => _director;
            set => _director = value;
        }

        public int ReleaseYear
        {
            get => _releaseYear;
            set => _releaseYear = value;
        }

        public string Genre
        {
            get => _genre;
            set => _genre = value;
        }

        public double ImdbRating
        {
            get => _imdbRating;
            set => _imdbRating = value;
        }

        public int RunTime
        {
            get => _runTime;
            set => _runTime = value;
        }

        public string Note
        {
            get => _note;
            set => _note = value;
        }
    }
}
