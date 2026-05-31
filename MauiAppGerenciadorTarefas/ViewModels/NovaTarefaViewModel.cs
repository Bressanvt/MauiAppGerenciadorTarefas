using System.Windows.Input;
using MauiAppGerenciadorTarefas.Models;
using MauiAppGerenciadorTarefas.Services;

namespace MauiAppGerenciadorTarefas.ViewModels;

public class NovaTarefaViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private string _nome = string.Empty;
    private string _descricao = string.Empty;
    private DateTime _data;

    public string Nome
    {
        get => _nome;
        set => SetProperty(ref _nome, value);
    }

    public string Descricao
    {
        get => _descricao;
        set => SetProperty(ref _descricao, value);
    }

    public DateTime Data
    {
        get => _data;
        set => SetProperty(ref _data, value);
    }

    public ICommand SalvarCommand { get; }
    public ICommand VoltarCommand { get; }

    public NovaTarefaViewModel(DatabaseService db)
    {
        _db = db;
        _data = DateTime.Today;
        SalvarCommand = new Command(async () => await SalvarAsync());
        VoltarCommand = new Command(async () => await VoltarAsync());
    }

    private async Task SalvarAsync()
    {
        if (string.IsNullOrWhiteSpace(Nome))
        {
            if (Shell.Current is Page page)
                await page.DisplayAlertAsync("Atenção", "Informe o nome da tarefa.", "OK");
            return;
        }

        var tarefa = new Tarefa
        {
            Nome = Nome,
            Descricao = Descricao ?? "",
            Data = Data,
            Concluida = false
        };

        await _db.SaveAsync(tarefa);
        await Shell.Current.GoToAsync("///lista");
    }

    private async Task VoltarAsync()
    {
        await Shell.Current.GoToAsync("///lista");
    }
}
