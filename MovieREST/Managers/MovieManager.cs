using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ModelLib.Model;

namespace MovieREST.Managers
{
    public class MovieManager
    {
        private const string connString = "Data Source=movie-db-server.database.windows.net;Initial Catalog=movie-db;User ID=Movieadmin;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string GET_ALL = "Select * from Movie";
        private const string GET_BY_ID = "Select * from Movie WHERE Id = @id";
        private const string DELETE = "DELETE from Movie WHERE Id = @id";
        private const string UPDATE = "Update Movie set Title = @title, Director = @director, ReleaseYear = @releaseYear, Genre = @genre, ImdbRating = @imdbRating, Runtime = @runTime, Note = @note where Id = @id";
        private const string CREATE = "Insert into Movie (Title, Director, ReleaseYear, Genre, ImdbRating, Runtime, Note) values (@title, @director, @releaseYear, @genre, @imdbRating, @runTime, @note)";
        private const string SORT_BY_RATING = "Select * from Movie WHERE ImdbRating >= @number";

        public IList<Movie> GetAll()
        {
            List<Movie> mList = new List<Movie>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            mList.Add(ReadNextMovie(reader));
                        }
                    }
                }
            }

            return mList;
        }

        public Movie GetMovieFromId(int id)
        {
            Movie movieId = new Movie();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_BY_ID, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        movieId = ReadNextMovie(reader);
                    }
                }
            }
            return movieId;
        }

        public List<Movie> GetMovieByRating(double rating)
        {
            List<Movie> movieRating = new List<Movie>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SORT_BY_RATING, conn))
                {
                    cmd.Parameters.AddWithValue("@number", rating);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        movieRating.Add(ReadNextMovie(reader)); 
                    }
                }
            }
            return movieRating;
        }

        public bool CreateMovie(Movie movie)
        {
            bool created = false;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(CREATE, conn))
                {
                    cmd.Parameters.AddWithValue("@title", movie.Title);
                    cmd.Parameters.AddWithValue("@director", movie.Director);
                    cmd.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear);
                    cmd.Parameters.AddWithValue("@genre", movie.Genre);
                    cmd.Parameters.AddWithValue("@imdbRating", movie.ImdbRating);
                    cmd.Parameters.AddWithValue("@runTime", movie.RunTime);
                    cmd.Parameters.AddWithValue("@note", movie.Note);

                    int rows = cmd.ExecuteNonQuery();
                    created = rows == 1;
                }
            }

            return created;
        }

        public bool UpdateMovie(Movie movie, int id)
        {
            bool updated = false;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(UPDATE, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@title", movie.Title);
                    cmd.Parameters.AddWithValue("@director", movie.Director);
                    cmd.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear);
                    cmd.Parameters.AddWithValue("@genre", movie.Genre);
                    cmd.Parameters.AddWithValue("@imdbRating", movie.ImdbRating);
                    cmd.Parameters.AddWithValue("@runTime", movie.RunTime);
                    cmd.Parameters.AddWithValue("@note", movie.Note);

                    int rows = cmd.ExecuteNonQuery();
                    updated = rows == 1;
                }
            }

            return updated;
        }

        public Movie DeleteMovie(int id)
        {
            Movie movie = GetMovieFromId(id);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(DELETE, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            return movie;
        }

        private Movie ReadNextMovie(SqlDataReader reader)
        {
            Movie movie = new Movie();

            movie.Id = reader.GetInt32(0);
            movie.Title = reader.GetString(1);
            movie.Director = reader.GetString(2);
            movie.ReleaseYear = reader.GetInt32(3);
            movie.Genre = reader.GetString(4);
            movie.ImdbRating = reader.GetDouble(5);
            movie.RunTime = reader.GetInt32(6);
            movie.Note = reader.GetString(7);

            return movie;
        }
    }
}
