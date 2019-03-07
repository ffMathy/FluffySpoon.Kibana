using System;
using System.Collections.Generic;
using System.Linq;

namespace FluffySpoon.Kibana
{
	internal class ScopeHelper
	{
		public static Scope[] GetScopes(string content, string entry, string exit)
		{
			var scopes = new List<Scope>();
			if (string.IsNullOrEmpty(content))
				return scopes.ToArray();

			var results = new string[]
			{
				null,
				null,
				null
			};

			var scope = 0;
			var area = 0;

			void PushScope()
			{
				var lastScope = scopes.Count == 0 ? null : scopes[scopes.Count - 1];
				var lastOffset = lastScope != null ? lastScope.Offset : 0;
				var offset = content.IndexOf(entry, lastOffset);

				var scopeObject = new Scope
				{
					Prefix = results[0]?.Substring(0, results[0].Length - (string.IsNullOrEmpty(results[1]) ? 0 : (entry.Length - 1))),
					Content = results[1],
					Suffix = results[2],
					Offset = offset,
					Length = entry.Length + (results[1]?.Length ?? 0) + exit.Length
				};

				scopeObject.Prefix = scopeObject.Prefix?.Trim();
				scopeObject.Content = scopeObject.Content?.Trim();
				scopeObject.Suffix = scopeObject.Suffix?.Trim();

				if (scopeObject.Prefix?.StartsWith(exit) == true)
					scopeObject.Prefix = scopeObject.Prefix.Substring(exit.Length).Trim();

				if (scopeObject.Suffix?.EndsWith(entry) == true)
					scopeObject.Suffix = scopeObject.Suffix.Substring(0, scopeObject.Suffix.Length - entry.Length).Trim();

				if (!string.IsNullOrEmpty(scopeObject.Suffix) || !string.IsNullOrEmpty(scopeObject.Content) || !string.IsNullOrEmpty(scopeObject.Prefix))
					scopes.Add(scopeObject);

				results[0] = string.IsNullOrEmpty(results[2]) ? string.Empty : results[2];
				results[1] = string.Empty;
				results[2] = string.Empty;
			};

			void PopCharacters(int count)
			{
				for (var i = 0; i < count; i++)
				{
					results[area] = results[area]?.Substring(0, results[area]?.Length == 0 ? 0 : results[area].Length - 1);
				}
			};

			void PushCharacter(string character)
			{
				results[area] += character;
			};

			EnumerateRelevantCodeCharacterRegions(content, (stringSoFar, character) =>
			{
				if (stringSoFar.EndsWith(exit))
				{
					scope--;
					if (scope == 0)
						area = 2;
				}

				PushCharacter(character);

				if (stringSoFar.EndsWith(entry))
				{
					scope++;
					if (scope == 1 && area == 2)
					{
						PushScope();
					}
					if (scope == 1)
					{
						PopCharacters(entry.Length - 1);
						area = 1;
					}
				}
			});

			PushScope();
			if (!string.IsNullOrEmpty(results[0]))
				PushScope();

			return scopes.ToArray();
		}

		public static string[] GetScopedList(string separator, string content, bool includeSeparatorInSplits = false)
		{
			var scopes = new[] {

                ("!(", ")"),
                ("(", ")"),
                ("'", "'"),
            };

			bool isEntryScope(string item) => scopes.Any(x => item.EndsWith(x.Item1));
			bool isExitScope(string item) => scopes.Any(x => item.EndsWith(x.Item2));
            int getScopeLength(string item) => Math.Max(
                scopes.FirstOrDefault(x => item.EndsWith(x.Item2)).Item1.Length,
                scopes.FirstOrDefault(x => item.EndsWith(x.Item2)).Item1.Length);

            var scope = 0;
			var totalStringSoFar = "";
            var lastSeenEntryScope = "";
			var splits = new List<string>();
            var skipNext = 0;

			void pushNewSplit()
			{
				totalStringSoFar = totalStringSoFar.Trim();
				if (string.IsNullOrEmpty(totalStringSoFar))
					return;

				splits.Add(totalStringSoFar);
				totalStringSoFar = "";
			};

            EnumerateRelevantCodeCharacterRegions(content, (stringSoFar, character) =>
            {
                var isCharacterExitScope = isExitScope(stringSoFar);
                var isCharacterEntryScope = isEntryScope(stringSoFar);

                var isCharacterBothScopes = isCharacterEntryScope && isCharacterExitScope;
                var isScope = isCharacterEntryScope || isCharacterExitScope;

                if(isScope)
                    skipNext = getScopeLength(stringSoFar) - 1;

                var shouldTreatAsExitScope = isCharacterBothScopes ?
                    character == lastSeenEntryScope :
                    isCharacterExitScope;
                if (shouldTreatAsExitScope && scope > 0)
                {
                    scope--;
                }
                else if (isCharacterEntryScope)
				{
					scope++;
                    lastSeenEntryScope = character;
				}

				if (scope == 0 && (character == separator || separator == ""))
				{
					if (separator == "" || includeSeparatorInSplits)
						totalStringSoFar += character;

					pushNewSplit();
				}
				else
				{
					totalStringSoFar += character;
				}
			});

			if (!string.IsNullOrEmpty(totalStringSoFar))
				pushNewSplit();

			return splits.ToArray();
		}

		static string EnumerateRelevantCodeCharacterRegions(string content, Action<string, string> enumerator = null)
		{
			if (content == null)
				return null;

			var insideString = false;
			var stringEntry = "";
			var insideStringEscapeCharacter = false;

			var stringSoFar = "";

			foreach (var character in content)
			{
				stringSoFar += character;

				if (insideString && character == '\\')
				{
					insideStringEscapeCharacter = true;
				}
				else if (insideString)
					insideStringEscapeCharacter = false;

				var isEnteringString = (character == '"' || character == '\'') && !insideString;
				var isExitingString = insideString && character == stringEntry.FirstOrDefault();
				if (!insideStringEscapeCharacter && (isEnteringString || isExitingString))
				{
					insideString = !insideString;
					stringEntry = character + "";
				}

				enumerator?.Invoke(stringSoFar, character.ToString());
			}

			return stringSoFar;
		}
	}

	internal class Scope
	{
		public Scope(string value = null)
		{
			if (value == null)
				return;

			Prefix = value;
			Length = value.Length;
			Suffix = string.Empty;
			Content = string.Empty;
			Offset = 0;
		}

		public string Prefix { get; set; }
		public string Content { get; set; }
		public string Suffix { get; set; }

		public int Offset { get; set; }
		public int Length { get; set; }
	}
}
