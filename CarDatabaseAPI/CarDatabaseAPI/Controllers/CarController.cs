using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarDatabaseAPI.Models;
namespace CarDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Car[]> Get()
        {
            try
            {
                List<Car> cars = DatabaseServices.GetAllCars();
                return Ok(cars.ToArray());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Car))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get(int id)
        {
            try
            {
                Car car = DatabaseServices.GetCarById(id);
                if (car == null)
                    return NotFound($"User with id: {id} wasn't found.");
                return Ok(car);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Car))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Car value)
        {
            try
            {
                if (value == null)
                    return BadRequest("User is null.");
                if (value.id != 0)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Cannot specify Id for new Car.");

                value.id = DatabaseServices.InsertCar(value);

                return CreatedAtAction(nameof(Get), new { id = value.id }, value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Car value)
        {
            try
            {
                if (value == null || value.id != id)
                    return BadRequest();

                int rowsAffected = DatabaseServices.UpdateCar(value);
                if (rowsAffected == 0)
                    return NotFound($"Car with id: {id} wasn't found, can't update.");

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();

                bool isDeleted = DatabaseServices.DeleteCar(id);
                if (!isDeleted)
                    return NotFound($"Car with id: {id} wasn't found, can't delete.");

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
