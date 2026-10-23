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
    public class HU11Test
    {
        [Test]
        public void IndicarSiMeGusto_HU11()
        {
            Catalogo catalogo = new Catalogo();
            MotorRecomendacion motor = new MotorRecomendacion();
            SistemaFachada fachada = new SistemaFachada (catalogo, motor);

            string usuarioId = "Lau44f";
            fachada.RegistrarUsuario(usuarioId, "Lautaro");

            string itemId="item1";
            IRecomendable item = new ItemPrueba(itemId, "de prueba");
            catalogo.Agregar(item);
            bool seRegistroConExito= true;
            try
            {
                fachada.RegistrarInteraccion(usuarioId, itemId, 7.0 );
            }
            catch (Exception)
            {
                seRegistroConExito= false;
            }
            Assert.IsTrue(seRegistroConExito, "El sistema debe permitir registrar que me gusto");
        }
        [Test]
        public void IndicarNoMeGusto_HU11()
        {
            Catalogo catalogo = new Catalogo();
            MotorRecomendacion motor = new MotorRecomendacion();
            SistemaFachada fachada = new SistemaFachada (catalogo, motor);

            string usuarioId = "Lau44f";
            fachada.RegistrarUsuario(usuarioId, "Lautaro");

            string itemId="item2";
            IRecomendable item = new ItemPrueba(itemId, "otra prueba");
            catalogo.Agregar(item);
            bool seRegistroConExito = true;

            try
            {
                fachada.RegistrarInteraccion(usuarioId, itemId, 1.0);
            }
            catch (Exception)
            {
                seRegistroConExito= false;
            }
            Assert.IsTrue(seRegistroConExito, "El sistema debe permitir registrar que no me gusto");
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