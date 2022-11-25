using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using WhatsappAPIMedia2.Service;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace WhatsappAPIMedia2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageSendingController : ControllerBase
    {
        private SendMsgService sendMsgService;
        public MessageSendingController()
        {
            sendMsgService = new SendMsgService();
        }
        [HttpPost]
        public async Task<ActionResult> PostSendMsg(Contact contact)
        {
            await sendMsgService.SendingMsgAsync(contact.PhoneNo);
            return Ok();
        }   
    }
}  
    
