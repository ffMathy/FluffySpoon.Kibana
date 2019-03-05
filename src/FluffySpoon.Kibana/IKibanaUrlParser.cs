namespace FluffySpoon.Kibana
{
	public interface IKibanaUrlParser
	{
		string ConvertQueryParameterValueToJson(string value);
	}
}