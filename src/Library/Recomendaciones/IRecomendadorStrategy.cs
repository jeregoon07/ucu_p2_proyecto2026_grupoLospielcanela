using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace Project
{
    public interface IRecomendadorStrategy
    {
        IReadOnlyList<IRecomendable> Recomendar(Usuario usuario, Catalogo catalogo);
    }
}