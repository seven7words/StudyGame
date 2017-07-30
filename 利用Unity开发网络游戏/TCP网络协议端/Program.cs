using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP网络协议端
{
    class Program
    {
        static void Main(string[] args)
        {
          
            StartServerAsync();
            Console.ReadKey();
        }

        static void StartServerAsync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse("192.168.6.110"), 5555));//绑定ip和端口号
            serverSocket.Listen(10);//开始监听端口号
                                   //Socket clientSocket = serverSocket.Accept();//接受一个客户端连接
            serverSocket.BeginAccept(AcceptCallBack,serverSocket);
           
        }
        static  Message msg = new Message();
        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket serverSocket  = ar.AsyncState as Socket;
            Socket clientSocket =   serverSocket.EndAccept(ar);
            //向客户端发送一条消息
            string msgStr = "Hello client!你好咩。。。";
            byte[] data = Encoding.UTF8.GetBytes(msgStr);
            clientSocket.Send(data);
            byte[] dataBuffet = new byte[1024];
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize , SocketFlags.None, ReceiveCallBack, clientSocket);
            serverSocket.BeginAccept(AcceptCallBack, serverSocket);
        }
         static byte[]dataBuffer = new byte[1024];
         static void ReceiveCallBack(IAsyncResult ar)
         {
             Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);//

                //string msgStr = Encoding.UTF8.GetString(dataBuffer, 0, count);
                //Console.WriteLine("从客户端接收到数据：" +count+ msgStr);
                if (count == 0)
                {
                    clientSocket.Close();
                    return;
                }
                 msg.AddCount(count);
                msg.ReadMessage();
                //clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);
                clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
            finally
            {
               
            }
           }
       static void StartServerSync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 192.168.1.5
            //IPAddress ipAddress = new IPAddress(new byte[] { 192.168.6.110 });
            ////IPAddress ipAddress = IPAddress.Parse("192.168.6.110");
            // IPEndPoint ipEndPoint = new IPEndPoint(ipAddress,88);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse("192.168.6.110"), 9999));//绑定ip和端口号
            serverSocket.Listen(0);//开始监听端口号
            Socket clientSocket = serverSocket.Accept();//接受一个客户端连接
            //向客户端发送一条消息
            string msg = "Hello client!你好咩。。。";
            byte[] data = Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(data);
            //接受客户端的一条消息
            byte[] dataBuffer = new byte[1024];
            int count = clientSocket.Receive(dataBuffer);
            string msgReceive = Encoding.UTF8.GetString(dataBuffer, 0, count);
            Console.WriteLine(msgReceive);

            clientSocket.Close();//关闭跟客户端协议
            serverSocket.Close();
        }

    }
}
