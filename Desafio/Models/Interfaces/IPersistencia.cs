using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models.Interfaces
{
    public interface IPersistencia
    {
        void SalvarDados();
        void CarregarDados();
    }
}