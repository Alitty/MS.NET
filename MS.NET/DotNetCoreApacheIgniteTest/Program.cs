using System;
using System.IO;
using System.Linq;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Client;
using Apache.Ignite.Core.Compute;
using System.Collections.Generic;

namespace DotNetCoreApacheIgniteTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cfg = new IgniteClientConfiguration();
            cfg.Endpoints = new List<string>() { "* 127.0.0.1" };
            using (var client = Ignition.StartClient(cfg))
            {
                var cache = client.GetCache<int, string>("cache");
                cache.Put(1, "Hello World.");
                Console.WriteLine(cache.Get(1));
            }
            //using (var ignite = Ignition.Start(new IgniteConfiguration() { JvmClasspath = string.Join(";", Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "libs"), "*.jar")),JvmDllPath= @"E:\Jdk\14.x\bin\server" }))
            //{
            //    var action = "Hello World Welcome".Split(" ").Select(o => new ComputeFunc { Content = o });
            //    var result = ignite.GetCompute().Call(action).Sum();

            //    Console.WriteLine(result);
            //}

            Console.ReadKey();
        }

        public class ComputeFunc : IComputeFunc<int>
        {
            public string Content { get; set; }

            public int Invoke() => Content.Length;
        }
    }
}