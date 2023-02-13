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
                
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost(Name = "SendMessage")]
        public string SendMessage(string toNumber, string body)
        {
#if DEBUG
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
#else
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
#endif
            var accountSid = MyConfig.GetValue<string>("Twilio:accountSid");
            var authToken = MyConfig.GetValue<string>("Twilio:authToken");

            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber($"whatsapp:+55{toNumber}"))
            {
                From = new PhoneNumber("whatsapp:+14155238886"),
                Body = body
            };

            var message = MessageResource.Create(messageOptions);
            
            _logger.Log(LogLevel.Information, $"Mensagem enviada. Id{message.Sid}");

            return message.Sid;          
        }
    }
}
