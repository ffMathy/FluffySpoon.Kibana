using System.Linq;
using System.Runtime.InteropServices;

namespace FluffySpoon.Kibana.States
{
	internal class ValueKibanaUrlParserState : IKibanaUrlParserState
	{
		public string Handle(string content)
		{
			content = content.Trim();

			var result = string.Empty;

            var objectScopes = ScopeHelper
                .GetScopes(content, "(", ")")
                .Where(x => x.Content != string.Empty)
                .ToArray();
            foreach (var objectScope in objectScopes)
            {
                if (IsScopeValidNested(objectScope))
                {
                    if (objectScope.Prefix == "(")
                        result += new ObjectKibanaUrlParserState().Handle(objectScope.Content);

                    if (objectScope.Prefix == "!(")
                        result += new ArrayKibanaUrlParserState().Handle(objectScope.Content);
                }
                else
                {
                    var isString = content.StartsWith("'") && content.EndsWith("'");
                    if (isString)
                    {
                        content = content.Substring(1, content.Length - 2);
                    }

                    var isSpecialValue = content.StartsWith('!');
                    if (!isSpecialValue && content.Any(x => !char.IsNumber(x)))
                    {
                        isString = true;
                    }

                    if (isSpecialValue)
                    {
                        result += new PrimitiveKibanaUrlParserState().Handle(content);
                    }
                    else
                    {
                        if (isString)
                        {
                            result += "\"";
                            content = content
                                .Replace("\"", "\\\"")
                                .Replace("\n", "\\n")
                                .Replace("\r", "\\r");
                        }

                        result += content;

                        if (isString)
                            result += "\"";
                    }
                }
            }

			return result;
		}

		private static bool IsScopeValidNested(Scope objectScope)
		{
			return objectScope.Suffix != null && !objectScope.Prefix?.StartsWith('\'') == true;
		}
	}
}