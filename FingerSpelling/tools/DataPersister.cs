using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using FingerSpelling.Gestures;
using Raven.Abstractions.Exceptions;
using Raven.Client;
using Raven.Client.Embedded;

namespace FingerSpelling.tools
{
    /// <summary> 
    /// Static class provides CRUD operations to db and gesture exports to filesystem.</summary>
    public static class DataPersister
    {
        private static readonly String directory = "Resources/Export/Gestures/";
        private static EmbeddableDocumentStore database;

        /// <summary> 
        /// Saves the gesture to file as XML, STRICTXML or BINARY.</summary>
        public static bool SaveToFile(String fileType, String fileName, FileMode fileMode, FileAccess fileAccess, Gesture persistObject)
        {
            try
            {
                switch (fileType)
                {
                    case "XML":
                        WriteXml(fileType, fileName, fileMode, fileAccess, persistObject);
                        break;

                    case "STRICTXML":
                        WriteStrictXml(fileType, fileName, fileMode, fileAccess, persistObject);
                        break;

                    case "BINARY":
                        WriteBinary(fileType, fileName, fileMode, fileAccess, persistObject);
                        break;
                }
            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }

        /// <summary> 
        /// Writes the gesture as binary stream to filesystem.</summary>
        private static void WriteBinary(String fileType, String fileName, FileMode fileMode, FileAccess fileAccess, Gesture persistObject)
        {
            FileStream stream = new FileStream(@"" + directory + fileName + ".dat", FileMode.Create);
            XmlDictionaryWriter binaryDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream);

            DataContractSerializer ser = new DataContractSerializer(typeof(Gesture));
            ser.WriteObject(binaryDictionaryWriter, persistObject);
            binaryDictionaryWriter.Flush();
            binaryDictionaryWriter.Close();
        }

        /// <summary> 
        /// Writes the gesture as strict (crashes if values empty) XML to filesystem.</summary>
        private static void WriteStrictXml(String fileType, String fileName, FileMode fileMode, FileAccess fileAccess, Gesture persistObject)
        {
            XmlSerializer x = new XmlSerializer(persistObject.GetType());
            StreamWriter writer = new StreamWriter(@"" + directory + fileName + ".xml");
            writer.AutoFlush = true;
            x.Serialize(writer, persistObject);
            writer.Close();
        }

        /// <summary> 
        /// Writes the gesture as XML to filesystem.</summary>
        private static void WriteXml(String fileType, String fileName, FileMode fileMode, FileAccess fileAccess, Gesture persistObject)
        {
            FileStream writer = new FileStream(@"" + directory + fileName + ".xml", FileMode.Create,
                                                FileAccess.Write);
            DataContractSerializer ser = new DataContractSerializer(typeof(Gesture));
            ser.WriteObject(writer, persistObject);
            writer.Close();
        }

        /// <summary> 
        /// Reads the gesture as XML from filesystem.</summary>
        public static void ReadFile(String fileName)
        {
            //Open the file written above and read values from it.
            Console.WriteLine("Resources/Gestures/" + fileName + ".dat");
            Stream stream = File.Open(@"Resources/Gestures/" + fileName + ".dat", FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter = new BinaryFormatter();

            Console.WriteLine("Reading Gesture");
            Gesture g = (Gesture)bformatter.Deserialize(stream);
            MessageBox.Show(g.contourPoints.ToString());
            stream.Close();
        }

        /// <summary> 
        /// Initializes the database.</summary>
        public static void InitializeDb()
        {
            database = RavenDBEmbedded.getRavenDBInstance.getDBInstance();
        }

        /// <summary> 
        /// Writes gestures to db.</summary>
        public static String SaveToDb(Gesture gesture)
        {
            IDocumentSession session = database.OpenSession();
            var previousSetting = session.Advanced.UseOptimisticConcurrency;
            String key = "";

            try
            {
                session.Advanced.UseOptimisticConcurrency = true;
                session.Store(gesture);
                key = session.Advanced.GetDocumentId(gesture);

                session.SaveChanges();

            }
            catch (ConcurrencyException)
            {
                // rollback changes so we can keep using the session
                session.Advanced.Evict(gesture);
            }
            finally
            {
                session.Advanced.UseOptimisticConcurrency = previousSetting;
            }

            return key;
        }

        /// <summary> 
        /// Reads gestures from db.</summary>
        public static Gesture ReadFromDb(String key)
        {
            IDocumentSession session = database.OpenSession();
            return session.Load<Gesture>(key);
        }

        /// <summary> 
        /// Searches given gesture from db.</summary>
        public static List<Gesture> searchMatchingGesture(Gesture actualGesture)
        {
            IDocumentSession session = database.OpenSession();

            return session.Query<Gesture>().Where(gesture => gesture.fingerCount == actualGesture.fingerCount).ToList();
        }

        /// <summary> 
        /// Fetches all gestures from db.</summary>
        public static List<Gesture> FetchAll()
        {
            IDocumentSession session = database.OpenSession();

            return session.Query<Gesture>().ToList();
        }

        /// <summary> 
        /// Writes json formatted gesture to db.</summary>
        public static String SaveToDb(string json)
        {
            IDocumentSession session = database.OpenSession();
            var previousSetting = session.Advanced.UseOptimisticConcurrency;
            String key = "";

            try
            {
                //session.Advanced.DocumentStore.Conventions.CustomizeJsonSerializer = serializer => serializer.Converters.Add(DataContractJsonSerializer);
                session.Advanced.UseOptimisticConcurrency = true;
                session.Store(json);
                key = session.Advanced.GetDocumentId(json);
                session.SaveChanges();
            }
            catch (ConcurrencyException)
            {
                // rollback changes so we can keep using the session
                session.Advanced.Evict(json);
                throw;
            }
            finally
            {
                session.Advanced.UseOptimisticConcurrency = previousSetting;
            }

            return key;
        }
    }
}
