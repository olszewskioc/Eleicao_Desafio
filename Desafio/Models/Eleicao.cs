using System;
using Desafio.Models.Usuarios;
using Desafio.Models.Interfaces;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Desafio.Models
{
    public class Eleicao : IPersistencia
    {
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Nome { get; set; }
        public List<Candidato> Candidatos { get; set; } = new List<Candidato>();
        public List<Voto> Votos { get; set; } = new List<Voto>();
        public List<int> Votantes { get; set; } = new List<int>();
        public bool Encerrada { get; set; } = false;

        public Eleicao(DateTime inicio, DateTime fim, string nome)
        {
            Inicio = inicio;
            Fim = fim;
            Nome = nome;
        }

        public void RegistrarVoto(Eleitor eleitor, int idCandidato)
        {
            try
            {
                if (DateTime.Now < Inicio || DateTime.Now > Fim || Encerrada)
                    throw new InvalidOperationException("A eleição não está ativa no momento.");

                if (Votantes.Contains(eleitor.ID))
                    throw new InvalidOperationException($"O eleitor {eleitor.Nome} já votou nesta eleição.");

                var candidato = Candidatos.FirstOrDefault(c => c.ID == idCandidato) ?? throw new ArgumentException("Candidato inválido.");
                candidato.ReceberVoto();
                Votos.Add(new Voto(eleitor, candidato));
                Votantes.Add(eleitor.ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public string GerarRelatorio()
        {
            var relatorio = new StringBuilder();
            relatorio.AppendLine($"Relatório da Eleição: {Nome}");
            relatorio.AppendLine($"Período: {Inicio} - {Fim}");

            foreach (var candidato in Candidatos)
            {
                relatorio.AppendLine($"Candidato: {candidato.Nome}, Cargo: {candidato.Cargo}, Votos: {candidato.Votos}");
            }

            return relatorio.ToString();
        }

        public void SalvarDados()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true
            };

            using (var writer = new StreamWriter($"{Nome}_candidatos.csv"))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(Candidatos);
            }

            using (var writer = new StreamWriter($"{Nome}_votos.csv"))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(Votos);
            }
        }

        public void CarregarDados()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true
            };

            if (File.Exists($"{Nome}_candidatos.csv"))
            {
                using (var reader = new StreamReader($"{Nome}_candidatos.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    Candidatos = csv.GetRecords<Candidato>().ToList();
                }
            }

            if (File.Exists($"{Nome}_votos.csv"))
            {
                using (var reader = new StreamReader($"{Nome}_votos.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    Votos = csv.GetRecords<Voto>().ToList();
                }
            }

            Votantes = Votos.Select(v => v.Eleitor.ID).Distinct().ToList();
        }
    }
}

// Código dado pelo Professor:
/* Segue 2 funções da classe Eleição:

 public void RegistrarVoto(Eleitor eleitor, int idCandidato)
    {
        if (DateTime.Now < Inicio || DateTime.Now > Fim)
            throw new InvalidOperationException("A eleição não está ativa no momento.");

        var candidato = Candidatos.FirstOrDefault(c => c.Id == idCandidato);
        if (candidato == null) throw new ArgumentException("Candidato inválido.");

        candidato.ReceberVoto();
        Votos.Add(new Voto(eleitor, candidato));
    }

    public string GerarRelatorio()
    {
        var relatorio = new StringBuilder();
        relatorio.AppendLine($"Relatório da Eleição: {Nome}");
        relatorio.AppendLine($"Período: {Inicio} - {Fim}");

        foreach (var candidato in Candidatos)
        {
            relatorio.AppendLine($"Candidato: {candidato.Nome}, Cargo: {candidato.Cargo}, Votos: {candidato.Votos}");
        }

        return relatorio.ToString();
    } */