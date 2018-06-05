﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Sync.V1;
using TwilioChat.Common;
using TwilioChat.Service;

namespace TwilioChat.Web.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TwilioController : Controller
    {
        private readonly TwilioAccount _account;
        private readonly ITokenGenerator _tokenGenerator;
        public TwilioController(IOptions<TwilioAccount> account, ITokenGenerator tokenGenerator)
        {
            _account = account.Value;
            _tokenGenerator = tokenGenerator;

            //if (_account.SyncServiceSid == String.Empty)
            //{
                _account.SyncServiceSid = "default";
            //}

            TwilioClient.Init(
                _account.ApiKey,
                _account.ApiSecret,
                _account.AccountSid
            );

            ProvisionSyncDefaultService(_account.SyncServiceSid);

        }

        [HttpGet("[action]")]
        public async Task<JsonResult> GetToken(string identity)
        {
            if ( identity == null)
            {
                identity = randomUserId();
            }

            var endpoint = $"TwilioChatDemo:{identity}:browser";

            var token = await 
                _tokenGenerator
                .Generate(identity, endpoint)
                .ConfigureAwait(false);

            return Json(new { identity, token });
        }

        [HttpPost("[action]")]
        public async Task Create(Chat chat)
        {
            
            Console.WriteLine(chat.Author + " " + chat.Body);
        }

        private void ProvisionSyncDefaultService(string serviceSid)
        {
            if (_account.SyncServiceSid.Equals("default"))
            {
                ServiceResource.Fetch("default");
            }
        }
        private string randomUserId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}