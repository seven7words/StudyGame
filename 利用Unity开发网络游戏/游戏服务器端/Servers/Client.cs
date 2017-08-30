using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Model;
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
        private User user;
        private Room room;
        private Result result;

        public MySqlConnection MySQLConn
        {
            get { return mysqlConn; }
        }



        public Room Room
        {
            set { room = value; }
            get { return room; }
        }
        public void SetUserData(User user, Result result)
        {
            this.user = user;
            this.result = result;
        }

        public string GetUserData()
        {
            return user.Id+","+ user.Username + "," + result.TotalCount + "," + result.WinCount;
        }

        public int GetUserId()
        {
            return user.Id;
        }
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
            if (clientSocket == null || clientSocket.Connected == false) return;
            clientSocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainSize,SocketFlags.None, ReceiveCallBack,null);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false) return;
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
            if (room != null)
            {
                room.QuitRoom(this);
            }
            server.RemoveClient(this);
           
        }

        public void Send(ActionCode actionCode, string data)
        {
            try
           {
                byte[] bytes = Message.PackData(actionCode, data);
                clientSocket.Send(bytes);
             }catch (Exception e)
            {
                Console.WriteLine("无法发送消息:" + e);
             }

        }

        public bool IsHouseOwner()
        {
            return Room.IsHouseOwner(this);
        }
    }
}
