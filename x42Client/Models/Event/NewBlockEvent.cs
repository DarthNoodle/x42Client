using System;

namespace x42Client.Models.Event
{
    public class NewBlockEvent: EventArgs
    {
        public readonly Block Block;

        public NewBlockEvent(Block block) => Block = block;
    }
}
