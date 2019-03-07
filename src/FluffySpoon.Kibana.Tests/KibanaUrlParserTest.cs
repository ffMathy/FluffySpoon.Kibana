using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace FluffySpoon.Kibana.Tests
{
	[TestClass]
	public class KibanaUrlParserTest
    {
        [TestMethod]
		public void CanParseEmptyObject()
		{
			Assert.AreEqual("{}", new KibanaParser().ConvertQueryParameterValueToJson(
				"()"));
		}

		[TestMethod]
		public void CanParseEmptyArray()
		{
			Assert.AreEqual("[]", new KibanaParser().ConvertQueryParameterValueToJson(
				"!()"));
		}

		[TestMethod]
		public void CanParseObject()
		{
			Assert.AreEqual("{\"a\":1,\"b\":\"foo\",\"c\":{\"e\":1337}}", new KibanaParser().ConvertQueryParameterValueToJson(
				"(a:1,b:'foo',c:(e:1337))"));
		}

        [TestMethod]
        public void CanParseComplexObject3()
        {
            var result = new KibanaParser().ConvertQueryParameterValueToJson("!((meta:(controlledBy:'a',index:'b',value:true)))");
            Assert.AreEqual(@"[{""meta"":{""controlledBy"":""a"",""index"":""b"",""value"":""true""}}]",
                result);
        }

		[TestMethod]
		public void CanParseComplexObject1()
		{
			Assert.AreEqual("{\"columns\":[\"message\",\"messageTemplate\",\"level\",\"fields.HttpRequestUrlHost\"],\"filters\":[{\"$state\":{\"store\":\"appState\"},\"meta\":{\"alias\":null,\"disabled\":false,\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"key\":\"level\",\"negate\":true,\"params\":{\"query\":\"Verbose\",\"type\":\"phrase\"},\"type\":\"phrase\",\"value\":\"Verbose\"},\"query\":{\"match\":{\"level\":{\"query\":\"Verbose\",\"type\":\"phrase\"}}}},{\"$state\":{\"store\":\"appState\"},\"meta\":{\"alias\":null,\"disabled\":false,\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"key\":\"level\",\"negate\":true,\"params\":{\"query\":\"Debug\",\"type\":\"phrase\"},\"type\":\"phrase\",\"value\":\"Debug\"},\"query\":{\"match\":{\"level\":{\"query\":\"Debug\",\"type\":\"phrase\"}}}},{\"$state\":{\"store\":\"appState\"},\"meta\":{\"alias\":null,\"disabled\":false,\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"key\":\"level\",\"negate\":true,\"params\":{\"query\":\"Information\",\"type\":\"phrase\"},\"type\":\"phrase\",\"value\":\"Information\"},\"query\":{\"match\":{\"level\":{\"query\":\"Information\",\"type\":\"phrase\"}}}},{\"$state\":{\"store\":\"appState\"},\"meta\":{\"alias\":null,\"disabled\":false,\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"key\":\"fields.Area\",\"negate\":true,\"params\":{\"query\":\"frontend\",\"type\":\"phrase\"},\"type\":\"phrase\",\"value\":\"frontend\"},\"query\":{\"match\":{\"fields.Area\":{\"query\":\"frontend\",\"type\":\"phrase\"}}}}],\"index\":\"c69d0b00-0f28-11e9-9d49-47cc76ece0a5\",\"interval\":\"auto\",\"query\":{\"language\":\"lucene\",\"query\":\"\"},\"sort\":[\"@timestamp\",\"desc\"]}", new KibanaParser().ConvertQueryParameterValueToJson(
				"(columns:!(message,messageTemplate,level,fields.HttpRequestUrlHost),filters:!(('$state':(store:appState),meta:(alias:!n,disabled:!f,index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,key:level,negate:!t,params:(query:Verbose,type:phrase),type:phrase,value:Verbose),query:(match:(level:(query:Verbose,type:phrase)))),('$state':(store:appState),meta:(alias:!n,disabled:!f,index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,key:level,negate:!t,params:(query:Debug,type:phrase),type:phrase,value:Debug),query:(match:(level:(query:Debug,type:phrase)))),('$state':(store:appState),meta:(alias:!n,disabled:!f,index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,key:level,negate:!t,params:(query:Information,type:phrase),type:phrase,value:Information),query:(match:(level:(query:Information,type:phrase)))),('$state':(store:appState),meta:(alias:!n,disabled:!f,index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,key:fields.Area,negate:!t,params:(query:frontend,type:phrase),type:phrase,value:frontend),query:(match:(fields.Area:(query:frontend,type:phrase))))),index:c69d0b00-0f28-11e9-9d49-47cc76ece0a5,interval:auto,query:(language:lucene,query:''),sort:!('@timestamp',desc))"));
        }

        [TestMethod]
        public void CanParseComplexObject2()
        {
            var result = new KibanaParser().ConvertQueryParameterValueToJson(
                @"(description:'',filters:!(('$state':(store:appState),geo_bounding_box:(ignore_unmapped:!t,location:(bottom_right:(lat:55.42901345240742,lon:13.194580078125002),top_left:(lat:56.435166753146135,lon:9.036254882812502))),meta:(alias:!n,disabled:!f,index:'14746b80-3f48-11e9-8d2e-3f08e16da335',key:location,negate:!f,params:(bottom_right:(lat:55.42901345240742,lon:13.194580078125002),top_left:(lat:56.435166753146135,lon:9.036254882812502)),type:geo_bounding_box,value:'{
  ""lat"": 56.435166753146135,
  ""lon"": 9.036254882812502
}
        to {
  ""lat"": 55.42901345240742,
  ""lon"": 13.194580078125002
}')),('$state':(store:appState),meta:(alias:!n,controlledBy:'1551791720117',disabled:!f,index:'14746b80-3f48-11e9-8d2e-3f08e16da335',key:plusUser,negate:!f,params:(query:!t,type:phrase),type:phrase,value:true),query:(match:(plusUser:(query:!t,type:phrase)))),('$state':(store:appState),meta:(alias:!n,disabled:!f,index:'14746b80-3f48-11e9-8d2e-3f08e16da335',key:zipCode,negate:!f,params:(query:'2300',type:phrase),type:phrase,value:'2300'),query:(match:(zipCode:(query:'2300',type:phrase)))),('$state':(store:appState),meta:(alias:!n,controlledBy:'1551791836779',disabled:!f,index:'14746b80-3f48-11e9-8d2e-3f08e16da335',key:lastClientVersion,negate:!f,params:!('2.36','2.20.3'),type:phrases,value:'2.36, 2.20.3'),query:(bool:(minimum_should_match:1,should:!((match_phrase:(lastClientVersion:'2.36')),(match_phrase:(lastClientVersion:'2.20.3'))))))),fullScreenMode:!f,options:(darkTheme:!f,hidePanelTitles:!f,useMargins:!t),panels:!((embeddableConfig:(mapCenter:!(55.785840337419124,10.906677246093752),mapZoom:8),gridData:(h:21,i:'1',w:40,x:0,y:0),id:'0fa4d020-3f4a-11e9-8d2e-3f08e16da335',panelIndex:'1',type:visualization,version:'6.6.1'),(embeddableConfig:(),gridData:(h:12,i:'2',w:10,x:21,y:21),id:'8745c590-3f49-11e9-8d2e-3f08e16da335',panelIndex:'2',type:visualization,version:'6.6.1'),(embeddableConfig:(),gridData:(h:10,i:'3',w:8,x:21,y:33),id:'6d19ca40-3f49-11e9-8d2e-3f08e16da335',panelIndex:'3',type:visualization,version:'6.6.1'),(embeddableConfig:(),gridData:(h:22,i:'4',w:28,x:0,y:43),id:'28311aa0-3f49-11e9-8d2e-3f08e16da335',panelIndex:'4',type:visualization,version:'6.6.1'),(embeddableConfig:(),gridData:(h:22,i:'5',w:21,x:0,y:21),id:eac48160-3f49-11e9-8d2e-3f08e16da335,panelIndex:'5',type:visualization,version:'6.6.1')),query:(language:kuery,query:''),timeRestore:!f,title:Users,viewMode:view)");
            JsonConvert.DeserializeObject(result);
        }

        [TestMethod]
        public void CanParseUrlToElasticsearchQuery()
        {
            var result = new KibanaParser().ConvertUrlToElasticsearchQueryString(
                "https://example.com/app/kibana#/dashboard/c4bfe170-3f4a-11e9-8d2e-3f08e16da335?_g=(refreshInterval:(pause:!t,value:0),time:(from:now-30m,mode:quick,to:now))&_a=(description:'',filters:!(),fullScreenMode:!f,options:(darkTheme:!f,hidePanelTitles:!f,useMargins:!t),panels:!((embeddableConfig:(mapCenter:!(57.136239319177434,-3.3398437500000004),mapZoom:2),gridData:(h:21,i:'1',w:40,x:0,y:0),id:'0fa4d020-3f4a-11e9-8d2e-3f08e16da335',panelIndex:'1',type:visualization,version:'6.6.1'),(embeddableConfig:(),gridData:(h:12,i:'2',w:10,x:21,y:21),id:'8745c590-3f49-11e9-8d2e-3f08e16da335',panelIndex:'2',type:visualization,version:'6.6.1'),(embeddableConfig:(),gridData:(h:10,i:'3',w:8,x:21,y:33),id:'6d19ca40-3f49-11e9-8d2e-3f08e16da335',panelIndex:'3',type:visualization,version:'6.6.1'),(embeddableConfig:(),gridData:(h:22,i:'4',w:28,x:0,y:43),id:'28311aa0-3f49-11e9-8d2e-3f08e16da335',panelIndex:'4',type:visualization,version:'6.6.1'),(embeddableConfig:(),gridData:(h:22,i:'5',w:21,x:0,y:21),id:eac48160-3f49-11e9-8d2e-3f08e16da335,panelIndex:'5',type:visualization,version:'6.6.1')),query:(language:kuery,query:''),timeRestore:!f,title:Users,viewMode:view)");
            JsonConvert.DeserializeObject(result);
        }
    }
}
