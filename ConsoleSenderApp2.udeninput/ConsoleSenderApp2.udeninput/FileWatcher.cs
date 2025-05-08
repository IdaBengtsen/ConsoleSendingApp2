using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSenderApp
{
    public class FileWatcher
    //Overvåger en mappe for nye jpg-filer
    {
        private readonly string watchPath;    //Sti til mappe der overvåges
        private readonly Action<string> _onNewImage;  //Forvirrende. Skal give fleksibilitet ifht. metoder der skal ske når der findes en ny fil
        private FileSystemWatcher watcher; //Selve overvågnings-objektet

        public FileWatcher(string watchpath, Action<string> onNewImageCallBack) //Action<string> er en metode der tager en string som parameter og returnerer en void
        {
            this.watchPath = watchpath;
            this._onNewImage = onNewImageCallBack;
            //Initialiserer Watchpath og onNewImage, 
        }
        public void Start()
        {
            watcher = new FileSystemWatcher(watchPath, "*.jpg");    //Opretter FileSystemWatcher som kun overvåger jpg-filer
            watcher.Created += OnNewFile;
            watcher.EnableRaisingEvents = true;                     //Aktivering af overvågning

            Console.WriteLine($"Overvåger mappen: {watchPath}");
        }
        private void OnNewFile(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Ny billede fundet: {e.FullPath}");
            Thread.Sleep(200);                                        //Venter 200 ms for at sikre filen er helt færdig
            _onNewImage?.Invoke(e.FullPath);
        }
    }
}
