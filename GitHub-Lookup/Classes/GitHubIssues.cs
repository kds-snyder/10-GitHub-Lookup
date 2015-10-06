using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub_Lookup.Classes
{
    // Github issues data for specific repository
    class GitHubIssues
    {
        public string login { get; set; }
        public string title { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string body { get; set; }
    }
}
