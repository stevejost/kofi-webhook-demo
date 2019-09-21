using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Webhook;
using KoFiWebhookDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KoFiWebhookDemo.Controllers
{
    public class WebhookController : Controller
    {
        private readonly FuzzConfig _fuzzConfig = null;
        public WebhookController(IConfiguration configuration)
        {
            this._fuzzConfig = configuration.GetSection("fuzz").Get<FuzzConfig>();
        }

        // in .net this is the attribute that says we receive a post @ this url.
        // the method takes a form parameter called "data" which is what the request from kofi looks like.
        [HttpPost("api/webhook/ko-fi")]
        public async Task<IActionResult> Kofi(string data)
        {

            // This is the discord webhook url from the configuration 
            var webhookClient = new DiscordWebhookClient(this._fuzzConfig.DiscordUrl);

            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            // Deserialize the kofi data using whatever json deserialization technique you use
            var kofiPacket = JsonConvert.DeserializeObject<KoFiData>(data, new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            });

            // Send a message to discord built from the kofi packet
            await webhookClient.SendMessageAsync($"{kofiPacket.FromName} donated {kofiPacket.Amount}! {kofiPacket.Message}");


            return this.Ok();
        }
    }
}