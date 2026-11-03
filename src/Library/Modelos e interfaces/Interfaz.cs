using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace Project
{
    /// <summary>
    /// Abstracción para los ítems recomendables
    /// </summary>
    public interface IRecomendable
    {
        string Id { get; }
        string Nombre { get; }
        public IReadOnlyList<string> Atributos { get; }
    }
}