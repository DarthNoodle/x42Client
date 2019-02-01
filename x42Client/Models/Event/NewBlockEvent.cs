using System;

namespace x42Client.Models.Event
{
    public class NewBlockEvent: BaseEvent
    {
        public readonly BlockHeader Block;

        public NewBlockEvent(BlockHeader block)
        {
            Block = block;
            Time = DateTime.Now;
        }//end of public NewBlockEvent(BlockHeader block)
    }//end of public class NewBlockEvent : BaseEvent
}
