namespace FluffySpoon.Kibana
{
	public class KibanaUrlParser
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
