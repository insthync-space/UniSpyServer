using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WebServer.Entity.Structure
{
    public class SoapXElement : XElement
    {
        private static XDeclaration _declaration = new XDeclaration("1.0", "utf-8", null);
        public static XNamespace SoapNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
        public static XElement SoapElement = new XElement(SoapNamespace + "Envelope",
                                new XAttribute(XNamespace.Xmlns + "SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/"),
                                new XAttribute(XNamespace.Xmlns + "SOAP-ENC", "http://schemas.xmlsoap.org/soap/encoding/"),
                                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"));
        public static XNamespace SakeNamespace = "http://gamespy.net/sake";
        public SoapXElement(XElement other) : base(other) { }

        public SoapXElement(XName name) : base(name) { }

        public SoapXElement(XStreamingElement other) : base(other) { }

        public SoapXElement(XName name, object content) : base(name, content) { }

        public SoapXElement(XName name, params object[] content) : base(name, content) { }
        public List<FieldObject> GetArrayObjects()
        {
            throw new NotImplementedException();
        }
        public XElement SetArrayObjects(List<FieldObject> objects)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return _declaration + Environment.NewLine + base.ToString();
        }
    }
}