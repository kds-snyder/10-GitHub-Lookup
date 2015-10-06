using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub_Lookup.Classes
{
    // Github data for specific repository
    public class GitHubRepository
    {
        public string name { get; set; }
        public int stargazers_count { get; set; }
        public int watchers_count { get; set; }
    }
}
