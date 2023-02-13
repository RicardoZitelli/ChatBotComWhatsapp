using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ChatBotComWhatsapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Your Account SID and Auth Token from twilio.com/console 
        const string accountSid = "AC7516404a3b4544f7fa2683f11d7aee58";
        const string authToken = "b92297d712a042ad092645acb25e648e";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
              

        [HttpPost(Name = "SendMessage")]
        public string SendMessage(string toNumber, string body)
        {
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber($"whatsapp:+55{toNumber}"));            
            messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            messageOptions.Body = body;

            var message = MessageResource.Create(messageOptions);
            
            _logger.Log(LogLevel.Information, $"Mensagem enviada. Id{message.Sid}");

            return message.Sid;          
        }
    }
}
