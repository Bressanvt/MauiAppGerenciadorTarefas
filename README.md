# Gerenciador de Tarefas

Um app feito em .NET MAUI pra controlar suas tarefas do dia a dia. Nada muito complexo, mas funcional.

Feito como parte dos estudos de MAUI, pra praticar MVVM, SQLite e navegação com Shell.

## O que faz

- Lista as tarefas que vc cadastrou
- Adicionar tarefa nova com nome, descrição e data
- Marcar como concluída (só clicar em cima)
- Excluir com swipe pra esquerda
- Pesquisar pela barra de busca
- Tudo salvo localmente no celular, sem precisar de internet

## Como rodar

Precisa ter o .NET 10 SDK instalado, aí é só:

```
dotnet build
```

E rodar no emulador ou dispositivo. Android, iOS ou Windows.

## Estrutura

Nada de outro mundo, organização padrão de projeto MAUI:

```
Models/         - Tarefa.cs (a entidade, com os campos que o SQLite usa)
ViewModels/     - BaseViewModel (INotifyPropertyChanged genérico)
                - ListaTarefasViewModel (lógica da listagem)
                - NovaTarefaViewModel (lógica do cadastro)
Views/          - As páginas XAML (ListaTarefasPage, NovaTarefaPage)
Services/       - DatabaseService (CRUD no SQLite)
```

Usei o pacote sqlite-net-pcl pra persistência, e a arquitetura é MVVM (separation of concerns e tal).

## Tecnologias

- .NET MAUI (duh)
- SQLite (sqlite-net-pcl)
- MVVM com data binding e commands
- Shell navigation com TabBar
- Injeção de dependência (DatabaseService via singleton)

## Observação

App criado pra estudo, então pode ter coisa que dava pra fazer melhor. Mas funciona e cobre os conceitos principais que a prova vai cobrar.
