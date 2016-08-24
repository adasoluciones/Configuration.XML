using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UnitTestProject1.Entities
{
    [XmlRoot]
    public class Configuracion
    {
        [XmlAttribute]
        public string FormatoFecha { get; set; }
        [XmlAttribute]
        public string IdiomaPorOmision { get; set; }
    }
}
