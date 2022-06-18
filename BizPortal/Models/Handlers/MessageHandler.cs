using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BizPortal.Models.Handlers
{
    /// <summary>
    /// http://weblogs.asp.net/fredriknormen/log-message-request-and-response-in-asp-net-webapi
    /// </summary>
    public abstract class MessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var corrId = string.Format("{0}{1}", DateTime.Now.Ticks, Thread.CurrentThread.ManagedThreadId);
            var requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);

            Guid consumerKey = Guid.Empty;
            if (request.Headers.Contains("Consumer-Key"))
            {
                Guid.TryParse(request.Headers.GetValues("Consumer-Key").FirstOrDefault(), out consumerKey);
            }
            
            var requestMessage = await request.Content.ReadAsByteArrayAsync();

            await IncommingMessageAsync(corrId, requestInfo, requestMessage, consumerKey);

            var response = await base.SendAsync(request, cancellationToken);

            byte[] responseMessage;

            if (response.IsSuccessStatusCode)
                responseMessage = await response.Content.ReadAsByteArrayAsync();
            else
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

            await OutgoingMessageAsync(corrId, requestInfo, responseMessage, consumerKey);

            return response;
        }


        protected abstract Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message, Guid consumerKey);
        protected abstract Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message, Guid consumerKey);
    }
}