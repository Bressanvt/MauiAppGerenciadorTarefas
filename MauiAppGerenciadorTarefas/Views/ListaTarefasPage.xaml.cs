using MauiAppGerenciadorTarefas.ViewModels;

namespace MauiAppGerenciadorTarefas.Views;

public partial class ListaTarefasPage : ContentPage
{
    private readonly ListaTarefasViewModel _viewModel;

    public ListaTarefasPage(ListaTarefasViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.CarregarCommand.Execute(null);
    }
}
