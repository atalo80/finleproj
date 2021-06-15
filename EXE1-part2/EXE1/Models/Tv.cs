using EXE1.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EXE1.Models
{
    public class Tv
    {
        int id;
        string name;
        string first_air_date;
        string origin_country;
        string original_language;
        string overview;
        float popularity;
        string poster_path;

        public Tv()
        {
        }

        public Tv(int id, string first_air_date, string origin_country, string original_language, string overview, float popularity, string poster_path)
        {
            this.id = id;
            this.first_air_date = first_air_date;
            this.origin_country = origin_country;
            this.original_language = original_language;
            this.Overview = overview;
            this.popularity = popularity;
            this.poster_path = poster_path;
        }

        public int Id { get => id; set => id = value; }
        public string First_air_date { get => first_air_date; set => first_air_date = value; }
        public string Origin_country { get => origin_country; set => origin_country = value; }
        public string Original_language { get => original_language; set => original_language = value; }
        public string poster_pathname { get => Overview; set => Overview = value; }
        public float Popularity { get => popularity; set => popularity = value; }
        public string Poster_path { get => poster_path; set => poster_path = value; }
        public string Name { get => name; set => name = value; }
        public string Overview { get => overview; set => overview = value; }

        public int Insert()
        {
            DataServices ds = new DataServices();
            ds.Insert(this);
            return 1;
        }

    }
}