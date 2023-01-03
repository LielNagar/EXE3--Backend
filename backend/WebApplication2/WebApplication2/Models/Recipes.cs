using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Recipes
    {
        private int id;
        private string name;
        private string imgUrl;
        private string cookingMethod;
        private int time;
        private int[] ingredientsIDS;
        private bool is_done;

        public Recipes(string name, string imgUrl, string cookingMethod, int time, int[] ingredientsIDS, bool is_done)
        {
            this.Is_done= is_done;
            this.IngredientsIDS = ingredientsIDS;
            this.Name = name;
            this.ImgUrl = imgUrl;
            this.CookingMethod = cookingMethod;
            this.Time = time;
        }
        public Recipes() { }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public string CookingMethod { get => cookingMethod; set => cookingMethod = value; }
        public int Time { get => time; set => time = value; }
        public int[] IngredientsIDS { get => ingredientsIDS; set => ingredientsIDS = value; }
        public bool Is_done { get => is_done; set => is_done = value; }
    }
}