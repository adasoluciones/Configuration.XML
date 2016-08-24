using System;
using System.Runtime.Serialization;

namespace Ada.Framework.Configuration.Xml.Exceptions
{
    /// <summary>
    /// Clase que representa una excepción lanzada al validar un archivo de configuración XML y ser considerado inválido.
    /// </summary>
    /// <remarks>
    ///     Registro de versiones:
    ///     
    ///         1.0 15/02/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
    /// </remarks>
    [Serializable]
    public class InvalidXmlException : Exception
    {
        /// <summary>
        /// Constructor sin parámetros.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 15/02/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        public InvalidXmlException() : base() { }

        /// <summary>
        /// Constructor de la clase que proporciona un mensaje de descripción.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 15/02/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="mensaje">Mensaje de descripción.</param>
        public InvalidXmlException(string mensaje)
            : base(mensaje) { }

        /// <summary>
        /// Constructor de la clase que proporciona información de un valor serializable y su contexto.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 15/02/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="serializationInfo">Objeto que almacena todos los datos para serializar o deserializar un objeto.</param>
        /// <param name="streamingContext">Describe el origen y destino de una secuencia de serialización determinada y proporciona un contexto definido por el llamador adicional.</param>
        public InvalidXmlException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }

        /// <summary>
        /// Constructor de la clase que proporciona un mensaje de descripción, y la excepcion específica que la ocacionó.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 15/02/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="mensaje">Mensaje de descripción.</param>
        /// <param name="innerException">Excepción que la lazó.</param>
        public InvalidXmlException(string mensaje, Exception innerException)
            : base(mensaje, innerException) { }
    }
}
