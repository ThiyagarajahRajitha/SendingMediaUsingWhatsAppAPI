using System.Net.Http.Headers;
using System.Text.Json;

namespace WhatsappAPIMedia2.Service
{
    public class UploadImageService
    {
        public async Task<string> UploadImage()
        {
            var filePath = @"C:\Users\Dell\OneDrive - INEXIS CONSULTING\Desktop\invoicee.jpg";
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

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EAAMy0NJm8dkBADtW9ZA97sNQVJyWEkA6PoAHEZA1F3F9BZAk6YUxbM7e6dkv8osQMFWH68QnftdH3ZAdfMPpzfo03cycjBWOlmJuVQWXPxYnZCswVmWnLs5M2fdZBOOoNbHGdO7uXklgV3mSsOovvGw8mA7p3XFXMaSuzN3XF6JzODLiBcRyGtZCQxWqrT7UDzNuAOKIdxd7gZDZD");
                var response = await client.PostAsync(url, multipartFormContent);
                response.EnsureSuccessStatusCode();
                var image = await JsonSerializer.DeserializeAsync<Image>(await response.Content.ReadAsStreamAsync());
                return image.id;

            }


        }
    }
}
