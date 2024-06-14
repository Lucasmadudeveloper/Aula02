using System;
using System.Collections.Generic;

namespace EmpresaDeVendasDeTubos
{
    class Program
    {
        static void Main(string[] args)
        {
            GestorDeVendas gestor = new GestorDeVendas();

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Adicionar Produto");
                Console.WriteLine("2. Realizar Venda");
                Console.WriteLine("3. Mostrar Relatório de Vendas");
                Console.WriteLine("4. Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarProduto(gestor);
                        break;
                    case "2":
                        RealizarVenda(gestor);
                        break;
                    case "3":
                        gestor.MostrarRelatorioDeVendas();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarProduto(GestorDeVendas gestor)
        {
            Console.Write("Nome do Produto: ");
            string nome = Console.ReadLine();
            Console.Write("Preço do Produto: ");
            decimal preco = decimal.Parse(Console.ReadLine());

            Produto produto = new Produto(nome, preco);
            gestor.AdicionarProduto(produto);

            Console.WriteLine("Produto adicionado com sucesso!");
        }

        static void RealizarVenda(GestorDeVendas gestor)
        {
            Console.Write("Nome do Produto: ");
            string nome = Console.ReadLine();
            Console.Write("Quantidade Vendida: ");
            int quantidade = int.Parse(Console.ReadLine());

            bool sucesso = gestor.RealizarVenda(nome, quantidade);
            if (sucesso)
            {
                Console.WriteLine("Venda realizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Produto não encontrado ou quantidade insuficiente.");
            }
        }
    }

    class Produto
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public Produto(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
        }
    }

    class Venda
    {
        public Produto ProdutoVendido { get; set; }
        public int Quantidade { get; set; }
        public decimal Total => ProdutoVendido.Preco * Quantidade;

        public Venda(Produto produtoVendido, int quantidade)
        {
            ProdutoVendido = produtoVendido;
            Quantidade = quantidade;
        }
    }

    class GestorDeVendas
    {
        private List<Produto> produtos;
        private List<Venda> vendas;

        public GestorDeVendas()
        {
            produtos = new List<Produto>();
            vendas = new List<Venda>();
        }

        public void AdicionarProduto(Produto produto)
        {
            produtos.Add(produto);
        }

        public bool RealizarVenda(string nomeProduto, int quantidade)
        {
            Produto produto = produtos.Find(p => p.Nome == nomeProduto);
            if (produto != null)
            {
                Venda venda = new Venda(produto, quantidade);
                vendas.Add(venda);
                return true;
            }
            return false;
        }

        public void MostrarRelatorioDeVendas()
        {
            Console.WriteLine("Relatório de Vendas:");
            foreach (Venda venda in vendas)
            {
                Console.WriteLine($"Produto: {venda.ProdutoVendido.Nome}, Quantidade: {venda.Quantidade}, Total: {venda.Total:C}");
            }

            decimal totalGeral = 0;
            foreach (Venda venda in vendas)
            {
                totalGeral += venda.Total;
            }
            Console.WriteLine($"Total Geral de Vendas: {totalGeral:C}");
        }
    }
}
