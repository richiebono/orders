using System;
using System.Collections.Generic;
using System.Text;

namespace Bono.Orders.Domain.Entities
{
    public class Attachment
    {
        public string FileName { get; private set; }

        public string MimeType { get; private set; }

        public byte[] File { get; private set; }

        public Attachment(string FileName, string MimeType, byte[] File)
        {
            this.FileName = FileName;
            this.MimeType = MimeType;
            this.File = File;
        }
    }
}
