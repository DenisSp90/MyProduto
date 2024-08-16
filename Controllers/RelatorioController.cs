using Microsoft.AspNetCore.Mvc;
using MyProduto.Services;

namespace MyProduto.Controllers;

public class RelatorioController : Controller
{
    private readonly IProdutoService _produtoService;
    private readonly IRelatorioService _relatorioService;

    public RelatorioController(
        IRelatorioService relatorioService, 
        IProdutoService produtoService)
    {
        _relatorioService = relatorioService;
        _produtoService = produtoService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ProdutosEmEstoque()
    {
        var produtos = _produtoService.GetProdutosEmEstoque();
        var tituloRelatorio = "Relatório de Produtos em Estoque";

        var pdf = _relatorioService.GerarRelatorioProdutosEmEstoque(produtos, tituloRelatorio);
        return File(pdf, "application/pdf", "Relatorio_ProdutosEmEstoque.pdf");
    }

    public IActionResult ProdutosComEstoqueBaixo()
    {
        var produtos = _produtoService.GetProdutosComEstoqueBaixo();
        var tituloRelatorio = "Relatório de Produtos com Estoque Baixo";

        var pdf = _relatorioService.GerarRelatorioProdutosComEstoqueBaixo(produtos, tituloRelatorio);
        return File(pdf, "application/pdf", "Relatorio_ProdutosComEstoqueBaixo.pdf");
    }

    public IActionResult MovimentacaoEstoque()
    {
        var movimentacoes = _produtoService.GetMovimentacaoEstoque();
        var tituloRelatorio = "Relatório de Movimentação do Estoque";

        var pdf = _relatorioService.GerarRelatorioMovimentacaoEstoque(movimentacoes, tituloRelatorio);
        return File(pdf, "application/pdf", "Relatorio_MovimentacaoEstoque.pdf");
    }

    [HttpGet]
    public IActionResult ProdutosParados()
    {
        var produtosParados = _produtoService.GetProdutosParados();
        var pdfBytes = _relatorioService.GerarRelatorioProdutosParados(produtosParados, "Relatório de Produtos Parados");

        return File(pdfBytes, "application/pdf", "ProdutosParados.pdf");
    }
}
