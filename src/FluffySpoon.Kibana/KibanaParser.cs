﻿using FluffySpoon.Kibana.States;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace FluffySpoon.Kibana
{
    public class KibanaParser : IKibanaParser
    {
        public string ConvertQueryParameterValueToJson(string value)
        {
            if (value == null)
                return null;

            value = value.Trim();

            return new ValueKibanaUrlParserState().Handle(value);
        }

        private string ExtractQueryParameterValueFromUrl(string url, string key)
        {
            var uri = new Uri(url.Replace("#", ""));
            var queryString = QueryHelpers.ParseQuery(uri.Query); ;

            var parameters = queryString[key];
            return parameters;
        }

        public string ConvertUrlToElasticsearchQueryString(string url, string timeFilterFieldName)
        {
            var gObject = JsonConvert.DeserializeObject<JObject>(
                ConvertQueryParameterValueToJson(
                    ExtractQueryParameterValueFromUrl(url, "_g")));

            var aObject = JsonConvert.DeserializeObject<JObject>(
                ConvertQueryParameterValueToJson(
                    ExtractQueryParameterValueFromUrl(url, "_a")));

            var from = "now-15m";
            var to = "now";

            var time = (JObject)gObject.Property("time")?.Value;
            if (time != null)
            {
                from = (string)time.Property("from")?.Value ?? from;
                to = (string)time.Property("to")?.Value ?? to;
            }

            var filters = (JArray)aObject.Property("filters")?.Value;
            var filterValues = filters?.Values<JObject>();

            var mustArray = new JArray();
            mustArray.Add(new JObject()
            {
                {
                    "range",
                    new JObject() {
                        {
                            timeFilterFieldName,
                            new JObject() {
                                { "gte", from },
                                { "lte", to }
                            }
                        }
                    }
                }
            });

            var mustNotArray = new JArray();

            if (filterValues != null)
            {
                foreach (var filter in filterValues)
                {
                    var meta = (JObject)filter.Property("meta").Value;
                    var shouldNegate = (bool)meta.Property("negate").Value;

                    var targetArray = shouldNegate ? mustNotArray : mustArray;

                    var queryProperty = (JObject)filter.Property("query")?.Value;
                    if (queryProperty != null)
                    {
                        var matchProperty = (JObject)queryProperty.Property("match").Value;

                        var matchProperties = matchProperty
                            .Properties()
                            .Select(x => x.Value)
                            .OfType<JObject>();

                        foreach (var property in matchProperties)
                        {
                            var type = property.Property("type").Value.ToString();
                            if (type != "phrase")
                                throw new InvalidOperationException("Unknown search type: " + type);

                            property.Remove("type");
                        }

                        queryProperty.Remove("match");
                        queryProperty.Add("match_phrase", matchProperty);

                        targetArray.Add(queryProperty);
                    }

                    var existsProperty = (JObject)filter.Property("exists")?.Value;
                    if (existsProperty != null)
                    {
                        targetArray.Add(new JObject()
                        {
                            {"exists", existsProperty}
                        });
                    }
                }
            }

            var boolQuery = new JObject
            {
                { "must", mustArray },
                { "must_not", mustNotArray }
            };

            var queryContainer = new JObject
            {
                {"bool", boolQuery}
            };

            return queryContainer.ToString();
        }
    }
}