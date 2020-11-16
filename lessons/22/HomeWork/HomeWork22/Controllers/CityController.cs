using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork22.Controllers
{
    using HomeWork22.Storage;
    using Model;
    using ViewModels;

    [Route("/api/cities")]
    public class CityController : Controller
    {
        private readonly IStorage _storage;

        public CityController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var city = _storage.Get(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CityCreateViewModel info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = new City(
                Guid.NewGuid(),
                info.Title.Trim().Capitalize(),
                info.Description.Trim().Capitalize(),
                info.Population);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _storage.Add(city);

            return CreatedAtAction("Get", new { id = city.Id }, city);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CityCreateViewModel info, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var city = _storage.Get(id);
            
            if (city == null)
                return NotFound();
            
            var updatedCity = new City(
                city.Id, 
                city.Title.Trim().Capitalize(), 
                info.Description.Trim().Capitalize(), 
                info.Population
                );
            
            if (city.Title == updatedCity.Description)
            {
                ModelState.AddModelError("Title", "Description duplicated");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _storage.Add(updatedCity);
            
            _storage.Delete(city);
            
            return Ok(updatedCity);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = _storage.Get(id);

            if (city == null)
                return NotFound();

            _storage.Delete(city);

            return NoContent();
        }
    }
}