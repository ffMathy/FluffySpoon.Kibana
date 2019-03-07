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
                ("(", ")")
            };

            string GetMatchingEntryScope(string item) => scopes.FirstOrDefault(x => item.EndsWith(x.Item1)).Item1;
            string GetMatchingExitScope(string item) => scopes.FirstOrDefault(x => item.EndsWith(x.Item2)).Item2;

            var scope = 0;
            var totalStringSoFar = "";
            var lastSeenEntryScope = "";
            var splits = new List<string>();

            void PushNewSplit()
            {
                totalStringSoFar = totalStringSoFar.Trim();
                if (string.IsNullOrEmpty(totalStringSoFar))
                    return;

                splits.Add(totalStringSoFar);
                totalStringSoFar = "";
            };

            var isInsideString = false;
            EnumerateRelevantCodeCharacterRegions(content, (stringSoFar, character) =>
            {
                if (character == "'")
                    isInsideString = !isInsideString;

                if (!isInsideString)
                {
                    var characterExitScope = GetMatchingExitScope(stringSoFar);
                    var characterEntryScope = GetMatchingEntryScope(stringSoFar);

                    var isCharacterExitScope = characterExitScope != null;
                    var isCharacterEntryScope = characterEntryScope != null;

                    var isCharacterBothScopes = isCharacterEntryScope && isCharacterExitScope;

                    var shouldTreatAsExitScope = isCharacterBothScopes
                        ? characterExitScope == lastSeenEntryScope
                        : isCharacterExitScope;
                    if (shouldTreatAsExitScope && scope > 0)
                    {
                        scope--;
                    }
                    else if (isCharacterEntryScope)
                    {
                        scope++;
                        lastSeenEntryScope = characterEntryScope;
                    }

                    if (scope == 0 && (character == separator || separator == ""))
                    {
                        if (separator == "" || includeSeparatorInSplits)
                            totalStringSoFar += character;

                        PushNewSplit();
                    }
                    else
                    {
                        totalStringSoFar += character;
                    }
                }
                else
                {
                    totalStringSoFar += character;
                }
            });

            if (!string.IsNullOrEmpty(totalStringSoFar))
                PushNewSplit();

            return splits.ToArray();
        }

        static void EnumerateRelevantCodeCharacterRegions(string content, Action<string, string> enumerator = null)
        {
            if (content == null)
                return;

            var stringSoFar = "";
            foreach (var character in content)
            {
                stringSoFar += character;
                enumerator?.Invoke(stringSoFar, character.ToString());
            }
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
