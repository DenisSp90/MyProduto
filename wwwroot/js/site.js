// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', () => {
    const toggleSidebar = document.getElementById('toggle-sidebar');
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('main-content');

    // Toggle the sidebar visibility
    toggleSidebar.addEventListener('click', () => {
        sidebar.classList.toggle('hidden');
        mainContent.classList.toggle('full-width');
    });

    
    document.getElementById('darkModeToggle').addEventListener('click', function () {
        debugger;
        document.body.classList.toggle('dark-mode');
    });

    //// Carregar preferência ao carregar a página
    //if (localStorage.getItem('darkMode') === 'enabled') {
    //    document.body.classList.add('dark-mode');
    //}

    //// Alternar modo e salvar preferência
    //document.getElementById('darkModeToggle').addEventListener('click', function () {
    //    document.body.classList.toggle('dark-mode');
    //    localStorage.setItem('darkMode', document.body.classList.contains('dark-mode') ? 'enabled' : 'disabled');
    //});

    // Ocultar sidebar em telas menores ao carregar
    if (window.innerWidth <= 768) {
        sidebar.classList.add('hidden');
        mainContent.classList.add('full-width');
    }
});

$(document).ready(function () {
    $('#print-link').on('click', function (e) {
        e.preventDefault(); // Evita que o link execute sua ação padrão

        Swal.fire({
            title: 'Você deseja realizar a impressão?',
            text: "Você será redirecionado para a página de relatório.",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Sim, imprimir!',
            cancelButtonText: 'Não, cancelar',
        }).then((result) => {
            if (result.isConfirmed) {
                // Redireciona para a URL desejada
                window.location.href = 'Relatorio/MovimentacaoEstoque';
            }
        });
    });

    $('#estoque-link').on('click', function (e) {
        e.preventDefault(); // Evita que o link execute sua ação padrão

        Swal.fire({
            title: 'Você deseja visualizar os produtos em estoque?',
            text: "Você será redirecionado para a página de produtos em estoque.",
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim, visualizar!',
            cancelButtonText: 'Não, cancelar',
        }).then((result) => {
            if (result.isConfirmed) {
                // Redireciona para a URL desejada
                window.location.href = 'Relatorio/ProdutosEmEstoque';
            }
        });
    });

    $('#estoque-baixo-link').on('click', function (e) {
        e.preventDefault(); // Evita que o link execute sua ação padrão

        Swal.fire({
            title: 'Você deseja visualizar produtos com estoque baixo?',
            text: "Você será redirecionado para a página de produtos com estoque baixo.",
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim, visualizar!',
            cancelButtonText: 'Não, cancelar',
        }).then((result) => {
            if (result.isConfirmed) {
                // Redireciona para a URL desejada
                window.location.href = 'Relatorio/ProdutosComEstoqueBaixo';
            }
        });
    });

    $('#produtos-parados-link').on('click', function (e) {
        e.preventDefault(); // Evita que o link execute sua ação padrão

        Swal.fire({
            title: 'Você deseja visualizar produtos parados?',
            text: "Você será redirecionado para a página de produtos parados.",
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim, visualizar!',
            cancelButtonText: 'Não, cancelar',
        }).then((result) => {
            if (result.isConfirmed) {
                // Redireciona para a URL desejada
                window.location.href = 'Relatorio/ProdutosParados';
            }
        });
    });
});
