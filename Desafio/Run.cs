using System;
using System.Collections.Generic;
using Desafio.Models.Usuarios;
using Desafio.Models.Enums;
using Desafio.Models;

namespace Desafio
{
    public class Run
    {
        static void MenuAdm()
        {
            Console.Clear();
            Console.WriteLine("\nInforme uma opção:\n");
            Console.WriteLine("1 - Criar Eleição");
            Console.WriteLine("2 - Encerrar Eleição");
            Console.WriteLine("3 - Consultar Eleições");
            Console.WriteLine("4 - Cadastrar Candidato");
            Console.WriteLine("0 - Sair");
        }

        static void MenuEleitor()
        {
            Console.Clear();
            Console.WriteLine("\nInforme uma opção:\n");
            Console.WriteLine("1 - Escolher Eleição para Votar");
            Console.WriteLine("0 - Sair");
        }

        static bool Login(List<Eleitor> eleitores, int id, string pin, out Eleitor? eleitor)
        {
            eleitor = null;
            try
            {
                eleitor = eleitores.Find(u => u.ID == id) ?? throw new InvalidOperationException("Usuário não encontrado!");
                if (eleitor.CheckPin(pin))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("PIN incorreto!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return false;
            }
        }

        static void Main(string[] args)
        {
            List<Eleicao> eleicoes = new List<Eleicao>();
            Administrador admin = new Administrador("admin", TipoUser.Administrador, 0, "0000", eleicoes);
            List<Eleitor> eleitores = new List<Eleitor>
            {
                new Eleitor("Thiago", TipoUser.Eleitor, 1, "1234")
            };
            eleicoes.Add(new Eleicao(DateTime.Now, DateTime.Now.AddDays(1), "Eleição 1"));
            eleicoes[0].Candidatos.Add(new Candidato(1, "Candidato 1", Cargo.Presidente));

            string op = "";
            bool check = false;
            int id;
            string pin;
            Eleitor? currentEleitor = null;

            do
            {
                if (!check)
                {
                    Console.WriteLine("Informe o ID do usuário: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Informe o PIN do usuário: ");
                    pin = Console.ReadLine() ?? throw new InvalidOperationException("PIN obrigatório!");

                    if (id == admin.ID && pin == admin.Pin)
                    {
                        check = true;
                        Console.Clear();
                        Console.WriteLine("Login como Administrador bem-sucedido!");
                        do
                        {
                            MenuAdm();
                            op = Console.ReadLine()!;
                            switch (op)
                            {
                                case "1":
                                    Console.WriteLine("Criando Eleição...");
                                    admin.CriarEleicao();
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    Console.WriteLine("Encerrando Eleição...");
                                    for (int i = 0; i < eleicoes.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1} - {eleicoes[i].Nome}");
                                    }
                                    Console.Write("Informe o número da eleição escolhida: ");
                                    int el = Convert.ToInt32(Console.ReadLine());
                                    if (el > 0 && el <= eleicoes.Count)
                                    {
                                        admin.EncerrarEleicao(eleicoes[el - 1]); 
                                    }
                                    else
                                    {
                                        Console.WriteLine("Número de eleição inválido!");
                                    }

                                    Console.ReadKey();
                                    break;

                                case "3":
                                    Console.WriteLine("Consultando Eleições...");
                                    Console.ReadKey();
                                    break;

                                case "4":
                                    Console.WriteLine("Cadastrando Candidato...");
                                    for (int j = 0; j < eleicoes.Count; j++)
                                    {
                                        Console.WriteLine($"{j + 1} - {eleicoes[j].Nome}");
                                    }

                                    Console.Write($"Informe o número da eleição escolhida: ");
                                    int ele = Convert.ToInt32(Console.ReadLine());
                                    if (ele > 0 && ele <= eleicoes.Count)
                                    {
                                        admin.CadastrarCandidato(eleicoes[ele - 1]); 
                                    }
                                    else
                                    {
                                        Console.WriteLine("Número de eleição inválido!");
                                    }
                                    Console.ReadKey();
                                    break;
                                case "0":
                                    Console.WriteLine("Saindo do menu Administrador...");
                                    Console.ReadKey();
                                    break;
                                default:
                                    Console.WriteLine("Opção inválida!");
                                    Console.ReadKey();
                                    break;
                            }
                        } while (op != "0");
                        check = false;
                    }
                    else
                    {
                        check = Login(eleitores, id, pin, out currentEleitor);
                        if (check && currentEleitor != null)
                        {
                            Console.Clear();
                            Console.WriteLine($"Bem-vindo, {currentEleitor.Nome}!");
                            do
                            {
                                MenuEleitor();
                                op = Console.ReadLine()!;
                                switch (op)
                                {
                                    case "1":
                                        Console.WriteLine("Escolhendo Eleição para Votar...");
                                        for (int i = 0; i < eleicoes.Count; i++)
                                        {
                                            Console.WriteLine($"{i + 1} - {eleicoes[i].Nome}");
                                        }
                                        Console.Write("Informe o número da eleição escolhida: ");
                                        int el = Convert.ToInt32(Console.ReadLine());
                                        if (el > 0 && el <= eleicoes.Count)
                                        {
                                            currentEleitor.Votar(eleicoes[el - 1]); 
                                        }
                                        else
                                        {
                                            Console.WriteLine("Número de eleição inválido!");
                                        }
                                        Console.ReadKey();
                                        break;
                                    case "0":
                                        Console.WriteLine("Saindo do menu Eleitor...");
                                        Console.ReadKey();
                                        break;
                                    default:
                                        Console.WriteLine("Opção inválida!");
                                        Console.ReadKey();
                                        break;
                                }
                            } while (op != "0");
                            check = false;
                        }
                    }
                }
            } while (op != "0");
        }
    }
}
