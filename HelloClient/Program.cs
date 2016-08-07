using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors;
using HelloActor.Interfaces;

namespace HelloClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = ActorProxy.Create<IHelloActor>(ActorId.CreateRandom(), "fabric:/HelloApplication");
            Console.WriteLine("From Actor {0}; {1}", proxy.GetActorId(), proxy.SayHelloAsync("よろしく").Result);
            int count = 10;
            while(true)
            {
                count = proxy.GetCountAsync().Result;

                Console.WriteLine("Count from Actor {0}: {1}", proxy.GetActorId(), count);
                count++;
                Console.WriteLine("Setting Count to in Actor {0}: {1}", proxy.GetActorId(), count);
                proxy.SetCountAsync(count).Wait();
                Thread.Sleep(1000);
            }
        }
    }
}
