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
			Assert.AreEqual("{\"a\":1,\"b\":\"foo\"}", new KibanaUrlParser().ConvertQueryParameterValueToJson(
				"(a:1,b:'foo')"));
		}
	}
}
