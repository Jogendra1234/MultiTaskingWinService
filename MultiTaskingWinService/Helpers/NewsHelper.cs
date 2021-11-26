using DatabaseHandlerLibrary;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace C9ISM.Scheduler.Helpers
{
    public class NewsHelper
    {
        public async Task SaveNews()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ticker Tap News processing ...");
            DatabaseHandler<Company> dbHandlerObj = new DatabaseHandler<Company>();

            News lstNews =  LoadJson();
            if(lstNews != null && lstNews.success && lstNews.data.total > 0)
            {
                foreach (var item in lstNews.data.items)
                {
                    var sqlParameters = new
                    {
                        CID = 3519,
                        CName = "ITC",
                        Title = item.title,
                        Link =  item.link,
                        Newsdate = item.date,
                        Description = item.description,
                        Image = item.image,
                        FeedType = item.feed_type

                    };

                    await dbHandlerObj.SaveData(CommandType.StoredProcedure, sqlParameters, "sp_SaveNews");
                }
            }
            
         
        }

        public News LoadJson()
        {
            string JPath = @"./JSON/response.json";
            using (StreamReader r = new StreamReader(JPath))
            {
                string json = r.ReadToEnd();
                News items = JsonConvert.DeserializeObject<News>(json);
                return items;
            }
        }
    }
}
