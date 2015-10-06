using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub_Lookup.Classes
{
    // Github commit data for specific repository
    class GitHubCommits
    {
        public string name { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public string message { get; set; }
    }
}
