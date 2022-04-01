using Microsoft.AspNetCore.Mvc;
using TimeRegistration.Models;
using TimeRegistration.Repositories;

namespace TimeRegistration.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {
        private readonly ITimeLogRepository _repo;

        public TimeLogController(ITimeLogRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult Create(TimeLog timeLog)
        {
            if (_repo.Create(timeLog))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var getAll = _repo.ReadAll();
                return Ok(getAll);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("UpdateExit/{id}")]
        public IActionResult UpdateExit(int id)
        {
            if (_repo.UpdateExit(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult Delete(TimeLog timeLog)
        {
            if (_repo.Delete(timeLog))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
