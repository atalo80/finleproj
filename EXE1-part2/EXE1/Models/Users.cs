using EXE1.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EXE1.Models
{
    public class Users
    {
        string name;
        string lastName;
        string email;
        string password;
        string phone;
        char gender;
        int yearofBirth;
        string favoriteStyle;
        string addresses;

        public Users()
        {

        }
        public Users(string name, string lastName, string email, string password, string phone, char gender, int yearofBirth, string favoriteStyle, string addresses)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.Phone = phone;
            this.Gender = gender;
            this.YearofBirth = yearofBirth;
            this.FavoriteStyle = favoriteStyle;
            this.Addresses = addresses;
        }

        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Phone { get => phone; set => phone = value; }
      
        public int YearofBirth { get => yearofBirth; set => yearofBirth = value; }
        public string FavoriteStyle { get => favoriteStyle; set => favoriteStyle = value; }
        public string Addresses { get => addresses; set => addresses = value; }
        public char Gender { get => gender; set => gender = value; }

        public int Insert()
        {
            DataServices ds = new DataServices();
            ds.Insert(this);
            return 1;
        }

        public Users Get(string name, string password)
        {
            DataServices ds = new DataServices();
            Users u = ds.getUserID(name , password);
            return u;
        }
    }
}