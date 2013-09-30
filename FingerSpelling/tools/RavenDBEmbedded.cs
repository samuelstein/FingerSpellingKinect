using System;
using FingerSpelling.Gestures;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace FingerSpelling.tools
{
    /// <summary> 
    /// Representation of ravendb database. Implemented as singleton</summary>
    public sealed class RavenDBEmbedded
    {
        private static EmbeddableDocumentStore documentStore;
        private static readonly RavenDBEmbedded ravenDbEmbedded = new RavenDBEmbedded();

        static RavenDBEmbedded()
        {

            InitDocumentStore();
            Console.WriteLine("constructing db done");
        }

        /// <summary> 
        /// Get singleton instance.</summary>
        public static RavenDBEmbedded getRavenDBInstance
        {
            get
            {
                return ravenDbEmbedded;
            }
        }

        public EmbeddableDocumentStore getDBInstance()
        {
            return documentStore;
        }

        /// <summary> 
        /// Initializes directory for db and allows webui</summary>
        private static void InitDocumentStore()
        {
            // UseEmbeddedHttpServer = tells RavenDB to enable WebUI so we can look at
            // the data and also to enable REST API
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);

            documentStore = new EmbeddableDocumentStore
                             {
                                 DataDirectory = @"Resources/DB",
                                 UseEmbeddedHttpServer = true
                             };
            //generate a custom Id
            documentStore.Conventions.RegisterIdConvention<Gesture>((dbname, commands, gesture) => "gestures/" + gesture.gestureName);
            documentStore.Initialize();
        }

        /// <summary> 
        /// Method for implementing a faceted search with lucene.</summary>
        private static void setupFacetedSearch()
        {


        }


    }
}
