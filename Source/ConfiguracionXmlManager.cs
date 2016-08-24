using Ada.Framework.Configuration.Exceptions;
using Ada.Framework.Util.FileMonitor;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Ada.Framework.Configuration.Xml
{
    public abstract class ConfiguracionXmlManager<T>
    {
        private static T Configuracion;
        private static DateTime FechaUltimaModificacion;

        public abstract string NombreArchivoPorDefecto { get; }
        public abstract string NombreArchivoValidacionPorDefecto { get; }
        public abstract string NombreArchivoConfiguracion { get; }
        public abstract string NombreArchivoValidacionConfiguracion { get; }
        protected abstract bool ValidarXmlSchema { get; }

        public virtual string RutaArchivo
        {
            get
            {
                IMonitorArchivo monitor = MonitorArchivoFactory.ObtenerArchivo();
                string ruta = FrameworkConfigurationManager.ObtenerRutaArchivoConfiguracion(NombreArchivoConfiguracion);
                return monitor.ObtenerRutaArchivo(ruta, NombreArchivoPorDefecto);
            }
        }
        public virtual string RutaArchivoEsquema
        {
            get
            {
                IMonitorArchivo monitor = MonitorArchivoFactory.ObtenerArchivo();
                string ruta = FrameworkConfigurationManager.ObtenerRutaArchivoConfiguracion(NombreArchivoValidacionConfiguracion);
                return monitor.ObtenerRutaArchivo(ruta, NombreArchivoValidacionPorDefecto);
            }
        }
        
        protected abstract void ValidarXml(XmlDocument documento);

        public virtual T ObtenerConfiguracion()
        {
            return ObtenerConfiguracion(RutaArchivo);
        }

        public virtual T ObtenerConfiguracion(string ruta)
        {
            return ObtenerConfiguracion(null, ruta); 
        }
        
        public virtual T ObtenerConfiguracion(string rutaReferencial, string ruta)
        {
            try
            {
                IMonitorArchivo monitor = MonitorArchivoFactory.ObtenerArchivo();
                ruta = monitor.ObtenerRutaAbsoluta(rutaReferencial, ruta);

                DateTime fechaUltimaModificacionActual = monitor.ObtenerFechaUltimaModificacion(ruta);

                if (fechaUltimaModificacionActual > FechaUltimaModificacion)
                {
                    if (ValidarXmlSchema)
                    {
                        ValidarContraXmlSchema();
                    }
                    else
                    {
                        XmlDocument doc = ObtenerXmlDocument(ruta);
                        ValidarXml(doc);
                    }

                    XmlSerializer serializer = ObtenerXmlSerializer();

                    FileStream file = null;

                    try
                    {
                        file = ObtenerFileStreamForRead(ruta);
                        Configuracion = Deserialize(serializer, file);
                    }
                    finally
                    {
                        if (file != null)
                        {
                            file.Close();
                        }
                    }

                    FechaUltimaModificacion = fechaUltimaModificacionActual;
                }
                return Configuracion;
            }
            catch (Exception ex)
            {
                throw new AdaFrameworkConfigurationException(string.Format("!Error al obtener la configuración de {0}!. {1}", NombreArchivoConfiguracion, ex.Message), ex);
            }
        }

        public virtual void GuardarConfiguracion(T config)
        {
            GuardarConfiguracion(config, RutaArchivo);
        }

        public virtual void GuardarConfiguracion(T config, string ruta)
        {
            GuardarConfiguracion(config, null, ruta);
        }

        public virtual void GuardarConfiguracion(T config, string rutaReferencial, string ruta)
        {
            IMonitorArchivo monitor = MonitorArchivoFactory.ObtenerArchivo();
            ruta = monitor.ObtenerRutaAbsoluta(rutaReferencial, ruta);

            FileStream file = null;

            try
            {
                XmlSerializer serializer = ObtenerXmlSerializer();

                file = ObtenerFileStreamForWrite(ruta);
                Serialize(serializer, file, config);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        protected virtual void ValidarContraXmlSchema()
        {
            XmlDocument doc = new XmlDocument();
            XmlReader reader = null;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(string.Empty, RutaArchivoEsquema);
            settings.ValidationType = ValidationType.Schema;

            try
            {
                reader = XmlReader.Create(RutaArchivo, settings);
                doc.Load(reader);
                doc.Validate(ErrorAlValidar);
            }
            catch (XmlSchemaValidationException xmlValException)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                throw new AdaFrameworkConfigurationException(string.Format("!Archivo de configuración {0} erróneo!. {1}", NombreArchivoConfiguracion, xmlValException.Message));
            }
            catch (IOException ioE)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                throw new AdaFrameworkConfigurationException(string.Format("¡No se ha encontrado el archivo de configuración {0} o su esquema!", NombreArchivoConfiguracion), ioE);
            }
        }

        protected virtual void ErrorAlValidar(object sender, ValidationEventArgs e)
        {
            throw new AdaFrameworkConfigurationException(string.Format("!Archivo de configuración {0} erróneo!. {1}", NombreArchivoConfiguracion, e.Message));
        }

        protected virtual XmlDocument ObtenerXmlDocument(string ruta)
        {
            XmlDocument retorno = new XmlDocument();
            retorno.Load(ruta);
            return retorno;
        }

        protected virtual XmlSerializer ObtenerXmlSerializer()
        {
            XmlSerializer retorno = new XmlSerializer(typeof(T));
            return retorno;
        }

        protected virtual FileStream ObtenerFileStreamForRead(string ruta)
        {
            FileStream retorno = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return retorno;
        }

        protected virtual FileStream ObtenerFileStreamForWrite(string ruta)
        {
            FileStream retorno = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            return retorno;
        }

        protected virtual T Deserialize(XmlSerializer serializer, FileStream file)
        {
            return (T)serializer.Deserialize(file);
        }

        protected virtual void Serialize(XmlSerializer serializer, FileStream file, T obj)
        {
            serializer.Serialize(file, obj);
        }
    }
}