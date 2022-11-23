using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace WhatsappAPIMedia2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageSendingController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> PostSendMsg(Contact contact)
        {
            await SendingMsgAsync(contact.PhoneNo);
            return Ok();
        }

        private async Task SendingMsgAsync(string phoneNum)
        {
            var imagId = await UploadImage();
            var msg = new
            {
                messaging_product = "whatsapp",
                to = phoneNum,
                type = "template",
                template = new
                {
                    name = "invoice",
                    language = new
                    {
                        code = "en_US"
                    },
                    components = new[]
                                {
                                    new
                                    {
                                    type = "header",
                                    parameters = new[]
                                            { new
                                                {
                                                type = "image",
                                                image = new {
                                                            id = imagId
                                                        }
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

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EAAMy0NJm8dkBABfVmJBPxAMOWOQjG0fhIa2BEBMaiwMZA6EJn22IIbqZCrW6Qu9PXUVEFRnZAcQwmrhalzbk5gAZCqZBeifa51HyhNrNHO5rk89GKZASsNHcZCFB2TCdicZBQTU6ZAZAJtMhe9EJU1jf5XPxNAd73r6bZCXwwOPwLcHy2tjFqZBmXxVLE8vW3w7bLbTQTESdFEZBCxYBZAbUC39PVgDwSZCi3SSiy8ZD");

            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();

        }

        private async Task<string> UploadImage()
        {
            var filePath = @"C:\Users\Dell\Downloads\pexels-pixabay-60597.jpg";
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                //Load the file and set the file's content type header
                var fileStreamContent = new StreamContent(System.IO.File.OpenRead(filePath));
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                //Add file
                multipartFormContent.Add(new StringContent("whatsapp"), "messaging_product");
                multipartFormContent.Add(fileStreamContent, name: "file", fileName: "pexels-pixabay-60597.jpg");

                //Send it
                using var client = new HttpClient();
                var url = "https://graph.facebook.com/v15.0/112918351627372/media";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EAAMy0NJm8dkBABfVmJBPxAMOWOQjG0fhIa2BEBMaiwMZA6EJn22IIbqZCrW6Qu9PXUVEFRnZAcQwmrhalzbk5gAZCqZBeifa51HyhNrNHO5rk89GKZASsNHcZCFB2TCdicZBQTU6ZAZAJtMhe9EJU1jf5XPxNAd73r6bZCXwwOPwLcHy2tjFqZBmXxVLE8vW3w7bLbTQTESdFEZBCxYBZAbUC39PVgDwSZCi3SSiy8ZD");
                var response = await client.PostAsync(url, multipartFormContent);
                response.EnsureSuccessStatusCode();
                var image= await JsonSerializer.DeserializeAsync<Image>(await response.Content.ReadAsStreamAsync());
                return image.id;

            }


        }

        
    }

}  
    
