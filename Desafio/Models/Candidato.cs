using System;
using Desafio.Models.Enums;

namespace Desafio.Models
{
    public class Candidato
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int Votos {get; set; }
        public Cargo Cargo { get; set; }
        
        public Candidato(int id, string nome, Cargo cargo)
        {
            ID = id;
            Nome = nome;
            Votos = 0;
            Cargo = cargo;
        }
        public void ReceberVoto()
        {
            Votos++;
        }
    }
}