using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork22.Controllers
{
    using Model;
    using ViewModels;

    [Route("/api/cities")]
    public class CityController : Controller
    {
        public Storage Storage { get; }
        
        public CityController(Storage storage)
        {
            Storage = storage;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(Storage.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var city = Storage
                .Cities
                .FirstOrDefault(_ => _.Id == id);

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

            var duplicate = Storage.Cities.FirstOrDefault(_ => _.Title == city.Title);
            if (duplicate != null)
            {
                ModelState.AddModelError("Title", "Duplicate value");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Storage.Cities.Add(city);

            return CreatedAtAction("Get", new { id = city.Id }, city);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CityCreateViewModel info, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var city = Storage
                .Cities
                .FirstOrDefault(_ => _.Id == id);
            
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

            Storage.Cities.Add(updatedCity);
            
            Storage.Cities.Remove(city);
            
            return Ok(updatedCity);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = Storage
                .Cities
                .FirstOrDefault(_ => _.Id == id);

            if (city == null)
                return NotFound();

            Storage.Cities.Remove(city);

            return NoContent();
        }
    }
}