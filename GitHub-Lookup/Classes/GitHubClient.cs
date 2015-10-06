using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
