
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace КриптоПро
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            string text="";

            using (var root = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            { 
            using (var key2 = root.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI", false))
            {
                var user = key2.GetValue("LastLoggedOnSAMUser");
             
                text += user;
                text += " ";

            };
            
                using (var key = root.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Products\7AB5E7046046FB044ACD63458B5F481C\InstallProperties", false))
                {
                    var registeredOwner = key.GetValue("ProductID");
                    Console.WriteLine("Ваш ключ {0} скопирован в буфер обмена", registeredOwner);
                    Clipboard.SetText(registeredOwner.ToString());
                    Console.WriteLine("Теперь можно Ctrl + C");
                    text += registeredOwner;
                    
                };


                Console.WriteLine("Сохранить ключ в файл CPro_key.txt?(y/n)");
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    Console.WriteLine("Введите папку сохранения ключа или нажмите Enter (файл будет сохраненен в папке рядом с данной программой, например D:\\)");
                    string file_path = Console.ReadLine();
                    StreamWriter f = new StreamWriter(@file_path + "CPro_key.txt", true);
                    f.WriteLine(text);
                    f.Close();
                }
            };
            

        }
    }
}
