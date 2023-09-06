using API_Angular.Intefaces;
using API_Angular.Models;
using API_Angular.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Angular.Controllers
{
    [ApiController]
    [Route("api/Funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioInterface _funcionarioInterface;

        public FuncionarioController(IFuncionarioInterface funcionarioInterface)
        {
            _funcionarioInterface = funcionarioInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> GetFuncionarios()
        {
            return Ok(await _funcionarioInterface.GetFuncionarios());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> GetFuncionarioById(int id)
        {
            return Ok(await _funcionarioInterface.GetFuncionarioById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> CreateFuncionario(FuncionarioModel newFuncionario)
        {
            return Ok(await _funcionarioInterface.CreateFuncionario(newFuncionario));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> UpdateFuncionario(FuncionarioModel editFuncionario)
        {
            return Ok(await _funcionarioInterface.UpdateFuncionario(editFuncionario));
        }

        [HttpPut("InativaFuncionario")]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> InativaFuncionario(int id)
        {
            return Ok(await _funcionarioInterface.InativaFuncionario(id));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> DeleteFuncionario(int id)
        {
            return Ok(await _funcionarioInterface.DeleteFuncionario(id));
        }
    }
}
