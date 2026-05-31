using MauiAppGerenciadorTarefas.ViewModels;

namespace MauiAppGerenciadorTarefas.Views;

public partial class NovaTarefaPage : ContentPage
{
    public NovaTarefaPage(NovaTarefaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
