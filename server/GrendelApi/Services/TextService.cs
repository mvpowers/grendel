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
        Task SendSessionExpireText(long recipientPhone);
        Task SendSessionExpireTexts(List<long> recipientPhones);
        Task SendPasswordResetText(long recipientPhone, string passwordResetToken);
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
                    body: $"Time to vote for STW: {_appSettings.AppUrl}",
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
        
        public async Task SendSessionExpireText(long recipientPhone)
        {
            try
            {
                var message = await MessageResource.CreateAsync(
                    body: $"The results are in for STW: {_appSettings.AppUrl}/result",
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
        
        public async Task SendSessionExpireTexts(List<long> recipientPhones)
        {
            TwilioClient.Init(_appSettings.TwilioSid, _appSettings.TwilioAuthToken);
            
            foreach (var recipientPhone in recipientPhones)
            {
                await SendSessionExpireText(recipientPhone);
            }
        }
        
        public async Task SendPasswordResetText(long recipientPhone, string passwordResetToken)
        {
            try
            {
                TwilioClient.Init(_appSettings.TwilioSid, _appSettings.TwilioAuthToken);
                
                var message = await MessageResource.CreateAsync(
                    body: $"Set your STW password: {_appSettings.AppUrl}/reset?token={passwordResetToken}",
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
    }
}