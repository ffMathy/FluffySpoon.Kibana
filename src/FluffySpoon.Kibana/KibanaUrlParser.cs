using FluffySpoon.Kibana.States;

namespace FluffySpoon.Kibana
{
	public class KibanaUrlParser : IKibanaUrlParser
	{
		public string ConvertQueryParameterValueToJson(string value)
		{
			if (value == null)
				return null;

			value = value.Trim();

			return new ValueKibanaUrlParserState().Handle(value);
		}
	}
}
