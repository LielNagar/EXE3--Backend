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
    public class IngredientsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Ingredients> Get()
        {
            Ingredients_DAL IDAL = new Ingredients_DAL();
            return IDAL.getIngredients();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Ingredients value)
        {
            try
            {
                Ingredients_DAL IDAL = new Ingredients_DAL();
                if (IDAL.createIngredient(value) == 1) return Content(HttpStatusCode.Created, value);
                else return Content(HttpStatusCode.BadRequest, "couldnt post");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}