using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Common;
using UnityEngine;
/// <summary>
/// 用来管理跟服务器端的Socket连接
/// </summary>
public class ClientManager:BaseManager
{
    private const string IP = "127.0.0.1";
    private const int PORT = 6688;
    private Socket clientSocket;
    private Message msg = new Message();
    public ClientManager(GameFacade facade):base(facade)
    {

    }
    public override void OnInit()
    {
        base.OnInit();
        clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
            Start();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法连接到服务器端，请检查网络"+e);
            
        }
        
    }

    private void Start()
    {
        clientSocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainSize, SocketFlags.None, RecieveCallBack, null);
    }

    private void RecieveCallBack(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count,OnProcessDataCallBack);
            Start();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
           
        }
        
    }

    private void OnProcessDataCallBack(ActionCode actionCode, string data)
    {
        //TODO
        facade.HandleResponse(actionCode, data);
    }
    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭服务器端的连接，请检查网络" + e);
            
        }
    }
}
