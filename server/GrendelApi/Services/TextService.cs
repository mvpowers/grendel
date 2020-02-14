using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelData;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace GrendelApi.Services
{
    public interface ITextService
    {
        Task SendSessionStartText(long recipientPhone);
        Task SendSessionStartTexts(List<long> recipientPhones);
    }
    
    public class TextService : ITextService
    {
        private readonly ILogger<TextService> _logger;
        private readonly IAppSettings _appSettings;

        public TextService(ILogger<TextService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task SendSessionStartText(long recipientPhone)
        {
            try
            {
                var message = await MessageResource.CreateAsync(
                    body: "hello there",
                    from: new Twilio.Types.PhoneNumber(_appSettings.TwilioSendNumber),
                    to: new Twilio.Types.PhoneNumber($"+1{recipientPhone}")
                );
                
                _logger.LogDebug($"Text sent: {message.Sid}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Text failed: {e.Message}");
            }

        }
        
        public async Task SendSessionStartTexts(List<long> recipientPhones)
        {
            TwilioClient.Init(_appSettings.TwilioSid, _appSettings.TwilioAuthToken);
            
            foreach (var recipientPhone in recipientPhones)
            {
                await SendSessionStartText(recipientPhone);
            }
        }
    }
}