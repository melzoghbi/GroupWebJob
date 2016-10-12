# OutlookGroupWebJob
A web job that sends JSON payload to office 365 groups in C#. This web job can be deployed to any Azure App service

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


## How to deploy to Azure App service
Right click on "ConsoleApp" project and click on "Publish as Azure WebJob". This will allow you to create or add this webjob to any existing azure app service application.
