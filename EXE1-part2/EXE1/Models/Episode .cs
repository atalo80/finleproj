using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EXE1.Models.DAL;

namespace EXE1.Models
{
    public class Episode
    {
        int tvid;
        string nameTvshows;
        int season;
        string nameEpisod;
        string picture;
        string overview;
        string date;
        int episode_number;

        public string NameTvshows { get => nameTvshows; set => nameTvshows = value; }
        public int Season { get => season; set => season = value; }
        public string NameEpisod { get => nameEpisod; set => nameEpisod = value; }
        public string Picture { get => picture; set => picture = value; }
        public string Overview { get => overview; set => overview = value; }
        public string Date { get => date; set => date = value; }
        public int Episode_number { get => episode_number; set => episode_number = value; }
        public int Tvid { get => tvid; set => tvid = value; }

        public Episode()
        {
        }

        public Episode(int tvid, string nameTvshows, int season, string nameEpisod, string picture, string overview, string date, int episode_number)
        {
            this.tvid = tvid;
            this.nameTvshows = nameTvshows;
            this.season = season;
            this.nameEpisod = nameEpisod;
            this.picture = picture;
            this.overview = overview;
            this.date = date;
            this.episode_number = episode_number;
        }


        public List<string> Get()
        {
            DataServices ds = new DataServices();
            List<string> sList = ds.getsesens();
            return sList;
        }

        public int Insert()
        {
            DataServices ds = new DataServices();
            ds.Insert(this);
            return 1;
        }

        public List<Episode> Get(string nameTvshows)
        {
            DataServices ds = new DataServices();
            List<Episode> sList = ds.getviews(nameTvshows);
            return sList;
        }
    }
}