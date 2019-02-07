using System;
using System.Net;


namespace x42Client.Models.Event
{
    public class ConnectDisconnectEvent: BaseEvent
    {
        public readonly bool IsConnected = false;
        public readonly IPAddress Address;
        public readonly ushort Port;
        

        public ConnectDisconnectEvent(bool isConnected, IPAddress address, ushort port)
        {
            IsConnected = isConnected;
            Address = address;
            Port = port;
            Time = DateTime.Now;
        }
    }//end of public class ConnectDisconnectEvent: EventArgs
}
