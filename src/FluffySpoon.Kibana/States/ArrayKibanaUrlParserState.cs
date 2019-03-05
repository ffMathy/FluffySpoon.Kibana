using System.Linq;

namespace FluffySpoon.Kibana.States
{
	internal class ArrayKibanaUrlParserState : IKibanaUrlParserState
	{
		public string Handle(string content)
		{
			var result = string.Empty;
			result += "[";

			var arrayScopes = ScopeHelper
				.GetScopedList(",", content)
				.Select(x => x.Trim());
			var count = 0;
			foreach (var arrayScope in arrayScopes)
			{
				if (count++ > 0)
					result += ",";

				result += new ValueKibanaUrlParserState().Handle(arrayScope);
			}

			result += "]";
			return result;
		}
	}
}