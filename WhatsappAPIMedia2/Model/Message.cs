using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WhatsappAPIMedia2.Model
{
    public class Message
    {
        public string messaging_product { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public Template template { get; set; }
    }

    public class Template
    {
        public string Name { get; set; }
        public Language language { get; set; }
    }

    public class Language
    {
        public string code { get; set; }
    }

    public class Component
    {
        public string type { get; set; }
        public Parameter parameter { get; set; }
    }

    public class Parameter
    {
        public string type { get; set; }
        public string text { get; set; }
        public Image id { get; set; }
    }

    public class Image
    {
        public int id { get; set; }
    }

}
