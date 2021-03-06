﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Shiva.Tools
{
    /// <summary>
    /// Tools for string
    /// </summary>
    public static class StringTools
    {
        private static readonly Regex _formatRegex = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        /// <summary>
        /// The end format identifier
        /// </summary>
        private const string ENDFORMATID = "end";

        /// <summary>
        /// The format format identifier
        /// </summary>
        private const string FORMATFORMATID = "format";

        /// <summary>
        /// The property format identifier
        /// </summary>
        private const string PROPERTYFORMATID = "property";

        /// <summary>
        /// The start format identifier
        /// </summary>
        private const string STARTFORMATID = "start";
        /// <summary>
        /// Formats string of the by name value.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <param name="values">The values.</param>
        /// <param name="formatProvider">format provider</param>
        /// <returns>formated string</returns>
        public static string FormatByName(this string stringValue, IDictionary<string, object> values, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
                return string.Empty;

            if (values == null)
                return stringValue;

            var rewrittenstringformat = _formatRegex.Replace(stringValue, delegate (Match m)
            {
                var startGroup = m.Groups[STARTFORMATID];
                var propertyGroup = m.Groups[PROPERTYFORMATID];
                var formatGroup = m.Groups[FORMATFORMATID];
                var endGroup = m.Groups[ENDFORMATID];

                var index = values.Keys.ToList().IndexOf(propertyGroup.Value);
                if (index < 0)
                    return $"[NotFound:{propertyGroup.Value}]";
                return new string('{', startGroup.Captures.Count) + index + formatGroup.Value + new string('}', endGroup.Captures.Count);
            });
            if (formatProvider != null)
                return string.Format(formatProvider, rewrittenstringformat, values.Values.ToArray());
            else
                return string.Format(rewrittenstringformat, values.Values.ToArray());
        }
    }
}
