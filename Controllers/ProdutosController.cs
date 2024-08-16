using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProduto.Data;
using MyProduto.Models;

namespace MyProduto.Controllers;

public class ProdutosController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public ProdutosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Produtos.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Produto produto)
    {
        if (ModelState.IsValid)
        {
            produto.UltimaMovimentacao = DateTime.Now;
            _context.Add(produto);
            await _context.SaveChangesAsync();

            var movimentacao = new MovimentacaoEstoque
            {
                ProdutoId = produto.Id,
                DataMovimentacao = DateTime.Now,
                Quantidade = produto.QuantidadeEstoque,
                TipoMovimentacao = "Entrada"
            };

            _context.MovimentacoesEstoque.Add(movimentacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }

    public async Task<IActionResult> Detalhes(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] Produto produto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var produtoOriginal = await _context.Produtos
                    .Include(p => p.MovimentacoesEstoque)
                    .FirstOrDefaultAsync(p => p.Id == produto.Id);

                if (produtoOriginal == null)
                {
                    return NotFound();
                }

                // Capturar a diferença de quantidade antes de atualizar
                int diferencaQuantidade = produto.QuantidadeEstoque - produtoOriginal.QuantidadeEstoque;

                produtoOriginal.Nome = produto.Nome;
                produtoOriginal.Preco = produto.Preco;
                produtoOriginal.EstoqueMinimo = produto.EstoqueMinimo;
                produtoOriginal.QuantidadeEstoque = produto.QuantidadeEstoque;
                produtoOriginal.UltimaMovimentacao = DateTime.Now;

                // Salvar as alterações no produto original
                await _context.SaveChangesAsync();

                if (diferencaQuantidade != 0)
                {
                    var movimentacao = new MovimentacaoEstoque
                    {
                        ProdutoId = produtoOriginal.Id,
                        DataMovimentacao = DateTime.Now,
                        Quantidade = diferencaQuantidade,
                        TipoMovimentacao = diferencaQuantidade > 0 ? "Entrada" : "Saída"
                    };

                    _context.MovimentacoesEstoque.Add(movimentacao);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var produto = await _context.Produtos
                .Include(p => p.MovimentacoesEstoque)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto != null)
            {
                var movimentacao = new MovimentacaoEstoque
                {
                    ProdutoId = produto.Id,
                    DataMovimentacao = DateTime.Now,
                    Quantidade = -produto.QuantidadeEstoque,
                    TipoMovimentacao = "Saída"
                };

                _context.MovimentacoesEstoque.Add(movimentacao);

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true }); 
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Ocorreu um erro ao tentar deletar o produto. Por favor, tente novamente.");
            return Json(new { success = false, errorMessage = ex.Message });
        }
    }

    [HttpGet]
    public async Task<JsonResult> CheckProdutoNome(string nome)
    {
        try
        {
            var produtoExistente = await _context.Produtos
                 .Where(p => p.Nome.ToLower() == nome.ToLower())
                .ToArrayAsync();

            return Json(produtoExistente != null);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao verificar o produto: {ex.Message}");


            return Json(new { success = false, message = "Erro ao verificar o produto." });
        }
    }

    private bool ProdutoExists(int id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }   
}
