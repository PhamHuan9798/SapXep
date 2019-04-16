using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Net.Sockets;

namespace SapXep
{
    class Program
    {
        static void Main(string[] args)
        {
            ReceiveFile();


            //ThucHien.TH();
            Console.ReadKey();
        }
        static void SendFile(string fileName, string filePath)
        {
            Console.WriteLine("Waiting to connect:");
            Console.WriteLine("--------------------");
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 5656);// khoi tao IP client
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            socket.Bind(ipEnd);
            socket.Listen(10);

            Socket client_socket = socket.Accept();
            Console.WriteLine("\n-----Connected--------");
            byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);

            byte[] fileData = File.ReadAllBytes(filePath + fileName);

            byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];

            byte[] fileNameBit = BitConverter.GetBytes(fileNameByte.Length);

            fileNameBit.CopyTo(clientData, 0);
            fileNameByte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileNameByte.Length);

            client_socket.Send(clientData);
        }
        static void ReceiveFile()
        {
            IPAddress[] ipAddress = Dns.GetHostAddresses("192.168.0.103");
            IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5656);

            Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            clientSock.Connect(ipEnd);

            byte[] clientData = new byte[1024 * 10000];
            string receivedPath = @"C:\";
            int receivedBytesLen = clientSock.Receive(clientData);
            int fileNameLen = BitConverter.ToInt32(clientData, 0);
            string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
            Console.WriteLine("Client:{0} connected & File {1} started received.", clientSock.RemoteEndPoint, fileName);
            BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + fileName, FileMode.Append));
            bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
            Console.WriteLine("File: {0} received & saved at path: {1}", fileName, receivedPath);
            bWrite.Close();

            clientSock.Close();
            Console.ReadLine();
        }
      
    }
}
