using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GitHub_Lookup.Classes;


namespace GitHub_Lookup
{
    class Program
    {
        static void Main(string[] args)
        {
            // Display Github data for user until <enter> is pressed
            while (true)
            {
                Console.Write("Enter the GitHub username (Press <enter> to exit): ");
                string gitUser = Console.ReadLine();
                if (gitUser.Length == 0)
                {
                    break;
                }

                // Get the GitHub data
                GitHubClient githubClient = new GitHubClient();
                if (getGithubData(gitUser, ref githubClient))
                {
                    //Display the Github data
                    displayGithubData(githubClient);
                }
            }
        }

        // Read GitHub data for input userName, placing it in githubClient
        // Return true if data was successfully read, otherwise return false
        static bool getGithubData(string userName, ref GitHubClient githubClient)
        {
            try
            {
                string json = getWebData("https://api.github.com/users/" + userName,
                                                 "User " + userName + " not found");
                if (json.Length > 0)
                {
                    githubClient =
                               JsonConvert.DeserializeObject<GitHubClient>(json);
                    return true;
                }
                else
                {
                    return false;
                }
            }
	        catch (Exception e)
	        {
                Console.WriteLine
                    ("An error occurred: {0}", e.Message);                                             
                return false;                                        
	        } 
        }
        
        // Display the GitHub data contained in githubClient
        static void displayGithubData(GitHubClient githubClient)
        {
            // GitHub name is sometimes empty
            if (githubClient.name != null)
            {
                Console.WriteLine("GitHub user = {0}, name = {1}",
                githubClient.login, githubClient.name);
            }
            else
            {
                Console.WriteLine("GitHub user = {0}",
                githubClient.login);
            }
            
            Console.WriteLine("Followed by {0} users, and following {1} users",
                                githubClient.followers, githubClient.following);
            Console.WriteLine("Number of repositories: {0}", githubClient.public_repos);
            //Console.WriteLine("Repo URL: {0}", githubClient.repos_url);

            if (githubClient.public_repos > 0)
            {
                displayGithubRepos(githubClient.repos_url);
            }           
     }
     
     // Display the Github repositories, given input URL with repository info
     static void displayGithubRepos(string repos_url)
     {
            string json = getWebData(repos_url, 
                "Repository info at " + repos_url + " not found"); 
            if (json.Length > 0)
            {
                //GitHubRepositoryList githubRepoList = new GitHubRepositoryList();
                //githubRepoList =
                  //  JsonConvert.DeserializeObject<GitHubRepositoryList>(json);

                List<GitHubRepository> githubRepoList = 
                   JsonConvert.DeserializeObject<List<GitHubRepository>>(json);

                Console.WriteLine("Repositories:");

                foreach (GitHubRepository repository in githubRepoList)
                {
                    Console.WriteLine("{0}, {1} stars, {2} watchers",
                        repository.name, repository.stargazers_count,
                            repository.watchers_count);
                }

                // Display additional repository information
                //displayRepoInfo(githubRepoList);
            }
        }

        // Display additional info for repositories
        // Input contains list of repository info
        static void displayRepoInfo(List<GitHubRepository> githubRepoList)
        {
            bool finished = false;
            while (!finished)
            {
                Console.Write
               ("Enter issue to view issues or commit to see commits" +  
                       " or <return> to stop viewing data for this user: ");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "commit":
                    case "c":
                        Console.WriteLine("commit");
                        break;
                    case "issue":
                    case "i":
                        Console.WriteLine("issue");
                        break;
                    case "":
                        finished = true;
                        break;
                    default:
                        break;
                }
            }
            
        }

        // Read from input webUrl
        // Return result of read as string
        // If read fails:
        //   If web exception ocurred, display webErrString & return empty string
        //   If other exception ocurred, display the exception & return empty string
        static string getWebData(string webUrl,
                        string webErrString = "An error occurred reading the data")
        {
            try
            {
                // Read the data at the input URL
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                    string json = webClient.DownloadString(webUrl);
                    return json;
                }
            }
            catch (WebException)
            {
                Console.WriteLine(webErrString);
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine
                    ("An error occurred while reading URL {0}: {1}",
                    webUrl, e.Message);
                return "";
            }
        }

    }
}
