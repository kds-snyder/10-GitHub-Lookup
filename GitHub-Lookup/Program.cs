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
                Console.Write("Enter the GitHub username (press <enter> to exit): ");
                string gitUser = Console.ReadLine();
                if (gitUser.Length == 0)
                {
                    break;
                }

                // Get the GitHub data
                //GitHubClient githubClient = new GitHubClient();
                GitHubClient githubClient = GitHubClient.getGithubData(gitUser);

                if (githubClient != null)
                {
                    //Display the Github data
                    displayGithubData(gitUser, githubClient);
                }
            }
        }

        
        // Display the GitHub data contained in githubClient
        static void displayGithubData(string gitUserName, GitHubClient githubClient)
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

            // Display repository data if there are repositories
            if (githubClient.public_repos > 0)
            {
                 displayGithubRepos(gitUserName, githubClient.repos_url);                             
            }           
     }
     
     // Display the Github repositories, given input URL with repository info
     static void displayGithubRepos(string gitUser, string repos_url)
     {
            string json = getWebData(repos_url, 
                "Repository info at " + repos_url + " not found"); 
            if (json.Length > 0)
            {
               GitHubRepositoryList.repositories =
                    JsonConvert.DeserializeObject<List<GitHubRepository>>(json);

                Console.WriteLine("Repositories:");

                foreach (GitHubRepository repository in 
                                GitHubRepositoryList.repositories)
                {
                    Console.WriteLine("{0}, {1} stars, {2} watchers",
                        repository.name, repository.stargazers_count,
                            repository.watchers_count);                   
                }

                // Display additional repository information
                displayRepoInfo(gitUser, GitHubRepositoryList.repositories);
            }
        }

            // Display additional info for repositories
            // Input contains list of repository info
            static void displayRepoInfo(string gitUser,
                                List<GitHubRepository> githubRepoList)
        {
            bool finished = false;
            while (!finished)
            {
                Console.Write
               ("Enter issue to view issues, or commit to view commits," +  
                       " or <return> to stop viewing data for this user: ");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "commit":
                    case "c":
                        //Console.WriteLine("commit");
                        displayCommits(gitUser, githubRepoList);
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

        // Display the commits for a repository as specified by the user
        static void displayCommits(string gitUserName,
                            List<GitHubRepository> githubRepoList)
        {
            while (true)
            {
                Console.Write
                    ("Enter the repository name (press <enter> to return to previous prompt): ");
                string repositoryName = Console.ReadLine();
                if (repositoryName.Length == 0)
                {
                    break;
                }

                // Get the repository commit data
                string repositoryCommitUrl = "https://api.github.com/repos/"
                    + gitUserName + "/" + repositoryName + "/commits";
                string json = getWebData(repositoryCommitUrl,
                    "Error occurred reading " + repositoryName + " commit data"
                    + " at " + repositoryCommitUrl);
                if (json.Length > 0)
                {
//                    Console.WriteLine(json);
                    GitHubCommitList.commits =
                        JsonConvert.DeserializeObject<List<GitHubCommit>>(json);

                    foreach (GitHubCommit commit in
                                         GitHubCommitList.commits)
                    {
                        Console.WriteLine("Commit date: {0}", commit.date);
                        Console.WriteLine("Email: {0}", commit.email);
                        Console.WriteLine("Name: {0}", commit.name);
                        Console.WriteLine("Message: {0}", commit.message);
                        Console.WriteLine("-------------------------------------");
                    }


                }

            }
        }




    }
}
