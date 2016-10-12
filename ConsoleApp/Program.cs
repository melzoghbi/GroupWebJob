using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMessage().Wait();
            Console.WriteLine("A message has been sent successfully!");

        }

        private static async Task<bool> SendMessage()
        {
            string webhookUrl = ConfigurationManager.AppSettings["webhookUrl"];

            //prepare the json payload
            var json = @"
                {
                    'summary': 'A new listing was posted to the system',
                    'sections': [
                        {
                            'activityTitle': 'New BillsList listing',
                            'activitySubtitle': '" + "Title" + @"',
                            'activityImage': '" + "http://connectorsdemo.azurewebsites.net/images/MicrosoftSurface_024_Cafe_OH-06315_VS_R1c.jpg" + @"',
                            'facts': [
                                {
                                    'name': 'Category',
                                    'value': '" + "Category" + @"'
                                },
                                {
                                    'name': 'Price',
                                    'value': '$" + "5" + @"'
                                },
                                {
                                    'name': 'Listed by',
                                    'value': '" + "Mostafa E" + @"'
                                }
                            ]
                        }
                    ],
                    'potentialAction': [
                        {
                            '@context': 'http://schema.org',
                            '@type': 'ViewAction',
                            'name': 'View in BillsList',
                            'target': [
                                'https://localhost:44300/items/detail/" + "1" + @"'
                            ]
                        },
                        {
                            '@context': 'http://schema.org',
                            '@type': 'ViewAction',
                            'name': 'Buy Now',
                            'target': [
                                'https://localhost:44300/items/detail/" + "1" + @"'
                            ]
                        }
                    ]}";

            //prepare the http POST
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(webhookUrl, content))
            {
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }
    }
}
