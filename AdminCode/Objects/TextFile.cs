using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AdminCode.Objects
{
    public class TextFile
    {
        [XmlAttribute(AttributeName = "FileName")]
        public string FileName { get; set; }

        [XmlAttribute(AttributeName = "BackupFileName")]
        public string BackupFileName { get; set; } = string.Empty;

        [XmlIgnore()]
        public string SafeFileName { get; set; }

        [XmlIgnore()]
        public string SafeBackupFileName { get; set; }

        [XmlIgnore()]
        public string Contents { get; set; } = string.Empty;

        public TextFile() 
        {
            
        }

        public TextFile(string fileName)
        {
            FileName = fileName;
            SafeFileName = Path.GetFileName(fileName);

            if(FileName.StartsWith("Sans titre"))
            {
                SafeBackupFileName = $"{FileName}@{DateTime.Now:MM-dd-yyyy-HH-mm-ss}";
                BackupFileName = Path.Combine(Session.BackupPath, SafeBackupFileName);
            }
        }
    }
}
