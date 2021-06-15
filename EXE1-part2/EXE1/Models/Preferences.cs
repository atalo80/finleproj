using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EXE1.Models.DAL;

namespace EXE1.Models                 
{
    public class Preferences
    {
        int idserise;
        int  idusers; //need to add user
        Episode episode;

        public Preferences(int idserise, int idusers, Episode episode)
        {
            this.Idserise = idserise;
            this.Idusers = idusers;
            this.Episode = episode;
        }

        public int Idserise { get => idserise; set => idserise = value; }
        public int Idusers { get => idusers; set => idusers = value; }
        public Episode Episode { get => episode; set => episode = value; }

        public int Insert()
        {
            DataServices ds = new DataServices();
            ds.Insert(this);
            return 1;
        }

    }
}