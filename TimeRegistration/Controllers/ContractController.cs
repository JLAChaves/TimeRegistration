using Microsoft.AspNetCore.Mvc;
using TimeRegistration.Models;
using TimeRegistration.Repositories;

namespace TimeRegistration.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractRepository _repo;

        public ContractController(IContractRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult Create(Contract contract)
        {
            if (_repo.Create(contract))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("Search")]
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

        [HttpGet("SearchId/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var getById = _repo.ReadId(id);
                return Ok(getById);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("SearchName/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var getByName = _repo.ReadNames(name);
                return Ok(getByName);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut]
        public IActionResult Update(Contract contract)
        {
            if (_repo.Update(contract))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateValue(int id)
        {
            if (_repo.UpdateTotalHours(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult Delete(Contract contract)
        {
            if (_repo.Delete(contract))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
