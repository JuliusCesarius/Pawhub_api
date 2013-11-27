using System.Collections.Generic;

namespace blastic.pawhub.models
{
    public class MultilingualContent
    {
        public List<Content> contents { get; set; }
    }
    public class Content
    {
        public string culture { get; set; }
        public string content { get; set; }
    }
}