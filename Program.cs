using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDeAssinaturasAvancado
{
    public class Servico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double PrecoMensal { get; set; }

        public Servico(int id, string nome, double precoMensal)
        {
            Id = id;
            Nome = nome;
            PrecoMensal = precoMensal;
        }

        public override string ToString()
        {
            return $"\u001b[33mID: {Id}\u001b[0m | Nome: \u001b[36m{Nome}\u001b[0m | Preço Mensal: \u001b[32mR${PrecoMensal:F2}\u001b[0m";
        }
    }

    class Program
    {
        static List<Servico> servicos = new List<Servico>();
        static int proximoId = 1;

        static void Main(string[] args)
        {
            while (true)
            {
                MostrarMenu();
            }
        }

        static void MostrarMenu()
        {
            Console.Clear();
            EscreverComCor("=== Sistema Avançado de Assinaturas ===", ConsoleColor.Cyan);
            Console.WriteLine("1. Cadastrar Serviço");
            Console.WriteLine("2. Listar Serviços");
            Console.WriteLine("3. Excluir Serviço");
            Console.WriteLine("4. Ver Total Mensal");
            Console.WriteLine("5. Filtrar Serviços por Preço");
            Console.WriteLine("6. Sair");
            Console.Write("\nEscolha uma opção: ");

            string opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    CadastrarServico();
                    break;
                case "2":
                    ListarServicos();
                    break;
                case "3":
                    ExcluirServico();
                    break;
                case "4":
                    VerTotalMensal();
                    break;
                case "5":
                    FiltrarPorPreco();
                    break;
                case "6":
                    Sair();
                    break;
                default:
                    EscreverComCor("Opção inválida! Pressione Enter para tentar novamente.", ConsoleColor.Red);
                    Console.ReadLine();
                    break;
            }
        }

        static void CadastrarServico()
        {
            Console.Clear();
            EscreverComCor("=== Cadastro de Serviço ===", ConsoleColor.Cyan);

            Console.Write("Nome do Serviço: ");
            string nome = Console.ReadLine();

            Console.Write("Preço Mensal: R$ ");
            if (double.TryParse(Console.ReadLine(), out double preco) && preco > 0)
            {
                servicos.Add(new Servico(proximoId++, nome, preco));
                EscreverComCor("Serviço cadastrado com sucesso!", ConsoleColor.Green);
            }
            else
            {
                EscreverComCor("Preço inválido. Tente novamente.", ConsoleColor.Red);
            }

            Pausar();
        }

        static void ListarServicos()
        {
            Console.Clear();
            EscreverComCor("=== Lista de Serviços ===", ConsoleColor.Cyan);

            if (servicos.Count == 0)
            {
                EscreverComCor("Nenhum serviço cadastrado.", ConsoleColor.Yellow);
            }
            else
            {
                foreach (var servico in servicos)
                {
                    Console.WriteLine(servico);
                }
            }

            Pausar();
        }

        static void ExcluirServico()
        {
            Console.Clear();
            EscreverComCor("=== Excluir Serviço ===", ConsoleColor.Cyan);

            if (servicos.Count == 0)
            {
                EscreverComCor("Nenhum serviço cadastrado para excluir.", ConsoleColor.Yellow);
            }
            else
            {
                ListarServicos();
                Console.Write("\nDigite o ID do serviço para excluir: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var servico = servicos.FirstOrDefault(s => s.Id == id);
                    if (servico != null)
                    {
                        servicos.Remove(servico);
                        EscreverComCor($"Serviço '{servico.Nome}' excluído com sucesso!", ConsoleColor.Green);
                    }
                    else
                    {
                        EscreverComCor("Serviço não encontrado.", ConsoleColor.Red);
                    }
                }
                else
                {
                    EscreverComCor("ID inválido.", ConsoleColor.Red);
                }
            }

            Pausar();
        }

        static void VerTotalMensal()
        {
            Console.Clear();
            EscreverComCor("=== Total Mensal de Assinaturas ===", ConsoleColor.Cyan);

            double total = servicos.Sum(s => s.PrecoMensal);
            EscreverComCor($"Total mensal dos serviços cadastrados: R${total:F2}", ConsoleColor.Green);

            Pausar();
        }

        static void FiltrarPorPreco()
        {
            Console.Clear();
            EscreverComCor("=== Filtrar Serviços por Preço ===", ConsoleColor.Cyan);

            Console.Write("Digite o valor máximo: R$ ");
            if (double.TryParse(Console.ReadLine(), out double maxPreco) && maxPreco > 0)
            {
                var filtrados = servicos.Where(s => s.PrecoMensal <= maxPreco).ToList();
                if (filtrados.Count > 0)
                {
                    EscreverComCor("Serviços dentro do valor especificado:", ConsoleColor.Green);
                    foreach (var servico in filtrados)
                    {
                        Console.WriteLine(servico);
                    }
                }
                else
                {
                    EscreverComCor("Nenhum serviço encontrado para esse valor.", ConsoleColor.Yellow);
                }
            }
            else
            {
                EscreverComCor("Valor inválido. Tente novamente.", ConsoleColor.Red);
            }

            Pausar();
        }

        static void Sair()
        {
            Console.Clear();
            EscreverComCor("Obrigado por usar o Sistema Avançado de Assinaturas!", ConsoleColor.Green);
            Environment.Exit(0);
        }

        static void EscreverComCor(string texto, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(texto);
            Console.ResetColor();
        }

        static void Pausar()
        {
            Console.WriteLine("\nPressione Enter para voltar ao menu...");
            Console.ReadLine();
        }
    }
}
