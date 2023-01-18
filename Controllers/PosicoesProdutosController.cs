using Microsoft.AspNetCore.Mvc;
using Radar_Api.Models;
using Radar_Api.Repositorios.Interfaces;

namespace api.Controllers;

[Route("posicoesProdutos")]
public class PosicoesProdutosController : ControllerBase
{
   private IServico<PosicaoProduto> _servico; 

   public PosicoesProdutosController(IServico<PosicaoProduto> servico)
   {
        _servico = servico;
   }

   [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var posicoesProdutos = await _servico.TodosAsync();
        return StatusCode(200, posicoesProdutos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details([FromRoute] int id)
    {
        var posicaoProduto = (await _servico.TodosAsync()).Find(c => c.Id == id);

        return StatusCode(200, posicaoProduto );
    }

    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] PosicaoProduto posicaoProduto)
    {
        await _servico.IncluirAsync(posicaoProduto);
        return StatusCode(201, posicaoProduto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PosicaoProduto posicaoProduto)
    {
        if (id != posicaoProduto.Id)
        {
            return StatusCode(400, new
            {
                Mensagem = "O Id precisa bater com o id da URL"
            });
        }

        var posicaoProdutoDb = await _servico.AtualizarAsync(posicaoProduto);

        return StatusCode(200, posicaoProdutoDb);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var posicaoProdutoDb = (await _servico.TodosAsync()).Find(c => c.Id == id);
        if (posicaoProdutoDb is null)
        {
            return StatusCode(404, new
            {
                Mensagem = "A posição do Produto informada não existe"
            });
        }

        await _servico.ApagarAsync(posicaoProdutoDb);

        return StatusCode(204);
    }
}