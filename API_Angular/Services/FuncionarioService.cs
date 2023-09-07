using API_Angular.DataContext;
using API_Angular.Intefaces;
using API_Angular.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.PTG;

namespace API_Angular.Services
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly AppDbContext _context;

        public FuncionarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> CreateFuncionario(FuncionarioModel newFuncionario)
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                if (newFuncionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                newFuncionario.DataDeCriacao = DateTime.Now.ToLocalTime();
                newFuncionario.DataDeAlteracao = DateTime.Now.ToLocalTime();

                _context.Add(newFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;    
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id)
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);
                
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id)
        {
            var serviceResponse = new ServiceResponse<FuncionarioModel>();

            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);

                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Não foi possível encontrar o funcionário!";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = funcionario;

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionarios()
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();

                if (serviceResponse.Dados!.Count == 0)
                    serviceResponse.Mensagem = "Não há nenhum usuário!";
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id)
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);

                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;
                }

                funcionario!.Ativo = false;
                funcionario.DataDeAlteracao = DateTime.Now.ToLocalTime();

                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList(); 
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editandoFuncionario)
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionario = await _context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == editandoFuncionario.Id);

                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;
                }

                funcionario!.DataDeAlteracao = DateTime.Now.ToLocalTime();
                _context.Funcionarios.Update(editandoFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }
    }
}
