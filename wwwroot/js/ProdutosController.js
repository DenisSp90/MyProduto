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
    debugger;
    // Aplica a máscara de moeda ao campo com id 'preco'
    $('#preco').mask('000.000.000.000.000,00', { reverse: true });
});