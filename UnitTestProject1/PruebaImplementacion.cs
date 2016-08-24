using Ada.Framework.Configuration.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitTestProject1.Entities;
using System.Xml;

namespace UnitTestProject1
{
    public class ManagerConfiguracionPrueba : ConfiguracionXmlManager<Configuracion>
    {
        public override string NombreArchivoPorDefecto
        {
            get { return "Configuracion.Xml"; }
        }

        public override string NombreArchivoValidacionPorDefecto
        {
            get { return "Configuracion.xsd"; }
        }

        public override string NombreArchivoConfiguracion
        {
            get { return "ConfiguracionPrueba"; }
        }

        public override string NombreArchivoValidacionConfiguracion
        {
            get { return "ValidacionConfiguracionPrueba"; }
        }

        protected override bool ValidarXmlSchema
        {
            get { return true; }
        }

        protected override void ValidarXml(XmlDocument documento)
        {
            throw new NotImplementedException();
        }
    }
}
