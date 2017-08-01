using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Tool;
using MySql.Data.MySqlClient;

namespace GameServer.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();
        private MySqlConnection mysqlConn;
        public Client()
        {
            
        }

        public Client(Socket clientSocket,Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            mysqlConn = ConnHelper.Connect();
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
                msg.ReadMessage(count,OnPrecessMessage);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
            
        }

        private void OnPrecessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandlerRequest(requestCode,actionCode,data,this);
        }
        private void Close()
        {
            ConnHelper.CloseConnection(mysqlConn);
            if(clientSocket!=null)
                clientSocket.Close();
            server.RemoveClient(this);
        }

        public void Send(RequestCode requestCode, string data)
        {
            byte[] bytes = Message.PackData(requestCode, data);
            clientSocket.Send(bytes);
        }
    }
}
