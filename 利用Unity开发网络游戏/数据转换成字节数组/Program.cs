using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据转换成字节数组
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = Encoding.UTF8.GetBytes("1");
            foreach (byte b in data)
            {
                Console.Write(b+" ");
            }
            Console.ReadKey();
        }
    }
}
