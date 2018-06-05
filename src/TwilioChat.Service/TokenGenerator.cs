using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Twilio.Jwt.AccessToken;
using TwilioChat.Common;

namespace TwilioChat.Service
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TwilioAccount _account;
        public TokenGenerator(IOptions<TwilioAccount> account)
        {
            _account = account.Value;
        }
        public Task<string> Generate(string identity, string endpointId)
        {
            var grants = new HashSet<IGrant>();

            if (_account.ChatServiceSid != string.Empty)
            {
                var chatGrant = new ChatGrant
                {
                    ServiceSid = _account.ChatServiceSid,
                    EndpointId = endpointId
                };

                grants.Add(chatGrant);
            }

            var token = new Token(
                _account.AccountSid,
                _account.ApiKey,
                _account.ApiSecret,
                identity,
                grants: grants
            ).ToJwt();

            return Task.FromResult(token);
        }
    }
}
