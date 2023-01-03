using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Ingredients
    {
        private int id;
        private string name;
        private string imgUrl;
        private int calories;
        public string Name { get => name; set => name = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public int Calories { get => calories; set => calories = value; }
        public int Id { get => id; set => id = value; }

        public Ingredients(string name, string imgUrl, int calories)
        {
            this.Name = name;
            this.ImgUrl = imgUrl;
            this.Calories = calories;
        }

        public Ingredients() { }
    }
}