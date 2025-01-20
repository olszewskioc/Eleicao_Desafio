using Desafio.Models.Enums;
using Desafio.Models;
using System.Text;
using System.Security.Cryptography;

namespace Desafio.Models.Usuarios
{
    public class Administrador : Usuario
    {
        public List<Eleicao> Eleicoes;
        public Administrador(string Nome, TipoUser Tipo, int ID, string Pin, List<Eleicao> eleicoes) : base(Nome, Tipo, ID, Pin)
        {
            Eleicoes = eleicoes;
        }
        public void CadastrarCandidato(Eleicao eleicao)
        {
            try
            {
                System.Console.WriteLine("Digite o nome do candidato: ");
                string nome = Console.ReadLine() ?? throw new ArgumentNullException("Nome candidato é obrigatório!");
                System.Console.WriteLine("Digite a data de inicio da eleição: ");
                DateTime inicio = DateTime.Parse(Console.ReadLine() ?? throw new ArgumentNullException("Data início obrigatória!"));
                System.Console.WriteLine("Digite o cargo do candidato: ");
                string cargoA = Console.ReadLine() ?? throw new ArgumentNullException("Cargo obrigatório!");
                Cargo cargo;

                if (!Enum.TryParse(cargoA, out cargo))
                {
                    Console.WriteLine("Cargo inválido!");
                    return;
                }

                eleicao.Candidatos.Add(new Candidato(eleicao.Candidatos.Count + 1, nome, cargo));
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void CriarEleicao()
        {
            try
            {
                System.Console.WriteLine("Digite a data de inicio da eleição: ");
                DateTime inicio = DateTime.Parse(Console.ReadLine() ?? throw new ArgumentNullException("Data início obrigatória!"));
                System.Console.WriteLine("Digite a data para finalizar a eleição: ");
                DateTime fim = DateTime.Parse(Console.ReadLine() ?? throw new ArgumentNullException("Data fim obrigatória!"));
                if (inicio >= fim)
                    throw new InvalidOperationException("Data fim deve ser maior que data início!");
                System.Console.WriteLine("Digite o nome da eleição: ");
                string nome = Console.ReadLine() ?? throw new ArgumentNullException("Nome eleição é obrigatório!");

                Eleicao eleicao = new Eleicao(inicio, fim, nome);
                Eleicoes.Add(eleicao);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void EncerrarEleicao(Eleicao eleicao)
        {
            eleicao.Encerrada = true;            
        }
        public void ConsultarEleicao()
        {

        }       


        static void EncryptFile(string password)
        {
            string VotosEncriptados = "VotosEncriptados.csv";
            string VotosEncriptadosTemporario = "VotosEncriptadosTemporario.csv";
            byte[] key = Encoding.UTF8.GetBytes(password.PadRight(32).Substring(0, 32));

            byte[] iv = Encoding.UTF8.GetBytes(password.PadRight(16).Substring(0, 16));

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (FileStream inputFileStream = new FileStream(VotosEncriptados, FileMode.Open))
                using (FileStream outputFileStream = new FileStream(VotosEncriptadosTemporario, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    inputFileStream.CopyTo(cryptoStream);
                }
            
            }
            File.Delete(VotosEncriptados);
            File.Move(VotosEncriptadosTemporario, VotosEncriptados);
        }

        static void DecryptFile(string password)
        {
            string VotosDesencriptados = "VotosDesencriptados.csv";
            string VotosDesencriptadosTemporario = "VotosDesencriptadosTemporario.csv";
            byte[] key = Encoding.UTF8.GetBytes(password.PadRight(32).Substring(0, 32));
            byte[] iv = Encoding.UTF8.GetBytes(password.PadRight(16).Substring(0, 16));

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (FileStream inputFileStream = new FileStream(VotosDesencriptados, FileMode.Open))
                using (FileStream outputFileStream = new FileStream(VotosDesencriptadosTemporario, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputFileStream);
                }
            }
        }

    }
}