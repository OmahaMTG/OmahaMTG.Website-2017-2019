using System;
using System.Collections.Generic;

namespace OmahaMTG.AdminContentHandlers.Post
{
    public class Model
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public bool IsDraft { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}