using System;
using Desafio.Models.Enums;
using Desafio.Models;


namespace Desafio.Models.Usuarios
{
    public class Eleitor : Usuario
    {
        public Eleitor(string Nome, TipoUser Tipo, int ID, string Pin) : base(Nome, Tipo, ID, Pin)
        {
        }
        public void Votar(Eleicao eleicao)
        {
            Console.WriteLine($"Votando na eleição {eleicao.Nome}");
            
        }
    }
}