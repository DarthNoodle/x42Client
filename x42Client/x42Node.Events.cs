using x42Client.Models.Event;
using x42Client.RestClient.Responses;
using x42Client.Utils.Extensions;
using x42Client.Utils.Validation;

namespace x42Client
{

    public delegate void NewBlockEventHandler(object sender, NewBlockEvent e);


    public partial class x42Node
    {
        /// <summary>
        /// Triggers When A New Block Is Detected
        /// </summary>
        public NewBlockEventHandler NewBlockEvent;


        public virtual async void OnNewBlock(ulong blockNumber)
        {
            GetBlockResponse blockData = await _RestClient.GetBlock(blockNumber);

            Guard.Null(blockData, nameof(blockData), $"Node '{Name}' ({Address}:{Port}) Detected A New Block @ Height '{blockNumber}' But GetBlock Returned NULL!");

            this.NewBlockEvent?.Invoke(this, new NewBlockEvent(blockData.ToBlock()));
        }//end of public virtual void OnNewBlock(ulong blockNumber)

    }//end of public partial class x42Node.Events
}
