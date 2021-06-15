using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using EXE1.Controllers;
using EXE1.Models;
namespace EXE1.Models.DAL
{

    public class DataServices
    {

        public SqlDataAdapter da;
        public DataTable dt;
        static int userid;

        public DataServices()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //--------------------------------------------------------------------------------------------------
        // This method inserts a car to the cars table 
        //--------------------------------------------------------------------------------------------------
        public int Insert(Object obj)
        {

            SqlConnection con;
            SqlCommand cmd = new SqlCommand();

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            BuildInsertCommand(con, cmd, obj, userid);      // helper method to build the insert string

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (SqlException Ex)
            {
                switch (Ex.Number)
                {
                    case 2627:
                    case 2601:
                        return 0;
                    default:
                        throw (Ex);

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private void BuildInsertCommand(SqlConnection con, SqlCommand cmd, Object obj, int userid)
        {
            cmd.Connection = con;
            cmd.CommandTimeout = 10;   // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.Text;


            string cText = "";
            // use a string builder to create the dynamic string
            if (obj is Episode)
            {
                Episode Episode = (Episode)obj;
                cText += "INSERT INTO TheMovieDB_Episodes_2021 (tv_id, season_number, episode_number, name, overview, air_date,poster_path ) " +
                "Values (@tv_id, @season_number, @episode_number, @name, @overview, @air_date,@poster_path)";

                cmd.CommandText = cText;
                cmd.Parameters.AddWithValue("@overview", Episode.Overview);
                cmd.Parameters.AddWithValue("@name", Episode.NameEpisod);
                cmd.Parameters.AddWithValue("@air_date", Episode.Date);
                cmd.Parameters.AddWithValue("@poster_path", Episode.Picture);

                cmd.Parameters.Add("@tv_id", SqlDbType.Int);
                cmd.Parameters["@tv_id"].Value = Episode.Tvid;

                cmd.Parameters.Add("@season_number", SqlDbType.Int);
                cmd.Parameters["@season_number"].Value = Episode.Season;

                cmd.Parameters.Add("@episode_number", SqlDbType.Int);
                cmd.Parameters["@episode_number"].Value = Episode.Episode_number;

            }
            else if (obj is Tv)
            {

                Tv tv = (Tv)obj;

                cText += "INSERT INTO TheMovieDB_TV_2021 (tv_id, first_air_date, name, origin_country, original_language, overview, popularity, poster_path ) " +
                "Values (@tv_id, @first_air_date, @name, @origin_country, @original_language, @overview, @popularity, @poster_path )";

                cmd.CommandText = cText;
                cmd.Parameters.AddWithValue("@overview", tv.Overview);
                cmd.Parameters.AddWithValue("@name", tv.Name);
                cmd.Parameters.AddWithValue("@origin_country", tv.Origin_country);
                cmd.Parameters.AddWithValue("@original_language", tv.Original_language);
                cmd.Parameters.AddWithValue("@poster_path", tv.Poster_path);


                cmd.Parameters.Add("@tv_id", SqlDbType.Int);
                cmd.Parameters["@tv_id"].Value = tv.Id;


                cmd.Parameters.Add("@first_air_date", SqlDbType.DateTime);
                cmd.Parameters["@first_air_date"].Value = DateTime.Parse(tv.First_air_date);

                cmd.Parameters.Add("@popularity", SqlDbType.Float);
                cmd.Parameters["@popularity"].Value = tv.Popularity;
            }
            else if (obj is Users)
            {

                Users users = (Users)obj;
                cText += "INSERT INTO TheMovieDB_Users_2021 (email, first_name, last_name, password, phone_num, address,  gender, birth_date, favoriteStyle) " +
                "Values (@email, @first_name, @last_name, @password, @phone_num, @address, @gender, @birth_date, @favoriteStyle )";

                cmd.CommandText = cText;
                cmd.Parameters.AddWithValue("@first_name", users.Name);
                cmd.Parameters.AddWithValue("@last_name", users.LastName);
                cmd.Parameters.AddWithValue("@password", users.Password);
                cmd.Parameters.AddWithValue("@email", users.Email);
                cmd.Parameters.AddWithValue("@favoriteStyle", users.FavoriteStyle);
                cmd.Parameters.AddWithValue("@phone_num", users.Phone);
                cmd.Parameters.AddWithValue("@address", users.Addresses);

                cmd.Parameters.Add("@gender", SqlDbType.Char);
                cmd.Parameters["@gender"].Value = users.Gender;

                cmd.Parameters.Add("@birth_date", SqlDbType.Int);
                cmd.Parameters["@birth_date"].Value = users.YearofBirth;
            }
            else if (obj is Preferences)
            {

                Preferences Pref = (Preferences)obj;
                cText += "INSERT INTO TheMovieDB_Preferences_2021 ( tv_id, user_id, season_number, episode_number) " +
                "Values (@tv_id, @user_id, @season_number, @episode_number)";

                cmd.CommandText = cText;
                cmd.Parameters.AddWithValue("@episode_number", SqlDbType.Int);
                cmd.Parameters["@episode_number"].Value = Pref.Episode.Episode_number;

                cmd.Parameters.Add("@season_number", SqlDbType.Int);
                cmd.Parameters["@season_number"].Value = Pref.Episode.Season;

                cmd.Parameters.Add("@tv_id", SqlDbType.Int);
                cmd.Parameters["@tv_id"].Value = Pref.Idserise;

                cmd.Parameters.Add("@user_id", SqlDbType.Int);
                cmd.Parameters["@user_id"].Value = userid;
            }


        }
        //---------------------------------------------------------------------------------
        // Read from the DB into a list - dataReader
        //---------------------------------------------------------------------------------
        public Users getUserID(string name, string password)
        {

            SqlConnection con = null;


            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM TheMovieDB_Users_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Users u = new Users();
                    u.Name = (string)dr["first_name"];
                    u.LastName = (string)dr["last_name"];
                    u.Password = (string)dr["password"];
                    u.Phone = (string)dr["phone_num"];
                    u.YearofBirth = Convert.ToInt32(dr["birth_date"]);
                    u.Gender = Convert.ToChar(dr["gender"]);
                    u.Addresses = (string)dr["address"];
                    u.Email = (string)dr["email"];
                    u.FavoriteStyle = (string)dr["favoriteStyle"];
                    if ((u.Name == name) && (u.Password == password))
                    {
                        userid = Convert.ToInt32(dr["user_id"]);
                        return u;
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //---------------------------------------------------------------------------------
        // Read from the DB into a list - dataReader
        //---------------------------------------------------------------------------------
        public List<string> getsesens()
        {
            List<string> nameTVlist = new List<string>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT name FROM TheMovieDB_TV_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    nameTVlist.Add((string)dr["name"]);

                }
                return nameTVlist;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //---------------------------------------------------------------------------------
        // Read from the DB into a list - dataReader
        //---------------------------------------------------------------------------------
        public List<Episode> getviews(string nameTvshows)
        {
            List<Episode> listepisode = new List<Episode>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file



                String selectSTR = "SELECT distinct e.* FROM TheMovieDB_TV_2021 as tv inner join TheMovieDB_Preferences_2021 as p on tv.tv_id=p.tv_id  inner join TheMovieDB_Episodes_2021 e on e.tv_id=p.tv_id where p.user_id=" + userid + " and tv.name='" + nameTvshows + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Episode episode = new Episode();

                    episode.Date = (string)dr["air_date"];
                    episode.Episode_number = Convert.ToInt32(dr["episode_number"]);
                    episode.NameEpisod = (string)dr["name"];
                    episode.NameTvshows = (string)dr["name"];
                    episode.Overview = (string)dr["overview"];
                    episode.Picture = (string)dr["poster_path"];
                    episode.Season = Convert.ToInt32(dr["season_number"]);
                    episode.Tvid = Convert.ToInt32(dr["tv_id"]);


                    listepisode.Add(episode);

                }
                return listepisode;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
    }
}