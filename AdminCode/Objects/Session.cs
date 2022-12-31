using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AdminCode.Objects
{
    public class Session
    {
        private const string FILENAME = "session.xml";
        private static string _applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string _applicationPath = Path.Combine(_applicationDataPath, "AdminCode");

        private readonly XmlWriterSettings _writerSettings;

        public static string BackupPath = Path.Combine(_applicationDataPath, "AdminCode", "backup");

        public static string FileName { get; } = Path.Combine(_applicationPath, FILENAME);

        [XmlAttribute(AttributeName = "ActiveIndex")]
        public int ActiveIndex { get; set; } = 0;

        [XmlElement(ElementName = "File")]
        public List<TextFile> TextFiles { get; set; }

        public Session()
        {
            TextFiles = new List<TextFile>();

            _writerSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("/t"),
                OmitXmlDeclaration = true
            };

            if (!Directory.Exists(_applicationPath))
            {
                Directory.CreateDirectory(_applicationPath);
            }
        }

        public static async Task<Session> Load()
        {
            var session = new Session();

            if(File.Exists(FileName))
            {
                var serializer = new XmlSerializer(typeof(Session));
                var streamReader = new StreamReader(FileName);

                try
                {
                    session = (Session)serializer.Deserialize(streamReader);

                    foreach(var file in session.TextFiles)
                    {
                        var fileName = file.FileName;
                        var backupFileName = file.BackupFileName;

                        file.SafeFileName = Path.GetFileName(fileName);

                        if(File.Exists(fileName))
                        {
                            using (StreamReader reader= new StreamReader(fileName))
                            {
                                file.Contents = await reader.ReadToEndAsync();
                            }
                        }

                        if (File.Exists(backupFileName))
                        {
                            using (StreamReader reader = new StreamReader(backupFileName))
                            {
                                file.Contents = await reader.ReadToEndAsync();
                            }
                        }
                    }
                    
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Une erreur c'est produite :" + ex.Message);
                }
                streamReader.Close();
            }

            return session;
        }

        public void Save()
        {
            var emptyNamespace = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});
            var Serializer = new XmlSerializer(typeof(Session));
            using(XmlWriter writer = XmlWriter.Create(FILENAME, _writerSettings))
            {
                 Serializer.Serialize(writer, this, emptyNamespace);
            }
        }

        public async void BackupFile(TextFile file)
        {
            if(!Directory.Exists(BackupPath)) 
            {
                await Task.Run(() => Directory.CreateDirectory(BackupPath));
            }

            if(file.FileName.StartsWith("Sans titre"))
            {
                using(StreamWriter writer = File.CreateText(file.BackupFileName))
                {
                    await writer.WriteAsync(file.Contents);
                }
            }
        }
    }
}
