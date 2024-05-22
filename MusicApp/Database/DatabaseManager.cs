using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Moq;
using MusicApp.Search;

namespace MusicApp.Database
{
    public class DatabaseManager : IDatabaseManager
    {
        private const string ConnectionString = "Data Source=LAPTOP-I0STBNP1\\SQLEXPRESS01;Initial Catalog=Software2;Integrated Security=True";
        private static DatabaseManager instance;

        // Private constructor to prevent instantiation from outside
        private DatabaseManager()
        {
        }

        // Public static method to get the instance of DatabaseManager class
        public static IDatabaseManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DatabaseManager();
            }
            return instance;
        }

        // Method to allow setting the instance for testing purposes
        public static void SetInstance(DatabaseManager mockInstance)
        {
            instance = mockInstance;
        }

        public bool RegisterUser(string username, string password, string salt)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO [USER] (userName, profilePicture, subscriptionPlan, hashedPassword, salt) VALUES (@userName, @profilePicture, @subscriptionPlan, @hashedPassword, @salt);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userName", username);
                    command.Parameters.AddWithValue("@profilePicture", " ");
                    command.Parameters.AddWithValue("@subscriptionPlan", " ");
                    command.Parameters.AddWithValue("@hashedPassword", password);
                    command.Parameters.AddWithValue("@salt", salt);
                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error while inserting values in the database: " + exception.Message);
                return false;
            }

            return true;
        }

        public (string, string) GetCredentials(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT hashedPassword, salt FROM [USER] WHERE userName = @userName;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userName", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPassword = reader.GetString(0);
                            string salt = reader.GetString(1);

                            return (hashedPassword, salt);
                        }
                        else
                        {
                            MessageBox.Show("No user found with the specified username.", "Warning", MessageBoxButton.OK);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while inserting values in the database: " + ex.Message);
            }

            return (" ", " ");
        }

        public List<Search.SearchResultItemControl> LoadUserSearchItems(List<Search.SearchResultItemControl> searchItems)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Specify query to execute
                    string query = "SELECT * FROM [USER];";

                    // Create a SqlDataAdapter to execute the query and retrieve data
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    // Fill a DataTable with the results of the query
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Convert each row into a search result item
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string image = row["profilePicture"].ToString();
                        string title = row["userName"].ToString();
                        string subtitle1 = "User";
                        SearchWindow searchWindow = new SearchWindow();
                        searchItems.Add(searchWindow.AddSearchResult(image, title, subtitle1));
                    }

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error while retrieving items from the database: " + exception.Message);
            }
            return searchItems;
        }

        public List<Search.SearchResultItemControl> LoadArtistSearchItems(List<Search.SearchResultItemControl> searchItems)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Specify query to execute
                    string query = "SELECT * FROM ARTIST;";

                    // Create a SqlDataAdapter to execute the query and retrieve data
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    // Fill a DataTable with the results of the query
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Convert each row into a search result item
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string image = row["artistPicture"].ToString();
                        string title = row["artistName"].ToString();
                        string subtitle1 = "Artist";
                        SearchWindow searchWindow = new SearchWindow();
                        searchItems.Add(searchWindow.AddSearchResult(image, title, subtitle1));
                    }

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error while retrieving items from the database: " + exception.Message);
            }
            return searchItems;
        }

        public List<Search.SearchResultItemControl> LoadSongSearchItems(List<Search.SearchResultItemControl> searchItems, string genreFilter = "")
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Specify query to execute
                    string query;
                    if (genreFilter != " ")
                    {
                        query = "SELECT * FROM SONG WHERE album IN ( SELECT albumId FROM ALBUM WHERE genre = @genre);";
                    }
                    else
                    {
                        query = "SELECT * FROM SONG;";
                    }

                    // Create a SqlDataAdapter to execute the query and retrieve data
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    if (genreFilter != " ")
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@genre", genreFilter);
                    }

                    // Fill a DataTable with the results of the query
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Convert each row into a search result item
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string title = row["songName"].ToString();

                        // Resolve the database references (foreign keys) to properly pass the data to AddSearchResult()
                        string getImageQuery = "SELECT albumPicture FROM ALBUM WHERE albumId = (SELECT album FROM SONG WHERE songName = @title);";
                        SqlCommand command = new SqlCommand(getImageQuery, connection);
                        command.Parameters.AddWithValue("@title", title);
                        string image = command.ExecuteScalar().ToString();

                        string getArtistQuery = "SELECT artistName FROM ARTIST WHERE artistId = ( SELECT albumArtist FROM ALBUM WHERE albumId = ( SELECT album FROM SONG WHERE songName = @title));";
                        SqlCommand command1 = new SqlCommand(getArtistQuery, connection);
                        command1.Parameters.AddWithValue("@title", title);
                        // Since we know the result of the select is a single element (one row and one column) we can use ExecuteScalar() to get that value
                        string subtitle1 = command1.ExecuteScalar().ToString();

                        string getDateQuery = "SELECT [date] FROM ALBUM WHERE albumId = (SELECT album FROM SONG WHERE songName = @title);";
                        SqlCommand command2 = new SqlCommand(getDateQuery, connection);
                        command2.Parameters.AddWithValue("@title", title);
                        string subtitle2 = command2.ExecuteScalar().ToString();

                        string getGenreQuery = "SELECT genre FROM ALBUM WHERE albumId = (SELECT album FROM SONG WHERE songName = @title);";
                        SqlCommand command3 = new SqlCommand(getGenreQuery, connection);
                        command3.Parameters.AddWithValue("@title", title);
                        string subtitle3 = command3.ExecuteScalar().ToString();

                        SearchWindow searchWindow = new SearchWindow();
                        searchItems.Add(searchWindow.AddSearchResult(image, title, subtitle1, subtitle2, subtitle3));
                    }

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error while retrieving items from the database: " + exception.Message);
            }
            return searchItems;
        }

        public List<Search.SearchResultItemControl> LoadAlbumSearchItems(List<Search.SearchResultItemControl> searchItems, string genreFilter = "")
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Specify query to execute
                    string query;
                    if (genreFilter != " ")
                    {
                        query = "SELECT * FROM ALBUM WHERE genre = @genre;";
                    }
                    else
                    {
                        query = "SELECT * FROM ALBUM;";
                    }

                    // Create a SqlDataAdapter to execute the query and retrieve data
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    if (genreFilter != " ")
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@genre", genreFilter);
                    }

                    // Fill a DataTable with the results of the query
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Convert each row into a search result item
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string image = row["albumPicture"].ToString();
                        string title = row["albumName"].ToString();
                        string subtitle2 = row["date"].ToString();
                        string subtitle3 = row["genre"].ToString();

                        // Resolve the database references (foreign keys) to properly pass the data to AddSearchResult()
                        string getArtistQuery = "SELECT artistName FROM ARTIST WHERE artistId = ( SELECT albumArtist FROM ALBUM WHERE albumName = @title);";
                        SqlCommand command = new SqlCommand(getArtistQuery, connection);
                        command.Parameters.AddWithValue("@title", title);
                        string subtitle1 = command.ExecuteScalar().ToString();

                        // Create a new instance of SearchWindow
                        SearchWindow searchWindow = new SearchWindow();
                        searchItems.Add(searchWindow.AddSearchResult(image, title, subtitle1, subtitle2, subtitle3));
                    }
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error while retrieving items from the database: " + exception.Message);
            }
            return searchItems;
        }
    }
}