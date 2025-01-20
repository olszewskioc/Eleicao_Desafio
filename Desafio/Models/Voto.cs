 using System;
using Desafio.Models.Usuarios;

namespace Desafio.Models
{
    public class Voto (Eleitor eleitor, Candidato candidato)
    {
        public Eleitor Eleitor { get; } = eleitor;
        public Candidato Candidato { get; } = candidato;
        public DateTime Data { get; } = DateTime.Now;

        
    }
}