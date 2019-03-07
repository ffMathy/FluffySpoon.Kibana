namespace FluffySpoon.Kibana
{
	public interface IKibanaParser
	{
		string ConvertQueryParameterValueToJson(string value);
        string ConvertUrlToElasticsearchQueryString(string url, string timeFilterFieldName);
    }
}