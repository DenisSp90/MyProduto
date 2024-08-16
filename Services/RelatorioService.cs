using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;
using MyProduto.Data;
using MyProduto.Models;

namespace MyProduto.Services;

public interface IRelatorioService
{   
    byte[] GerarRelatorioProdutosEmEstoque(IEnumerable<Produto> produtos, string tituloRelatorio);
    byte[] GerarRelatorioProdutosComEstoqueBaixo(IEnumerable<Produto> produtos, string tituloRelatorio);
    byte[] GerarRelatorioMovimentacaoEstoque(IEnumerable<MovimentacaoEstoque> movimentacoes, string tituloRelatorio);
    byte[] GerarRelatorioProdutosParados(IEnumerable<Produto> produtos, string tituloRelatorio);
}

public class RelatorioService : IRelatorioService
{
    private readonly ApplicationDbContext _context;

    public RelatorioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public byte[] GerarRelatorioProdutosEmEstoque(IEnumerable<Produto> produtos, string tituloRelatorio)
    {
        using (var stream = new MemoryStream())
        {
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Cabeçalho centralizado
            Paragraph header = new Paragraph(tituloRelatorio)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetBold();
            document.Add(header);

            // Espaçamento
            document.Add(new Paragraph("\n"));

            // Criação da tabela
            Table table = new Table(new float[] { 1, 3, 2, 2 });
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Cabeçalhos da Tabela
            table.AddHeaderCell(new Cell().Add(new Paragraph("ID")).SetBorder(Border.NO_BORDER).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Nome")).SetBorder(Border.NO_BORDER).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Preço")).SetBorder(Border.NO_BORDER).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Quantidade em Estoque")).SetBorder(Border.NO_BORDER).SetBold());

            // Adicionar linhas de produtos
            foreach (var produto in produtos)
            {
                table.AddCell(new Cell().Add(new Paragraph(produto.Id.ToString())).SetBorder(new SolidBorder(1)));
                table.AddCell(new Cell().Add(new Paragraph(produto.Nome)).SetBorder(new SolidBorder(1)));
                table.AddCell(new Cell().Add(new Paragraph(produto.Preco.ToString("C"))).SetBorder(new SolidBorder(1)));
                table.AddCell(new Cell().Add(new Paragraph(produto.QuantidadeEstoque.ToString())).SetBorder(new SolidBorder(1)));
            }

            // Adicionar tabela ao documento
            document.Add(table);

            document.Close();
            return stream.ToArray();
        }
    }

    public byte[] GerarRelatorioProdutosComEstoqueBaixo(IEnumerable<Produto> produtos, string tituloRelatorio)
    {
        using (var stream = new MemoryStream())
        {
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Cabeçalho centralizado
            Paragraph header = new Paragraph(tituloRelatorio)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetBold();
            document.Add(header);

            // Espaçamento
            document.Add(new Paragraph("\n"));

            // Criação da tabela
            Table table = new Table(new float[] { 1, 3, 2, 2 });
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Cabeçalhos da Tabela
            table.AddHeaderCell(new Cell().Add(new Paragraph("ID")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Nome")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Preço")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Estoque Atual")).SetBold());

            // Adicionar linhas de produtos
            foreach (var produto in produtos)
            {
                table.AddCell(new Cell().Add(new Paragraph(produto.Id.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(produto.Nome)));
                table.AddCell(new Cell().Add(new Paragraph(produto.Preco.ToString("C"))));
                table.AddCell(new Cell().Add(new Paragraph(produto.QuantidadeEstoque.ToString())));
            }

            // Adicionar tabela ao documento
            document.Add(table);

            document.Close();
            return stream.ToArray();
        }
    }

    public byte[] GerarRelatorioMovimentacaoEstoque(IEnumerable<MovimentacaoEstoque> movimentacoes, string tituloRelatorio)
    {
        using (var stream = new MemoryStream())
        {
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Cabeçalho centralizado
            Paragraph header = new Paragraph(tituloRelatorio)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetBold();
            document.Add(header);

            // Espaçamento
            document.Add(new Paragraph("\n"));

            // Criação da tabela
            Table table = new Table(new float[] { 1, 2, 2, 2, 3 });
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Cabeçalhos da Tabela
            table.AddHeaderCell(new Cell().Add(new Paragraph("ID")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Produto")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Data")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Quantidade")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Tipo Movimentação")).SetBold());

            // Adicionar linhas de movimentações
            foreach (var movimentacao in movimentacoes)
            {
                table.AddCell(new Cell().Add(new Paragraph(movimentacao.Id.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(movimentacao.Produto.Nome)));
                table.AddCell(new Cell().Add(new Paragraph(movimentacao.DataMovimentacao.ToString("dd/MM/yyyy"))));
                table.AddCell(new Cell().Add(new Paragraph(movimentacao.Quantidade.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(movimentacao.TipoMovimentacao)));
            }

            // Adicionar tabela ao documento
            document.Add(table);

            document.Close();
            return stream.ToArray();
        }
    }

    public byte[] GerarRelatorioProdutosParados(IEnumerable<Produto> produtos, string tituloRelatorio)
    {
        using (var stream = new MemoryStream())
        {
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Cabeçalho centralizado
            Paragraph header = new Paragraph(tituloRelatorio)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetBold();
            document.Add(header);

            // Espaçamento
            document.Add(new Paragraph("\n"));

            // Criação da tabela
            Table table = new Table(new float[] { 1, 3, 2, 2 });
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Cabeçalhos da Tabela
            table.AddHeaderCell(new Cell().Add(new Paragraph("ID")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Nome")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Preço")).SetBold());
            table.AddHeaderCell(new Cell().Add(new Paragraph("Quantidade em Estoque")).SetBold());

            // Adicionar linhas de produtos
            foreach (var produto in produtos)
            {
                table.AddCell(new Cell().Add(new Paragraph(produto.Id.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(produto.Nome)));
                table.AddCell(new Cell().Add(new Paragraph(produto.Preco.ToString("C"))));
                table.AddCell(new Cell().Add(new Paragraph(produto.QuantidadeEstoque.ToString())));
            }

            // Adicionar tabela ao documento
            document.Add(table);

            document.Close();
            return stream.ToArray();
        }
    }
}