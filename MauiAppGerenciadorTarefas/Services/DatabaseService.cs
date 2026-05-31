using SQLite;
using MauiAppGerenciadorTarefas.Models;

namespace MauiAppGerenciadorTarefas.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _connection;

    private async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (_connection is not null)
            return _connection;

        var path = Path.Combine(FileSystem.AppDataDirectory, "tarefas.db3");
        _connection = new SQLiteAsyncConnection(path);
        await _connection.CreateTableAsync<Tarefa>();
        return _connection;
    }

    public async Task<List<Tarefa>> GetAllAsync()
    {
        var conn = await GetConnectionAsync();
        return await conn.Table<Tarefa>().ToListAsync();
    }

    public async Task<Tarefa> GetByIdAsync(int id)
    {
        var conn = await GetConnectionAsync();
        return await conn.Table<Tarefa>().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<int> SaveAsync(Tarefa tarefa)
    {
        var conn = await GetConnectionAsync();
        if (tarefa.Id == 0)
            return await conn.InsertAsync(tarefa);
        else
            return await conn.UpdateAsync(tarefa);
    }

    public async Task<int> DeleteAsync(Tarefa tarefa)
    {
        var conn = await GetConnectionAsync();
        return await conn.DeleteAsync(tarefa);
    }

    public async Task<List<Tarefa>> SearchAsync(string texto)
    {
        var conn = await GetConnectionAsync();
        return await conn.Table<Tarefa>()
            .Where(t => t.Nome.Contains(texto) || t.Descricao.Contains(texto))
            .ToListAsync();
    }
}
