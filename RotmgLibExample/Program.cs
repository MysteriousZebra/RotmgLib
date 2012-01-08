using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RotmgLib.Network;
using System.IO;

namespace RotmgLibExample
{
    class Program
    {
        static void Main(string[] args)
        {
            RotmgClient client = new RotmgClient("ec2-50-18-235-85.us-west-1.compute.amazonaws.com");

            client.OnFailure += new FailureHandler(client_OnFailure);
            client.OnMapInfo += new MapInfoHandler(client_OnMapInfo);

            client.Connect();
            client.SendHello("120.10.2", -2, "thisisatestguid", "", "");
        }

        static void client_OnFailure(int error_id, string error_description)
        {
            Console.WriteLine("Error {0}: {1}", error_id, error_description);
        }

        static void client_OnMapInfo(int width, int height, string name, uint fp, int background, bool allow_player_teleport, bool show_displays, string[] extra_xml)
        {
            if (extra_xml.Length == 0)
                Console.WriteLine("No new XML available.");
            else
            {
                string file = "XMLDump" + DateTime.Now.ToBinary() + ".txt";
                StreamWriter writer = new StreamWriter(File.Create(file));

                for (int i = 0; i < extra_xml.Length; i++)
                    writer.WriteLine(extra_xml[i]);

                writer.Close();
                
                Console.WriteLine("New XML available, dumped to {0}.", file);
            }

            Console.ReadKey();
        }
    }
}
