using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace Project
{
    public interface IRecomendable
    {
        string Id { get; }
        string Nombre { get; }
        IReadOnlyList<string> Atributos { get; }
    }
}