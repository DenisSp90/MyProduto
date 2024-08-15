function confirmDelete(id) {
    debugger;
    Swal.fire({
        title: 'Tem certeza?',
        text: "Você não poderá reverter isso!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Produtos/DeleteConfirmed', // Substitua 'Controller' pelo nome real do seu controlador
                type: 'POST',
                data: {
                    id: id},
                success: function (response) {
                    debugger;

                    if (response.success) {
                        Swal.fire(
                            'Excluído!',
                            'O item foi excluído com sucesso.',
                            'success'
                        ).then(() => {
                            // Redirecionar ou atualizar a página
                            window.location.reload();
                        });
                    } else {
                        debugger;

                        Swal.fire(
                            'Erro!',
                            'Não foi possível excluir o item.',
                            'error'
                        );
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire(
                        'Erro!',
                        'Houve um problema ao tentar excluir o item.',
                        'error'
                    );
                }
            });
        }
    });
}

$(document).ready(function () {

    //$('#NomeProduto').on('blur', function () {
    //    debugger;


    //    var nome = $(this).val();
    //    var id = $('#ProdutoId').val(); // Supondo que você tenha um campo oculto para o ID do produto


    //    if (nome) {
    //        $.ajax({
    //            url: '/Produtos/CheckProdutoNome',
    //            type: 'GET',
    //            data: { nome: nome},
    //            success: function (exists) {
    //                debugger;

    //                if (exists) {
    //                    Swal.fire({
    //                        title: 'Produto Já Cadastrado',
    //                        text: 'Este nome de produto já está registrado. Deseja editar o produto existente?',
    //                        icon: 'warning',
    //                        showCancelButton: true,
    //                        confirmButtonText: 'Editar',
    //                        cancelButtonText: 'Continuar Cadastro'
    //                    }).then((result) => {
    //                        if (result.isConfirmed) {
    //                            // Redireciona para a view de edição com o ID do produto
    //                            window.location.href = '@Url.Action("Edit", "Produtos")?id=' + encodeURIComponent(id);
    //                        }
    //                    });
    //                }
    //            },
    //            error: function () {
    //                debugger;

    //                Swal.fire('Erro', 'Houve um erro ao verificar o nome do produto.', 'error');
    //            }
    //        });
    //    }
    //});
});