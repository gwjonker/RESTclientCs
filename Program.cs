﻿using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var repositories = ProcessRepositories().Result;

            foreach (var repo in repositories) 
            {
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.FullName);
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.GitUrl);
                Console.WriteLine(repo.LastPush);
            }
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = serializer.ReadObject(await streamTask) as List<Repository>;
            
            return repositories;
        }

    }
}