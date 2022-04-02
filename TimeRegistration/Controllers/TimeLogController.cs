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
        private readonly IContractRepository _repoContract;
        public TimeLogController(ITimeLogRepository repo, IContractRepository repoContract)
        {
            _repo = repo;
            _repoContract = repoContract;
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
            if (_repo.UpdateHourExit(id))
            {
                var contractId = _repo.ReadId(id);
                int? id2 = contractId.ContractId;
                if (_repoContract.UpdateTotalHours(id2))
                {
                    Ok();
                }
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
