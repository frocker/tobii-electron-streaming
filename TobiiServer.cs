using System;
using System.Net.Sockets;
using System.Text;
using Tobii.Interaction;
using Tobii.Interaction.Framework;
using Tobii.Interaction.Model;

/*Integrate this into a C# project that is set up to use the Tobii SDK.
  It will send the Tobii input to your electron App over UDP.
*/
namespace TobiiSDKServer
{
    class TobiiServer
    {
        static void Main(string[] args)
        {
            
            // Initialise Host to Tobii Connection
            var host = new Host();

            //Uncomment this section to Launch Calibration when the project opens
	       /*
            System.Threading.Thread.Sleep(1000);
            host.Context.LaunchConfigurationTool(ConfigurationTool.RetailCalibration, (data) => { });
            System.Threading.Thread.Sleep(10000);
	       */

            //Setup Server
            UdpClient udpClient = new UdpClient();
            udpClient.Connect("127.0.0.1", 33333);

            //Create stream. 
            var gazePointDataStream = host.Streams.CreateGazePointDataStream();

       
            // Get the gaze data
            gazePointDataStream.GazePoint((x, y, ts) => SendInput(udpClient,x, y, ts));

            // Read
            Console.ReadKey();

            // we will close the coonection to the Tobii Engine before exit.
            host.DisableConnection();

            //ToDo: Add code to boot your Electron App here

        }

        static void SendInput(UdpClient client, double x, double y, double ts)
        {
            String sendString = @"{""id"":""gaze_data"", ""x"":" + x + @", ""y"": " + y + @", ""timestamp"":" + ts + @"}";
            Byte[] senddata = Encoding.ASCII.GetBytes(sendString);
            client.Send(senddata, senddata.Length);
        }
    }
}
