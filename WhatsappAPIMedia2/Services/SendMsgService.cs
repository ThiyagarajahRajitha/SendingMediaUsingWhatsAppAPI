using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace WhatsappAPIMedia2.Service
{
    public class SendMsgService
    {
        private UploadImageService _uploadImageService;
        public SendMsgService()
        {
            _uploadImageService = new UploadImageService();
        }
        public async Task SendingMsgAsync(string phoneNum)
        {

            var imagId = await _uploadImageService.UploadImage();
            var msg = new
            {
                messaging_product = "whatsapp",
                to = phoneNum,
                type = "template",
                template = new
                {
                    name = "invoiceimage",
                    language = new
                    {
                        code = "en_US"
                    },
                    components = new dynamic[]
                                    {
                                        new{
                                            type = "header",
                                            parameters = new[]
                                                {
                                                    new
                                                        {
                                                        type = "image",
                                                        image = new {
                                                                id = imagId
                                                                }
                                                        }
                                                }
                                            },
                                        new{
                                            type = "body",
                                            parameters = new[]
                                                {
                                                    new
                                                        {
                                                        type = "text",
                                                        text = 450

                                                        }
                                                }
                                            }

                                    }
                }

            };

            string json = JsonSerializer.Serialize(msg);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://graph.facebook.com/v15.0/112918351627372/messages";
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EAAMy0NJm8dkBADtW9ZA97sNQVJyWEkA6PoAHEZA1F3F9BZAk6YUxbM7e6dkv8osQMFWH68QnftdH3ZAdfMPpzfo03cycjBWOlmJuVQWXPxYnZCswVmWnLs5M2fdZBOOoNbHGdO7uXklgV3mSsOovvGw8mA7p3XFXMaSuzN3XF6JzODLiBcRyGtZCQxWqrT7UDzNuAOKIdxd7gZDZD");

            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();

        }
    }
}
