using System;
using Desafio.Models.Enums;

namespace Desafio.Models.Usuarios
{
    public abstract class Usuario
    {
        public string Nome {get; set;}
        public TipoUser Tipo {get; set;}
        public int ID {get; set;}
        public string Pin {get; set;}

        public Usuario(string nome, TipoUser tipo, int id, string pin)
        {
            Nome = nome;
            Tipo = tipo;
            ID = id;
            Pin = pin;
        }

        public bool CheckPin(string pin)
        {
            try
            {
                if (pin != Pin)
                    throw new InvalidOperationException("Pin inválido para esse usuário!");
                return true;
            }
            catch (System.Exception ex)
            {
                 Console.WriteLine($"Error: {ex.Message}");
                return false;                 
            }
        }       
    }
}