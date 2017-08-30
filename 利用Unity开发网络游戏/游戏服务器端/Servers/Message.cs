﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Servers
{
    class Message
    {
        private byte[] data = new byte[1024];
        private int startIndex = 0;//我们存取了多少个字节的数据

        public byte[] Data
        {
            get { return data; }
        }

        public int StartIndex
        {
            get { return startIndex; }
        }

        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }

        //public void AddCount(int count)
        //{
        //    startIndex += count;
        //}
        /// <summary>
        /// 读取数据
        /// </summary>
        public void ReadMessage(int newDataAmount,Action<RequestCode,ActionCode,string> processDataCallBack)
        {
            startIndex += newDataAmount;
            while (true)
            {
                if (startIndex <= 4)
                    return;
                int count = BitConverter.ToInt32(data, 0);
                if ((startIndex - 4) >= count)
                {
                    //string s = Encoding.UTF8.GetString(data, 4, count);
                    //Console.WriteLine("解析出来一挑数据" + s);
                    RequestCode requestCode =(RequestCode) BitConverter.ToInt32(data, 4);
                    ActionCode actionCode = (ActionCode) BitConverter.ToInt32(data, 8);
                    string s = Encoding.UTF8.GetString(data, 12, count - 8);
                    processDataCallBack(requestCode, actionCode, s);
                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }


        }

        public static byte[] PackData(ActionCode actionCode, string data)
        {
            byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = actionCodeBytes.Length + dataBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            byte[] newBytes = dataAmountBytes.Concat(actionCodeBytes).ToArray<byte>();//Concat(dataBytes);
            return newBytes.Concat(dataBytes).ToArray<byte>();
        }
    }
}
