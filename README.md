# Task Tracker CLI

Um aplicativo de linha de comando (CLI) em .NET para gerenciar tarefas.

---

## Requisitos

- .NET 8 SDK ou superior instalado.

---

## Como rodar o projeto

1. Clone ou extraia este repositório.  
2. No diretório raiz do projeto (onde está o arquivo `.csproj`), execute o comando a seguir:

```bash
dotnet build
dotnet run -- <Command> <Argument>
```

Após compilar o projeto é possível executar o binário diretamente no terminal:

**Windows**: bin/Release/net8.0/Task Tracker.exe

**Linux/macOS:** bin/Release/net8.0/Task Tracker

## Usando o projeto

### Adicionar uma nova tarefa

```bash
dotnet run -- add "Complete a code challenge!"
```

Saída:

```bash
Task added successfully (ID: 1)
```

### Atualizar uma tarefa

```bash
dotnet run -- update <id> "Updated description."
```

### Excluir uma tarefa

```bash
dotnet run -- delete <id>
```

### Visualizar tarefas

```bash
dotnet run -- list [todo|done|in-progress]
```

**Opções**:

- **todo** -> lista apenas as tarefas pendentes.

- **done** -> lista apenas as tarefas concluídas.

- **in-progress** -> lista apenas as tarefas em andamento.

- **(sem parâmetro)** -> lista todas as tarefas.

### Atualizar o status da tarefa

```bash
dotnet run -- mark-in-progress <id>
dotnet run -- mark-done <id>
```

## Observações

Os dados são armazenados em um arquivo database.json localizado na raiz do projeto.