using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json.Serialization;

namespace WhatsappAPIMedia2.Model
{
    public class Message
    {
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; } = "whatsapp";

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "template";

        [JsonPropertyName("template")]
        public Template Template { get; set; }

        public Message Create(string to, Template template)
        {
            this.To = to;
            this.Template = template;
            return this;
        }
    }

    public class Template
    {
        [JsonPropertyName ("name")]
        public string Name { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; }

        [JsonPropertyName("components")]
        public Component[] Components { get; set; }
    }

    public class Language
    {
        [JsonPropertyName ("code")]
        public string Code { get; set; }
    }

    public class Component
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("parameters")]
        public Parameter[] Parameters { get; set; }
    }

    public class Parameter
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("text")]
        public int Text { get; set; }
        [JsonPropertyName("image")]
        public Image Image { get; set; }
    }
    public class Image
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
