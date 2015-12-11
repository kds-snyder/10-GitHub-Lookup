using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitHub_Lookup.Classes
{
    // GitHub data for specific user
    class GitHubClient
    {
        public string login { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public string repos_url { get; set; }
        public int public_repos { get; set; }

        // Read GitHub data for input userName
        // Return GitHubClient object if data was successfully read, otherwise return null
        public static GitHubClient getGithubData(string userName)
        {
            try
            {
                string githubUserUrl = "https://api.github.com/users/" + userName;
                string json = GitHub_Lookup.Classes.WebData.getWebData(githubUserUrl,
                                                 "GitHub user " + userName + " not found");
                if (json.Length > 0)
                {
                    GitHubClient githubClient =
                               JsonConvert.DeserializeObject<GitHubClient>(json);
                    return githubClient;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine
                    ("An error occurred: {0}", e.Message);
                return null;
            }
        }
    }
}
