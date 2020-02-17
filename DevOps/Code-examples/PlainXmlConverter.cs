using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SOConfigurationDeploy.ServiceLayer
{
    class PlainXmlConverter<T>
    {
        public string ConvertToPlainXml(T source)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww, settings))
                {
                    xmlSerializer.Serialize(writer, source, emptyNamespaces);
                    return sww.ToString(); // Your XML
                }
            }
        }
    }
}
