﻿using System;
using System.IO;

namespace FluffySpoon.Kibana.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var kibanaUrl = File.ReadAllText("KibanaUrl.txt");

            var parser = new KibanaParser();
            var elasticsearchQuery = parser.ConvertUrlToElasticsearchQueryString(kibanaUrl, "order_date");

            Console.WriteLine(elasticsearchQuery);
            File.WriteAllText("ElasticsearchQuery.txt", elasticsearchQuery);

            Console.ReadLine();
        }
    }
}
