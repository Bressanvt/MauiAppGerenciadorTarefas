using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiAppGerenciadorTarefas.Models;
using MauiAppGerenciadorTarefas.Services;

namespace MauiAppGerenciadorTarefas.ViewModels;

public class ListaTarefasViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private string _textoPesquisa = string.Empty;

    public ObservableCollection<Tarefa> Tarefas { get; set; } = new();

    public string TextoPesquisa
    {
        get => _textoPesquisa;
        set
        {
            SetProperty(ref _textoPesquisa, value);
            FiltrarCommand.Execute(null);
        }
    }

    public ICommand CarregarCommand { get; }
    public ICommand FiltrarCommand { get; }
    public ICommand ConcluirCommand { get; }
    public ICommand ExcluirCommand { get; }
    public ICommand NovaTarefaCommand { get; }

    public ListaTarefasViewModel(DatabaseService db)
    {
        _db = db;
        CarregarCommand = new Command(async () => await CarregarAsync());
        FiltrarCommand = new Command(async () => await FiltrarAsync());
        ConcluirCommand = new Command<Tarefa>(async t => await ConcluirAsync(t));
        ExcluirCommand = new Command<Tarefa>(async t => await ExcluirAsync(t));
        NovaTarefaCommand = new Command(async () => await NovaTarefaAsync());
    }

    public async Task CarregarAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var tarefas = await _db.GetAllAsync();
            Tarefas.Clear();
            foreach (var t in tarefas)
                Tarefas.Add(t);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task FiltrarAsync()
    {
        if (string.IsNullOrWhiteSpace(TextoPesquisa))
        {
            await CarregarAsync();
            return;
        }

        var tarefas = await _db.SearchAsync(TextoPesquisa);
        Tarefas.Clear();
        foreach (var t in tarefas)
            Tarefas.Add(t);
    }

    private async Task ConcluirAsync(Tarefa tarefa)
    {
        tarefa.Concluida = !tarefa.Concluida;
        await _db.SaveAsync(tarefa);
        var index = Tarefas.IndexOf(tarefa);
        if (index >= 0)
        {
            Tarefas.RemoveAt(index);
            Tarefas.Insert(index, tarefa);
        }
    }

    private async Task ExcluirAsync(Tarefa tarefa)
    {
        if (Shell.Current is not Page page) return;
        bool confirm = await page.DisplayAlertAsync(
            "Excluir", $"Deseja excluir '{tarefa.Nome}'?", "Sim", "Não");
        if (!confirm) return;

        await _db.DeleteAsync(tarefa);
        Tarefas.Remove(tarefa);
    }

    private async Task NovaTarefaAsync()
    {
        await Shell.Current.GoToAsync("///nova");
    }
}
