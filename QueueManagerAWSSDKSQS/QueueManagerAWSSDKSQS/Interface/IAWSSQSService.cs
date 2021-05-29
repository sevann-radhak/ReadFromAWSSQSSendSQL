using QueueManagerAWSSDKSQS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueueManagerAWSSDKSQS.Interface
{
    public interface IAWSSQSService<T> where T : class
    {
        public Task<ICollection<AllMessage<T>>> GetAllMessagesAsync();
    }
}
