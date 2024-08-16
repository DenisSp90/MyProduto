using Microsoft.EntityFrameworkCore;
using MyProduto.Data;
using MyProduto.Models;

namespace MyProduto.Services;

public interface IProdutoService
{
    List<Produto> GetProdutosEmEstoque();
    List<Produto> GetProdutosComEstoqueBaixo();
    List<MovimentacaoEstoque> GetMovimentacaoEstoque();
    List<Produto> GetProdutosParados();
}

public class ProdutoService : IProdutoService
{
    private readonly ApplicationDbContext _context;

    public ProdutoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<MovimentacaoEstoque> GetMovimentacaoEstoque()
    {
        return _context.MovimentacoesEstoque
            .Include(m => m.Produto)  
            .ToList();

    }

    public List<Produto> GetProdutosComEstoqueBaixo()
    {
        return _context.Produtos
            .Where(p => p.QuantidadeEstoque < p.EstoqueMinimo)
            .ToList();
    }

    public List<Produto> GetProdutosEmEstoque()
    {
        return _context.Produtos
            .Where(p => p.QuantidadeEstoque > 0)
            .ToList();
    }

    public List<Produto> GetProdutosParados()
    {
        var doisDiasAtras = DateTime.Now.AddDays(-2);

        return _context.Produtos
            .Where(p => !_context.MovimentacoesEstoque
                .Any(m => m.ProdutoId == p.Id && m.DataMovimentacao >= doisDiasAtras))
            .ToList();
    }
}
