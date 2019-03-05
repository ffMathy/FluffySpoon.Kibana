using System.Linq;
using System.Runtime.InteropServices;

namespace FluffySpoon.Kibana
{
	internal class ValueKibanaUrlParserState : IKibanaUrlParserState
	{
		public string Handle(string content)
		{
			content = content.Trim();

			var result = string.Empty;
			
			var objectScope = ScopeHelper.GetScopes(content, "(", ")").SingleOrDefault();
			var arrayScope = ScopeHelper.GetScopes(content, "[", "]").SingleOrDefault();

			if (!string.IsNullOrEmpty(objectScope?.Prefix) && !string.IsNullOrEmpty(objectScope.Suffix))
			{
				result += new ObjectKibanaUrlParserState().Handle(objectScope.Content);
			}
			else
			{
				var isString = content.StartsWith("'") && content.EndsWith("'");
				if (isString)
				{
					result += "\"";
					content = content.Substring(1, content.Length - 2);
				}

				result += content;
					
				if (isString)
					result += "\"";
			}

			return result;
		}
	}

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