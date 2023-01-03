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
    public class Ingredients_DAL
    {
        public int createIngredient(Ingredients ingredient)
        {
            SqlConnection con = Connect();
            SqlCommand command = createInsertIngredientCommand(con, ingredient);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }
        private SqlCommand createInsertIngredientCommand(SqlConnection con, Ingredients ingredient)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@Name", ingredient.Name);
            command.Parameters.AddWithValue("@calories", ingredient.Calories);
            command.Parameters.AddWithValue("@ImgUrl", ingredient.ImgUrl);
            command.CommandText = "spInsertIngredient";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public IEnumerable<Ingredients> getIngredients()
        {  
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand("SELECT * from [Ingredients]", con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Ingredients> ingredients = new List<Ingredients>();
            while (dr.Read())
            {
                Ingredients ingredient = new Ingredients();
                ingredient.Name = dr["name"].ToString();
                ingredient.ImgUrl = dr["imageUrl"].ToString();
                ingredient.Calories = Convert.ToInt32(dr["calories"]);
                ingredient.Id = Convert.ToInt32(dr["id"]);
                ingredients.Add(ingredient);
            }
            con.Close();
            return ingredients;
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