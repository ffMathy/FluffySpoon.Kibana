using System.Linq;

namespace FluffySpoon.Kibana.States
{
	internal class ObjectKibanaUrlParserState : IKibanaUrlParserState
	{
		public string Handle(string content)
		{
			var result = string.Empty;
			result += "{";

			var propertyScopes = ScopeHelper
				.GetScopedList(",", content)
				.Select(x => x.Trim());
			var count = 0;
			foreach (var propertyScope in propertyScopes)
			{
				if (count++ > 0)
					result += ",";

				var keyValuePair = ScopeHelper
					.GetScopedList(":", propertyScope)
					.Select(x => x.Trim())
					.ToArray();

				result += "\"";
				result += keyValuePair[0];
				result += "\":";

				result += new ValueKibanaUrlParserState().Handle(keyValuePair[1]);
			}

			result += "}";
			return result;
		}
	}
}