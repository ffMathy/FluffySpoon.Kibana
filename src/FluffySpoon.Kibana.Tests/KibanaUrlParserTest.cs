using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluffySpoon.Kibana.Tests
{
	[TestClass]
	public class KibanaUrlParserTest
	{
		[TestMethod]
		public void CanParseEmptyObject()
		{
			Assert.AreEqual("{}", new KibanaUrlParser().ConvertQueryParameterValueToJson(
				"()"));
		}

		[TestMethod]
		public void CanParseEmptyArray()
		{
			Assert.AreEqual("[]", new KibanaUrlParser().ConvertQueryParameterValueToJson(
				"!()"));
		}

		[TestMethod]
		public void CanParseObject()
		{
			Assert.AreEqual("{\"a\":1,\"b\":\"foo\",\"c\":{\"e\":1337}}", new KibanaUrlParser().ConvertQueryParameterValueToJson(
				"(a:1,b:'foo',c:(e:1337))"));
		}

		[TestMethod]
		public void CanParseComplexObject()
		{
			Assert.AreEqual("{\"columns\":[\"message\",\"messageTemplate\",\"level\",\"fields.HttpRequestUrlHost\"],\"filters\":[{\"$state\":{\"store\":\"appState\"},\"meta\":{\"alias\":null,\"disabled\":false,\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"key\":\"level\",\"negate\":true,\"params\":{\"query\":\"Verbose\",\"type\":\"phrase\"},\"type\":\"phrase\",\"value\":\"Verbose\"},\"query\":{\"match\":{\"level\":{\"query\":\"Verbose\",\"type\":\"phrase\"}}}},{\"$state\":{\"store\":\"appState\"},\"meta\":{\"alias\":null,\"disabled\":false,\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"key\":\"level\",\"negate\":true,\"params\":{\"query\":\"Debug\",\"type\":\"phrase\"},\"type\":\"phrase\",\"value\":\"Debug\"},\"query\":{\"match\":{\"level\":{\"query\":\"Debug\",\"type\":\"phrase\"}}}},{\"$state\":{\"store\":\"appState\"},\"meta\":{\"alias\":null,\"disabled\":false,\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"key\":\"level\",\"negate\":true,\"params\":{\"query\":\"Information\",\"type\":\"phrase\"},\"type\":\"phrase\",\"value\":\"Information\"},\"query\":{\"match\":{\"level\":{\"query\":\"Information\",\"type\":\"phrase\"}}}},{\"$state\":{\"store\":\"appState\"},\"meta\":{\"alias\":null,\"disabled\":false,\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"key\":\"fields.Area\",\"negate\":true,\"params\":{\"query\":\"frontend\",\"type\":\"phrase\"},\"type\":\"phrase\",\"value\":\"frontend\"},\"query\":{\"match\":{\"fields.Area\":{\"query\":\"frontend\",\"type\":\"phrase\"}}}}],\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"interval\":\"auto\",\"query\":{\"language\":\"lucene\",\"query\":\"\"},\"sort\":[\"@timestamp\",\"desc\"]}", new KibanaUrlParser().ConvertQueryParameterValueToJson(
				"(columns:!(message,messageTemplate,level,fields.HttpRequestUrlHost),filters:!(('$state':(store:appState),meta:(alias:!n,disabled:!f,index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,key:level,negate:!t,params:(query:Verbose,type:phrase),type:phrase,value:Verbose),query:(match:(level:(query:Verbose,type:phrase)))),('$state':(store:appState),meta:(alias:!n,disabled:!f,index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,key:level,negate:!t,params:(query:Debug,type:phrase),type:phrase,value:Debug),query:(match:(level:(query:Debug,type:phrase)))),('$state':(store:appState),meta:(alias:!n,disabled:!f,index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,key:level,negate:!t,params:(query:Information,type:phrase),type:phrase,value:Information),query:(match:(level:(query:Information,type:phrase)))),('$state':(store:appState),meta:(alias:!n,disabled:!f,index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,key:fields.Area,negate:!t,params:(query:frontend,type:phrase),type:phrase,value:frontend),query:(match:(fields.Area:(query:frontend,type:phrase))))),index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,interval:auto,query:(language:lucene,query:''),sort:!('@timestamp',desc))"));
		}
	}
}
