using Amazon.SQS.Model;
using Newtonsoft.Json;
using QueueManagerAWSSDKSQS.Interface;
using QueueManagerAWSSDKSQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueManagerAWSSDKSQS.Service
{
    public class AWSSQSService<T> : IAWSSQSService<T> where T : class
    {
        private readonly IAWSSQSHelper _AWSSQSHelper;

        public AWSSQSService(IAWSSQSHelper AWSSQSHelper)
        {
            _AWSSQSHelper = AWSSQSHelper;
        }

        public async Task<ICollection<AllMessage<T>>> GetAllMessagesAsync()
        {
            try
            {
                ICollection<Message> messages = await _AWSSQSHelper.ReceiveMessageAsync();

                return messages
                    .Select(m => new AllMessage<T>
                    {
                        MessageId = m.MessageId,
                        ReceiptHandle = m.ReceiptHandle,
                        Body = JsonConvert.DeserializeObject<T>(m.Body)
                    })?
                    .ToList();
            }
            catch (Exception ex)
            {
                //TODO: what to to here?
                throw new Exception(ex.Message);
            }
        }
    }
}
