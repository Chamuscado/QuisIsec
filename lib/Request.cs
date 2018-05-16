using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace lib
{
    [Serializable]
    public class Request
    {
        public RequestType Type { get; set; }
        public string Msg { get; set; }
        public List<object> List { get; set; }
        public object obj { get; set; }

        public string ToXml()
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(GetType());
            serializer.Serialize(stringwriter, this);
            return stringwriter.ToString();
        }

        public static Request LoadFromXmlString(string xmlText)
        {
            Request config;
            try
            {
                var stringReader = new StringReader(xmlText);
                var serializer = new XmlSerializer(typeof(Request));
                config = serializer.Deserialize(stringReader) as Request;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                config = new Request();
            }

            return config ?? (new Request());
        }

        public override string ToString()
        {
            return $"RequestType {Type} Mensagem: Msg {Msg} List: {List}";
        }
    }

    public enum RequestType
    {
        Time,
        ShowRightAnswer,
        End,
        Teams,
        Quest
    }
}