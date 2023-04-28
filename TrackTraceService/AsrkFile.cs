using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTraceService
{
    internal class AsrkFile
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public List<FileBody> fileBody { get; set; }
        public DateTime CreateDate { get; set; }
        public int IsSended { get; set; }
        public DateTime SendDate { get; set; }
    }
}
