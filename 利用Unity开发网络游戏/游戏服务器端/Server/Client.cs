using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Server
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();
        public Client()
        {
            
        }

        public Client(Socket clientSocket,Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
        }

        public void Start()
        {
            clientSocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainSize,SocketFlags.None, ReceiveCallBack,null);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                }
                //TODO 处理接收到的消息
                msg.ReadMessage(count);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
            
        }

        private void Close()
        {
            if(clientSocket!=null)
                clientSocket.Close();
            server.RemoveClient(this);
        }
    }
}
