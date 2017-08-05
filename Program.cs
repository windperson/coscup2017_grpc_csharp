using System;
using System.Threading.Tasks;
using CommandLine;
using Novacode;

using Grpc.Core;
using Coscup2017Demo.Saving;

namespace coscup2017_grpc_csharp
{
    class Program
    {

        public static void Main(string[] args)
        {
            var parseResult = Parser.Default.ParseArguments<Options>(args);

            if (parseResult is NotParsed<Options>)
            {
                Console.WriteLine("wrong parameter(s), exit...");
                return;
            }

            var cmdArgs = ((Parsed<Options>)parseResult).Value;

            if (String.IsNullOrEmpty(cmdArgs.DocPath) || String.IsNullOrEmpty(cmdArgs.HostAddr))
            {
                Console.WriteLine("lack parameter(s), exit...");
                return;
            }

            // Create a channel
            var channel = new Channel(cmdArgs.HostAddr, ChannelCredentials.Insecure);

            GetServerData(channel, cmdArgs.DocPath).Wait();

            //Shutdown
            channel.ShutdownAsync().Wait();
            Console.WriteLine("Saving Result client exit...");
        }

        async static Task GetServerData(Channel channel, String docPath)
        {
            // Create a client with the channel
            var client = new SaveTextService.SaveTextServiceClient(channel);

            // Create a request
            var request = new SaveResultRequest();

            // Send the request
            Console.WriteLine("SaveResult Client sending request");
            try
            {
                using (var call = client.SaveResult(request))
                {
                    var responseStream = call.ResponseStream;
                    while (await responseStream.MoveNext(System.Threading.CancellationToken.None))
                    {
                        var server_sendData = responseStream.Current;
                        var clientId = server_sendData.ClientId;
                        var recoginzed = server_sendData.Recognized;
                        var GoogleTimeStamp  = server_sendData.Timestamp;
                        var timeStamp = GoogleTimeStamp.ToDateTime();

                        WriteToDoc(docPath, clientId, recoginzed, timeStamp);
                    }
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"gRPC failed:{ex}");
            }
        }

        static void WriteToDoc(string docPath, int clientId, string recoginzed, DateTime timeStamp)
        {
            DocX doc;
            if (!System.IO.File.Exists(docPath))
            {
                doc = DocX.Create(docPath);
            }
            else
            {
                doc = DocX.Load(docPath);
            }

            var headerFormat = new Formatting();
            headerFormat.FontFamily = new Font("Arial Black");
            headerFormat.Size = 20;
            headerFormat.Bold = true;

            doc.InsertParagraph($"client: {clientId}", false, headerFormat);

            var para = doc.InsertParagraph();
            para.Append($"@ {timeStamp.ToUniversalTime().ToString("s")} says: {recoginzed} ");

            doc.Save();
        }
    }
}
