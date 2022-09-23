using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace aem4.data
{
	internal class helper
	{
		public static void GetData(string token, Action<string> action)
		{
			using (var w = new System.Net.WebClient())
			{
				w.Headers.Add("Authorization", $"Bearer {token}");
				var s = w.DownloadString(ConfigurationManager.AppSettings["urlgetplatformwell"]);
				var platforms = Newtonsoft.Json.JsonConvert.DeserializeObject<List<platform>>(s);
				using (var m = new model())
				{
					m.platform.AddOrUpdate(platforms.ToArray());
					m.SaveChanges();
					if(action != null)
					{
						action($"{platforms.Count.ToString()} platforms added/updated\n");
					}
				}
			}
		}
		public async static Task<string> Login(string username, string password)
		{
			var data = new Dictionary<string, string>();
			data["username"] = username;
			data["password"] = password;
			using (var h = new System.Net.Http.HttpClient())
			{
				h.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				var req = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(ConfigurationManager.AppSettings["urllogin"]),
					Content = new StringContent(
						Newtonsoft.Json.JsonConvert.SerializeObject(data),
						System.Text.Encoding.UTF8 ,
						"application/json-patch+json")
				};
				var ret = await h.SendAsync(req);
				var stoken = await ret.Content.ReadAsStringAsync();
				stoken = stoken.Remove(0, 1);
				stoken = stoken.Remove(stoken.Length - 1, 1);
				return stoken ;
			}
		}
	}
}
