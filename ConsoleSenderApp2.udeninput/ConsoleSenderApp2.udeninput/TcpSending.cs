using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSenderApp
{
    public class TcpSender
    //Sender Filer via TCP
    {
        private string ipAddress;
        private int port;


        public TcpSender(string ipAdress, int port)
        {
            this.ipAddress = ipAdress;
            this.port = port;
        }
        //Gemmer IP og port

        public void SendFile(string filePath)
        {
            try
            {
                byte[] data = File.ReadAllBytes(filePath);
                //Læser hele filen som bytes

                using TcpClient client = new TcpClient(ipAddress, port);
                using NetworkStream stream = client.GetStream();
                //Opretter TCP forbindelse og netværksstrøm

                byte[] lengthPrefix = BitConverter.GetBytes(data.Length);
                stream.Write(lengthPrefix, 0, lengthPrefix.Length);

                stream.Write(data, 0, data.Length);
                //Sender først str af billede som 4-bytes, derefter selve billede
            }

            catch (Exception ex)
            {

                Console.WriteLine($"[Fejl] kunne ikke sende fil: {ex.Message}");    //Kogger evt. fejl

            }
        }
    }
}