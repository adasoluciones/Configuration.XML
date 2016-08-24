using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestProject1.Entities;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ManagerConfiguracionPrueba manager = new ManagerConfiguracionPrueba();
            Configuracion config = manager.ObtenerConfiguracion();
        }
    }
}
