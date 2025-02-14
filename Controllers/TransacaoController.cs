using Microsoft.AspNetCore.Mvc;
using ApiDtMoney.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class TransacaoController : ControllerBase
{
    private readonly TransacaoDbContext _context;

    public TransacaoController(TransacaoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transacao>>> GetTransacao()
    {
        return await _context.transacoes.ToListAsync();
    }

    [HttpGet("total")]
    public async Task<ActionResult<double>> GetTotalValue()
    {
        var transacoes = await _context.transacoes.ToListAsync();
        var total = transacoes.Sum(t => (double)t.Valor);
        return Ok(total);
    }

    [HttpPost]
    public async Task<ActionResult> PostTransacao(Transacao transacao)
    {
        _context.transacoes.Add(transacao);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTransacao), new { id = transacao.Id }, transacao);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransacao(int id)
    {
        var transacao = await _context.transacoes.FindAsync(id);
        if (transacao == null)
        {
            return NotFound();
        }

        _context.transacoes.Remove(transacao);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}