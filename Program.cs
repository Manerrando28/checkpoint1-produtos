using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Produto> produtos = new List<Produto>();

    static void Main(string[] args)
    {
        CarregarProdutos(); // carrega do CSV ao iniciar

        int opcao;
        do
        {
            Console.WriteLine("\n=== Sistema de Produtos ===");
            Console.WriteLine("1 - Cadastrar produto");
            Console.WriteLine("2 - Listar produtos");
            Console.WriteLine("3 - Buscar produto por ID ou Nome");
            Console.WriteLine("4 - Atualizar estoque");
            Console.WriteLine("5 - Calcular valor total em estoque");
            Console.WriteLine("6 - Excluir produto");
            Console.WriteLine("7 - Relatório geral");
            Console.WriteLine("8 - Ordenar produtos");
            Console.WriteLine("9 - Exportar para CSV");
            Console.WriteLine("10 - Importar de CSV");
            Console.WriteLine("0 - Sair (salva em CSV)");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida!");
                continue;
            }

            switch (opcao)
            {
                case 1: Cadastrar(); break;
                case 2: ListarProdutos(); break;
                case 3: BuscarProduto(); break;
                case 4: AtualizarEstoque(); break;
                case 5: CalcularValorTotal(); break;
                case 6: ExcluirProduto(); break;
                case 7: RelatorioGeral(); break;
                case 8: OrdenarProdutos(); break;
                case 9: SalvarProdutos(); break;
                case 10: ImportarCSV(); break;
                case 0:
                    SalvarProdutos();
                    Console.WriteLine("Saindo...");
                    break;
                default: Console.WriteLine("Opção inválida!"); break;
            }

        } while (opcao != 0);
    }

    // --- Métodos principais ---
    static void Cadastrar()
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine()!);
        Console.Write("Nome: ");
        string nome = Console.ReadLine()!;
        Console.Write("Preço: ");
        decimal preco = decimal.Parse(Console.ReadLine()!);
        Console.Write("Quantidade: ");
        int qtd = int.Parse(Console.ReadLine()!);

        if (produtos.Exists(p => p.Id == id))
        {
            Console.WriteLine("Erro: já existe um produto com esse ID!");
            return;
        }
        if (preco < 0 || qtd < 0)
        {
            Console.WriteLine("Erro: valores não podem ser negativos!");
            return;
        }

        produtos.Add(new Produto { Id = id, Nome = nome, Preco = preco, QuantidadeEmEstoque = qtd });
        Console.WriteLine($"Produto {nome} cadastrado com sucesso!");
    }

    static void ListarProdutos()
    {
        Console.WriteLine("\n--- Produtos cadastrados ---");
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }
        foreach (var p in produtos)
            Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Preço: {p.Preco} | Estoque: {p.QuantidadeEmEstoque}");
    }

    static void BuscarProduto()
    {
        Console.Write("Digite o ID ou Nome: ");
        string entrada = Console.ReadLine()!;
        Produto? encontrado = int.TryParse(entrada, out int idBusca)
            ? produtos.Find(p => p.Id == idBusca)
            : produtos.Find(p => p.Nome.Equals(entrada, StringComparison.OrdinalIgnoreCase));

        if (encontrado != null)
            Console.WriteLine($"Produto encontrado: {encontrado.Nome}, Estoque: {encontrado.QuantidadeEmEstoque}");
        else
            Console.WriteLine("Produto não encontrado.");
    }

    static void AtualizarEstoque()
    {
        Console.Write("Digite o ID: ");
        int id = int.Parse(Console.ReadLine()!);
        var p = produtos.Find(x => x.Id == id);
        if (p == null) { Console.WriteLine("Produto não encontrado."); return; }

        Console.Write("Nova quantidade: ");
        int qtd = int.Parse(Console.ReadLine()!);
        if (qtd < 0) { Console.WriteLine("Erro: quantidade negativa!"); return; }

        p.QuantidadeEmEstoque = qtd;
        Console.WriteLine("Estoque atualizado!");
    }

    static void CalcularValorTotal()
    {
        Console.Write("Digite o ID: ");
        int id = int.Parse(Console.ReadLine()!);
        var p = produtos.Find(x => x.Id == id);
        if (p == null) { Console.WriteLine("Produto não encontrado."); return; }

        Console.WriteLine($"Valor total em estoque: R$ {p.Preco * p.QuantidadeEmEstoque}");
    }

    static void ExcluirProduto()
    {
        Console.Write("Digite o ID: ");
        int id = int.Parse(Console.ReadLine()!);
        var p = produtos.Find(x => x.Id == id);
        if (p == null) { Console.WriteLine("Produto não encontrado."); return; }

        produtos.Remove(p);
        Console.WriteLine("Produto removido!");
    }

    static void RelatorioGeral()
    {
        if (produtos.Count == 0) { Console.WriteLine("Nenhum produto cadastrado."); return; }
        decimal valorTotal = 0; int qtdTotal = 0;
        foreach (var p in produtos)
        {
            decimal subtotal = p.Preco * p.QuantidadeEmEstoque;
            valorTotal += subtotal; qtdTotal += p.QuantidadeEmEstoque;
            Console.WriteLine($"{p.Nome} | Qtd: {p.QuantidadeEmEstoque} | Subtotal: R$ {subtotal}");
        }
        Console.WriteLine($"Total de itens: {qtdTotal}, Valor total: R$ {valorTotal}");
    }

    static void OrdenarProdutos()
    {
        Console.WriteLine("Ordenar por: 1-Nome, 2-Preço");
        string escolha = Console.ReadLine()!;
        List<Produto> ordenados = new List<Produto>(produtos);
        if (escolha == "1") ordenados.Sort((a, b) => a.Nome.CompareTo(b.Nome));
        else if (escolha == "2") ordenados.Sort((a, b) => a.Preco.CompareTo(b.Preco));
        else { Console.WriteLine("Opção inválida."); return; }

        foreach (var p in ordenados)
            Console.WriteLine($"{p.Id} | {p.Nome} | R$ {p.Preco}");
    }

    static void SalvarProdutos()
    {
        string caminho = Path.Combine(Directory.GetCurrentDirectory(), "produtos.csv");
        using (StreamWriter writer = new StreamWriter(caminho))
        {
            writer.WriteLine("ID;Nome;Preco;Quantidade;Subtotal");
            foreach (var p in produtos)
            {
                decimal subtotal = p.Preco * p.QuantidadeEmEstoque;
                writer.WriteLine($"{p.Id};{p.Nome};{p.Preco};{p.QuantidadeEmEstoque};{subtotal}");
            }
        }
        Console.WriteLine("Produtos salvos em produtos.csv");
    }

    static void ImportarCSV()
    {
        string caminho = Path.Combine(Directory.GetCurrentDirectory(), "produtos.csv");
        if (!File.Exists(caminho)) { Console.WriteLine("Arquivo não encontrado."); return; }

        string[] linhas = File.ReadAllLines(caminho);
        for (int i = 1; i < linhas.Length; i++)
        {
            string[] dados = linhas[i].Split(';');
            if (dados.Length < 5) continue;
            Produto p = new Produto
            {
                Id = int.Parse(dados[0]),
                Nome = dados[1],
                Preco = decimal.Parse(dados[2]),
                QuantidadeEmEstoque = int.Parse(dados[3])
            };
            if (!produtos.Exists(x => x.Id == p.Id))
                produtos.Add(p);
        }
        Console.WriteLine("Produtos importados do CSV!");
    }

    static void CarregarProdutos()
    {
        string caminho = Path.Combine(Directory.GetCurrentDirectory(), "produtos.csv");
        if (!File.Exists(caminho)) return;

        string[] linhas = File.ReadAllLines(caminho);
        for (int i = 1; i < linhas.Length; i++)
        {
            string[] dados = linhas[i].Split(';');
            if (dados.Length < 5) continue;
            Produto p = new Produto
            {
                Id = int.Parse(dados[0]),
                Nome = dados[1],
                Preco = decimal.Parse(dados[2]),
                 QuantidadeEmEstoque = int.Parse(dados[3])
            };

            // evita duplicar
            if (!produtos.Exists(x => x.Id == p.Id))
                produtos.Add(p);
        }
        Console.WriteLine("Produtos carregados do CSV com sucesso!");
    }
}