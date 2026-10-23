using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security;
using Microsoft.VisualBasic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Project;
namespace Library.Tests
{
    [TestFixture]
    public class  HU10Test
    {
        [Test]
        public void HU10_RegistrarInteraccion()
        {
            Catalogo catalogo = new Catalogo();

            MotorRecomendacion motor= new MotorRecomendacion();

            SistemaFachada fachada = new SistemaFachada(catalogo,motor);

            string usuarioId="xxjerexxdestruct";
            fachada.RegistrarUsuario(usuarioId, "Jeremias");

            string itemId="item1";


            IRecomendable item = new ItemPrueba(itemId, "El señor de los anillos");
            catalogo.Agregar(item);

            bool seRegistroSinProblemas = true;


            try
            {
                fachada.RegistrarInteraccion(usuarioId, itemId, 5.0);
            }
            catch (Exception ex)
            {
                seRegistroSinProblemas=false;
            }
            Assert.IsTrue(seRegistroSinProblemas, "El debería registrar la interacción");
        }
        public class ItemPrueba: IRecomendable
        {
            public string Id{get;set;}
            public string Nombre{get;set;}
            public IReadOnlyList<string> Atributos { get; set;} = new List<string>();
            public ItemPrueba(string id, string nombre)
            {
                Id=id;
                Nombre=nombre;
            }
            
        }
    }
}