using System.ComponentModel.DataAnnotations;

namespace MyProduto.Models;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    public string Nome { get; set; }

    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O campo 'Quantidade em Estoque' é obrigatório.")]
    public int QuantidadeEstoque { get; set; }

    [Required(ErrorMessage = "O campo 'Estoque minimo' é obrigatório.")]
    public int EstoqueMinimo { get; set; }

    public DateTime UltimaMovimentacao { get; set; }

    // Relacionamento 1:N com MovimentacaoEstoque
    public ICollection<MovimentacaoEstoque>? MovimentacoesEstoque { get; set; }
}

public class MovimentacaoEstoque
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public int Quantidade { get; set; }
    public string TipoMovimentacao { get; set; } // Entrada ou Saída

    // Relacionamento N:1 com Produto
    public Produto Produto { get; set; }
}