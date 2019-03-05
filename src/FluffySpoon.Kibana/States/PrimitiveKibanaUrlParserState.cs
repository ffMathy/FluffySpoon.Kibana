using System.Linq;

namespace FluffySpoon.Kibana.States
{
	internal class PrimitiveKibanaUrlParserState : IKibanaUrlParserState
	{
		public string Handle(string content)
		{
			var afterExplamationMark = content.Substring(1);
			if(afterExplamationMark == "f")
				return "false";

			if(afterExplamationMark == "t")
				return "true";

			return "\"" + content + "\"";
		}
	}
}