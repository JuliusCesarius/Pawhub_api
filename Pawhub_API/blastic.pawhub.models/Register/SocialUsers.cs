using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blastic.pawhub.models.Register
{
    public class SocialUsers
    {
        public string _id { get; set; }
        public List<BasicUser> twitter { get; set; }
        public List<BasicUser> facebook { get; set; }
        public List<BasicUser> gplus { get; set; }
        public List<BasicUser> others { get; set; }
    }
}
