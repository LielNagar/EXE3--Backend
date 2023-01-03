using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.DAL;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RecipesController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Recipes> Get()
        {
            Recipes_DAL RDAL = new Recipes_DAL();
            return RDAL.getRecipes();
        }

        // GET api/<controller>/5
        public IEnumerable<Ingredients> Get(int id)
        {
            Recipes_DAL RDAL = new Recipes_DAL();
            return RDAL.getIngredients(id);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Recipes recipe)
        {
            Recipes_DAL RDAL = new Recipes_DAL();
            RDAL.insertRecipe(recipe);
            int recipeId = RDAL.getId();
            foreach (var id in recipe.IngredientsIDS)
            {
                RDAL.insertIngredientsToRecipe(id, recipeId);
            }
            return Content(HttpStatusCode.Created, recipe);
        }

        // PUT api/<controller>/5
        public int Put(int id, int value)
        {
            Recipes_DAL RDAL = new Recipes_DAL();
            return RDAL.putReady(id, value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}