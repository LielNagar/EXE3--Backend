using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public class Recipes_DAL
    {
        public int putReady(int id, int value)
        {
            SqlConnection con = Connect();
            SqlCommand command = createPutReadyCommand(con, id, value);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }
        private SqlCommand createPutReadyCommand(SqlConnection con, int id, int value)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@value", value);
            command.CommandText = "spPutReady";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public IEnumerable<Ingredients> getIngredients(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetIngredientsInRecipeCommand(con, id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Ingredients> ingredients = new List<Ingredients>();
            while (dr.Read())
            {
                Ingredients ingredient = new Ingredients();
                ingredient.Id = Convert.ToInt32(dr["id"]);
                ingredient.Name = dr["name"].ToString();
                ingredient.ImgUrl = dr["imageUrl"].ToString();
                ingredient.Calories = Convert.ToInt32(dr["calories"]);
                ingredients.Add(ingredient);
            }
            con.Close();
            return ingredients;
        }
        public SqlCommand createGetIngredientsInRecipeCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spGetIngredientsInRecipe";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
        public IEnumerable<Recipes> getRecipes()
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand("SELECT * FROM RECIPES", con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Recipes> recipes = new List<Recipes>();
            while (dr.Read())
            {
                Recipes recipe = new Recipes();
                recipe.Id = Convert.ToInt32(dr["id"]);
                recipe.Name = dr["name"].ToString();
                recipe.ImgUrl = dr["imageUrl"].ToString();
                recipe.Time = Convert.ToInt32(dr["time"]);
                recipe.CookingMethod = dr["CookingMethod"].ToString();
                recipe.Is_done = Convert.ToBoolean(dr["is_done"]);
                recipes.Add(recipe);
            }
            con.Close();
            return recipes;
        }
        public int insertIngredientsToRecipe(int id, int recipeId)
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = "INSERT INTO IngredientsInRecipes(recipeId,ingredientId) VALUES(@recipeId,@id)";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@recipeId", recipeId);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }
        public int insertRecipe(Recipes recipe)
        {
            SqlConnection con = Connect();
            SqlCommand command = createPostRecipeCommand(con, recipe);
            int numAffected = command.ExecuteNonQuery();
            return numAffected;
        }

        public int getId()
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand();
            command.CommandText = "select top 1 id from recipes order by id desc";
            command.Connection = con;
            command.CommandTimeout = 10;
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            int id = 0;
            while (dr.Read())
            {
                id = Convert.ToInt32(dr["id"]);
            }
            return id;
        }

        private SqlCommand createPostRecipeCommand(SqlConnection con, Recipes recipe)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@Name", recipe.Name);
            command.Parameters.AddWithValue("@Time", recipe.Time);
            command.Parameters.AddWithValue("@ImgUrl", recipe.ImgUrl);
            command.Parameters.AddWithValue("@CookingMethod", recipe.CookingMethod);
            command.CommandText = "spInsertRecipe";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
        private SqlConnection Connect() //CONNECTION TO DB FUNCTION
        {
            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);
            // open the database connection
            con.Open();
            return con;
        }
    }
}