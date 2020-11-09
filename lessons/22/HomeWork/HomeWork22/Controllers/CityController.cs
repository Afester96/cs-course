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
        public Storage Storage =>
            Storage.Instance;

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
            var city = new City(Guid.NewGuid(), info.Title, info.Description, info.Population);
            Storage.Cities.Add(city);

            return CreatedAtAction("Get", new { id = city.Id }, city);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CityCreateViewModel info, Guid id)
        {
            var city = Storage
                .Cities
                .FirstOrDefault(_ => _.Id == id);
            if (city == null)
                return NotFound();
            var updatedCity = new City(city.Id, city.Title, info.Description, info.Population);
            Storage.Cities.Add(updatedCity);
            Storage.Cities.Remove(city);
            return Ok(updatedCity);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
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