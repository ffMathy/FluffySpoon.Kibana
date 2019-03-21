using System;

namespace FluffySpoon.Kibana.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Kibana URL:");

            var kibanaUrl = Console.ReadLine();

            var parser = new KibanaParser();
            var elasticsearchQuery = parser.ConvertUrlToElasticsearchQueryString(kibanaUrl, "order_date");

            Console.WriteLine(elasticsearchQuery);

            Console.ReadLine();
        }
    }
}
