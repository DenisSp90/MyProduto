function confirmDelete(id) {
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
                url: '/Produtos/DeleteConfirmed',
                type: 'POST',
                data: {
                    id: id},
                success: function (response) {
                    if (response.success) {
                        Swal.fire(
                            'Excluído!',
                            'O item foi excluído com sucesso.',
                            'success'
                        ).then(() => {
                            window.location.reload();
                        });
                    } else {
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
    $('#preco').mask('000.000.000.000.000,00', { reverse: true });
});