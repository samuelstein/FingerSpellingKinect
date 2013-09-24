using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FingerSpelling.Gestures;
using Microsoft.Win32.SafeHandles;
using Raven.Abstractions.Exceptions;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Json.Linq;

namespace FingerSpelling.tools
{
    public static class DataPersister
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

        //public static EmbeddableDocumentStore initializeDB()
        //{
        //    //Connect to DB
        //    var documentStore = new EmbeddableDocumentStore { DataDirectory = @"Resources/DB" };
        //    documentStore.Initialize();
        //    //generate a custom Id
        //    documentStore.Conventions.RegisterIdConvention<Gesture>((dbname, commands, gesture) => "gestures/" + gesture.gestureName);

        //    return documentStore;
        //}

        public static String saveToDB(EmbeddableDocumentStore documentStore, Gesture gesture)
        {
            IDocumentSession session = documentStore.OpenSession();
            var previousSetting = session.Advanced.UseOptimisticConcurrency;
            String key = "";

            //var constraint = new UniqueConstraint { Type = typeof(T).Name, Key = key };

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
                //session.Advanced.Evict(constraint);
                //throw;
            }
            finally
            {
                session.Advanced.UseOptimisticConcurrency = previousSetting;
            }

            return key;
        }

        public static Gesture readFromDB(EmbeddableDocumentStore documentStore, String key)
        {
            IDocumentSession session = documentStore.OpenSession();
            return session.Load<Gesture>(key);
        }


        public static String saveToDB(EmbeddableDocumentStore documentStore, string json)
        {
            IDocumentSession session = documentStore.OpenSession();
            var previousSetting = session.Advanced.UseOptimisticConcurrency;
            String key = "";

            //var constraint = new UniqueConstraint { Type = typeof(T).Name, Key = key };

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
                //session.Advanced.Evict(constraint);
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
