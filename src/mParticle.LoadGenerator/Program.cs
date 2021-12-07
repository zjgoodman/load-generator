using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace mParticle.LoadGenerator
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            string configFile = "config.json";
            if (args.Length > 0)
            {
                configFile = args[0];
            }

            Config config = Config.GetArguments(configFile);
            if (config == null)
            {
                Console.WriteLine("Failed to parse configuration.");
                return;
            }
        }
        public static void Run()
        {
            Console.WriteLine("Hello world");
        }
        public static Task<int> MakeRequest()
        {
            // TODO make actual web request
            return Task.Run<int>(() => 200);
        }
        public static List<Task<int>> MakeRequests(int numberOfRequests)
        {
            List<Task<int>> requests = new List<Task<int>>();
            for (int i = 0; i < numberOfRequests; i++)
            {
                requests.Add(MakeRequest());
            }
            return requests;
        }
    }
}
