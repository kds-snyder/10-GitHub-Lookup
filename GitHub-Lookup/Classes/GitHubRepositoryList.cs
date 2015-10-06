using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub_Lookup.Classes
{
    // GitHub repository list, containing an entry for each repository
    public class GitHubRepositoryList
    {
        //public static List<GitHubRepository> repositories = 
         //                       new List<GitHubRepository>();

        public List<GitHubRepository> repositories { get; set; }
    }
}
