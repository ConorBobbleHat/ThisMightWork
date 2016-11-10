using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;

namespace ThisMightWork
{
    public class JSWebSocketServer : WebSocketBehavior
    {
        private void sendObj(object obj)
        {
            Send(JsonConvert.SerializeObject(obj));
        }

        /*protected override void OnMessage(MessageEventArgs e)
        {
           //this.sendObj("{a:2}");
        }*/

        public void SendDataFrame(object data)
        {
            var sendData = new DataPack();
            sendData.eventType = "onFrame";

            sendData.data = JsonConvert.SerializeObject(data);

            this.sendObj(sendData);
        }

        public void SendGestureData(object data)
        {
            var sendData = new DataPack();
            sendData.eventType = "onGesture";

            sendData.data = JsonConvert.SerializeObject(data);

            this.sendObj(sendData);
        }
    }
}
