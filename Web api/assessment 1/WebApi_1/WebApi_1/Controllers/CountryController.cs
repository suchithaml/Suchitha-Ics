using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_1.Models;

namespace WebApi_1.Controllers
{
    public class CountryController : ApiController
    {
        private static List<Country> countries = new List<Country>()
        {
            new Country { ID = 1, CountryName = "India", Capital = "New Delhi" },
            new Country { ID = 2, CountryName = "USA", Capital = "Washington D.C." },
            new Country { ID = 3, CountryName = "Japan", Capital = "Tokyo" },
            new Country { ID = 4, CountryName = "South Korea", Capital = "Seoul" },
            new Country { ID = 5, CountryName = "France", Capital = "Paris" }


        };

        public IHttpActionResult Get()
        {
            return Ok(countries);
        }

        public IHttpActionResult Get(int id)
        {
            var country = countries.FirstOrDefault(c => c.ID == id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        public IHttpActionResult Post([FromBody] Country country)
        {
            if (country == null)
            {
                return BadRequest("Invalid data.");
            }

            country.ID = countries.Max(c => c.ID) + 1; 
            countries.Add(country);
            return Created($"api/Country/{country.ID}", country);
        }

        public IHttpActionResult Put(int id, [FromBody] Country updatedCountry)
        {
            var existingCountry = countries.FirstOrDefault(c => c.ID == id);
            if (existingCountry == null)
            {
                return NotFound();
            }

            if (updatedCountry == null)
            {
                return BadRequest("Invalid data.");
            }

            existingCountry.CountryName = updatedCountry.CountryName;
            existingCountry.Capital = updatedCountry.Capital;
            return Ok(existingCountry);
        }

        public IHttpActionResult Delete(int id)
        {
            var country = countries.FirstOrDefault(c => c.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            countries.Remove(country);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
