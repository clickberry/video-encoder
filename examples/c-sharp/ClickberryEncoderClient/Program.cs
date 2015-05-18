using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClickberryEncoderClient
{
    internal class Program
    {
        private static void Main()
        {
            const string apiUrl = "http://localhost:51255/api/videos";
            const string filePath = @"C:\Users\alexe_000\Videos\test.mp4";

            try
            {
                string result = UploadFileAsync(apiUrl, filePath).Result;
                Console.Write(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while uploading file: {0}", e);
            }


            Console.ReadKey();
        }

        private static async Task<string> UploadFileAsync(string url, string filePath)
        {
            // Read file asynchronously
            using (
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
                    FileOptions.Asynchronous))
            {
                // Build request content
                using (
                    var content =
                        new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))
                    )
                {
                    var streamContent = new StreamContent(fileStream);
                    streamContent.Headers.Add("Content-Type", "video/mp4");

                    content.Add(streamContent, "video", "video");

                    // Make HTTP request
                    using (var client = new HttpClient())
                    {
                        using (HttpResponseMessage message = await client.PostAsync(url, content))
                        {
                            if (message.StatusCode != HttpStatusCode.Created)
                            {
                                throw new ApplicationException(string.Format("Invalid response received: {0}",
                                    message.StatusCode));
                            }

                            // Read response
                            string response = await message.Content.ReadAsStringAsync();
                            return response;
                        }
                    }
                }
            }
        }
    }
}