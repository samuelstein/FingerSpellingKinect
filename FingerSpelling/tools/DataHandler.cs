using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FingerSpelling.Gestures;
using Microsoft.Win32.SafeHandles;
using Raven.Client;
using Raven.Client.Embedded;

namespace FingerSpelling.tools
{
    public static class DataHandler
    {
        private static readonly String directory = "Resources/Export/Gestures/";

        public static bool saveToFile(String fileType, String fileName, FileMode fileMode, FileAccess fileAccess, Gesture persistObject)
        {
            try
            {
                switch (fileType)
                {
                    case "XML":
                        writeXML(fileType, fileName, fileMode, fileAccess, persistObject);
                        break;

                    case "STRICTXML":
                        writeStrictXML(fileType, fileName, fileMode, fileAccess, persistObject);
                        break;

                    case "BINARY":
                        writeBinary(fileType, fileName, fileMode, fileAccess, persistObject);
                        break;
                }
            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }
        private static void writeBinary(String fileType, String fileName, FileMode fileMode, FileAccess fileAccess, Gesture persistObject)
        {
            FileStream stream = new FileStream(@"" + directory + fileName + ".dat", FileMode.Create);
            XmlDictionaryWriter binaryDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream);

            DataContractSerializer ser = new DataContractSerializer(typeof(Gesture));
            ser.WriteObject(binaryDictionaryWriter, persistObject);
            binaryDictionaryWriter.Flush();
            binaryDictionaryWriter.Close();
        }

        private static void writeStrictXML(String fileType, String fileName, FileMode fileMode, FileAccess fileAccess, Gesture persistObject)
        {
            XmlSerializer x = new XmlSerializer(persistObject.GetType());
            StreamWriter writer = new StreamWriter(@"" + directory + fileName + ".xml");
            writer.AutoFlush = true;
            x.Serialize(writer, persistObject);
            writer.Close();
        }

        private static void writeXML(String fileType, String fileName, FileMode fileMode, FileAccess fileAccess, Gesture persistObject)
        {
            FileStream writer = new FileStream(@"" + directory + fileName + ".xml", FileMode.Create,
                                                FileAccess.Write);
            DataContractSerializer ser = new DataContractSerializer(typeof(Gesture));
            ser.WriteObject(writer, persistObject);
            writer.Close();
        }

        public static void readFile()
        {
            //TODO:
        }

        public static EmbeddableDocumentStore initializeDB()
        {
            //Connect to DB
            var documentStore = new EmbeddableDocumentStore { DataDirectory = @"Resources/DB" };
            documentStore.Initialize();
            //generate a custom Id
            documentStore.Conventions.RegisterIdConvention<Gesture>((dbname, commands, gesture) => "gestures/" + gesture.gestureName);

            return documentStore;
        }

        public static String saveToDB(EmbeddableDocumentStore documentStore, Gesture gesture)
        {
            // Saving the new instance to RavenDB
            IDocumentSession session = documentStore.OpenSession();
            session.Store(gesture);
            String key = session.Advanced.GetDocumentId(gesture);
            session.SaveChanges();

            return key;
        }

        public static Gesture readFromDB(EmbeddableDocumentStore documentStore, String key)
        {
            IDocumentSession session = documentStore.OpenSession();
            return session.Load<Gesture>(key);
        }

    }
}
