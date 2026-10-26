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
    /// <summary>
    /// Clase de prueba unitaria encargada de que funcione la Historia de Usuario 3
    /// </summary>
    [TestFixture]
    public class HU3_Test
    {
        /// <summary>
        /// Prueba unitaria que verifica que el sistema devuelve una lista de recomendaciones
        /// un usuario tiene que estar registrado y el ítem la informacion necesaria para que salga todo bien
        /// </summary>
        [Test]
        public void ObservarRecomendaciones_HU3()
        {
            Catalogo catalogo= new Catalogo();
            MotorRecomendacion motor= new MotorRecomendacion();
            SistemaFachada fachada = new SistemaFachada(catalogo, motor);
            
                string usuarioId= "Cabecitacarta";
                fachada.RegistrarUsuario(usuarioId, "Cabecita");
                List<IRecomendable> recomendaciones= fachada.ObtenerRecomendaciones(usuarioId);
                Assert.IsNotNull(recomendaciones, "El siste debe mostrar una lista de recomendaciones");
                
                foreach(IRecomendable item in recomendaciones)
            {
                Assert.IsNotNull(item.Nombre, "El item debe mostrar su nombre");
                Assert.IsNotNull(item.Atributos, "El item debe de mostrar sus atributos");
            }
        }
    }
}