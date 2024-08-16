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
    $('#toggle-sidebar').on('click', function () {
        $('#sidebar').toggleClass('open');
        $('#main-content').toggleClass('shifted'); 
    });

    function setupAlert(id, action) {
        $(id).on('click', function (e) {
            e.preventDefault(); 

            Swal.fire({
                title: 'Você deseja continuar?',
                text: "Você será redirecionado para a página solicitada.",
                icon: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim, continuar!',
                cancelButtonText: 'Não, cancelar',
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = action;
                }
            });
        });
    }

    setupAlert('#print-link', '/Relatorio/MovimentacaoEstoque');
    setupAlert('#estoque-link', '/Relatorio/ProdutosEmEstoque');
    setupAlert('#estoque-baixo-link', '/Relatorio/ProdutosComEstoqueBaixo');
    setupAlert('#produtos-parados-link', '/Relatorio/ProdutosParados');
});
