using System;
using System.Collections.Generic;

namespace BizPortal.ViewModels.V2
{
    public class FileGroup
    {
        public FileGroup()
        {
            FileGroupID = Guid.NewGuid();
        }

        public Guid FileGroupID { get; set; }
        public string Description { get; set; }
        public List<FileMetadata> Files { get; set; }
        public Dictionary<string, string> Extras { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
