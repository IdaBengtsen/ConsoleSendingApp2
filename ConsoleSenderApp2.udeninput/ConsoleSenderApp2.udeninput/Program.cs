using System.Linq.Expressions;

namespace ConsoleSenderApp
{
    class Program
    //Starter overvågning og sender nye billeder til en IP-adresse
    {
        static void Main(string[] args)
        {
            string path = "/home/pi/billeder"; //Definerer en sti til en mappe (RPI-mappen - sæt ind)
            var fw = new FileWatcher(path, SendImage);                  //FileWatcher objekt og sender sendImage
            fw.Start();                                                 //Starter overvågning

            Console.WriteLine("Klar!");
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        static TcpSender sender = new TcpSender("172.20.10.9", 5000); //TCP sender billeder videre til MAUI IPadressen)

        static void SendImage(string filePath)
        {
            try
            {
                Console.WriteLine($"Sender billeder: {filePath}");
                sender.SendFile(filePath);                           //Sender filen til sender.SendFile
                Console.WriteLine($"[OK] fil sendt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FEJL] kunne ikke finde fil: {ex.Message}");
            }

        }
    }
}