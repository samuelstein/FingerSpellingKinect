using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;


public class Index_Auto_2fGestures_2fByfingerCount : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fGestures_2fByfingerCount()
	{
		this.ViewText = @"from doc in docs.Gestures
select new { fingerCount = doc.fingerCount }";
		this.ForEntityNames.Add("Gestures");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Gestures", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				fingerCount = doc.fingerCount,
				__document_id = doc.__document_id
			});
		this.AddField("fingerCount");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("fingerCount");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("fingerCount");
		this.AddQueryParameterForReduce("__document_id");
	}
}
