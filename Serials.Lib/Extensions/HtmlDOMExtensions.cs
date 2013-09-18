using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Serials.Lib.Extensions
{
    public static class HtmlDOMExtensions
    {
        public static IEnumerable<XElement> WithId(this IEnumerable<XElement> elements, Func<string, bool> predicate)
        {
            return elements.Where(AttributeMatch("id", predicate));
        }

        private static Func<XElement, bool> AttributeMatch(string attribute, Func<string, bool> predicate)
        {
            return x => x.Attribute(attribute) != null && predicate.Invoke(x.Attribute(attribute).Value);
        }

        public static string AsNumberWithLeadingZero(this int value)
        {
            return value.ToString("00");
        }
    }
}