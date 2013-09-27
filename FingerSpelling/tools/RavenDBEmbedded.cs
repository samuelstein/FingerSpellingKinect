using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FingerSpelling.Gestures;
using Raven.Abstractions.Data;
using Raven.Client.Embedded;
using Raven.Database.Server;
using Raven.Imports.Newtonsoft.Json.Serialization;

namespace FingerSpelling.tools
{
    //Singleton

    public sealed class RavenDBEmbedded
    {
        private static EmbeddableDocumentStore documentStore;
        private static readonly RavenDBEmbedded ravenDbEmbedded = new RavenDBEmbedded();

        static RavenDBEmbedded()
        {

            InitDocumentStore();
            Console.WriteLine("constructing db done");
        }

        public static RavenDBEmbedded getRavenDBInstance
        {
            get
            {
                return ravenDbEmbedded;
            }
        }

        //private RavenDBEmbedded()
        //{
        //    InitDocumentStore();
        //    Console.WriteLine("constructing db");
        //}

        public EmbeddableDocumentStore getDBInstance()
        {
            return documentStore;
        }


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
            //documentStore.Conventions.JsonContractResolver = new IncludeNonPublicMembersContractResolver();
            //documentStore.Conventions.JsonContractResolver = new DefaultContractResolver(shareCache: true)
            //{
            //    DefaultMembersSearchFlags =
            //        BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic
            //};
        }

        private static void setupFacetedSearch()
        {
            
            //List<Facet> findGesture = new List<Facet>
            //    {
            //        new Facet{fingerCount=""}
            //    };

        }


    }
}
