using SQLite;

namespace MauiAppGerenciadorTarefas.Models;

public class Tarefa
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public DateTime Data { get; set; }

    public bool Concluida { get; set; }

    public string Status => Concluida ? "✅ Concluída" : "⏳ Pendente";

    public string CorStatus => Concluida ? "#4CAF50" : "#FF9800";
}
