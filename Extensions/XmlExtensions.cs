using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PolyhydraGames.Extensions
{
    public static class XmlExtensions
    {
        public static bool Contains(this XAttribute attribute, string value)
        {

            return (attribute != null && attribute.Value.Contains(value));
        }
        public static T AttributeToEnum<T>(this XElement item, string attribute) where T : struct
        {
            var value = (item?.Attribute(attribute) != null) ? item.Attribute(attribute).Value : "";
            return value.ToEnum<T>();
        }
        public static string AttributeToString(this XElement item, string attribute)
        {
            if (item == null) return "";
            var xelement = item.Attribute(attribute);
            return xelement?.Value ?? "";
        }
        public static int AttributeToInt(this XElement item, string attribute,int dflt = 0)
        {
            if (item == null) return dflt;
            var xelement = item.Attribute(attribute);
            return xelement?.Value.ToInt() ?? dflt;
        }

        public static bool AttributeToBoolean(this XElement item, string attribute)
        {
            if (item == null)
                return false;
            var xelement = item.Attribute(attribute);
            return (xelement != null) && xelement.Value != "" && bool.Parse(xelement.Value);
        }

        public static bool AttributeToBoolFromInt(this XElement item, string attribute)
        {
            if (item == null)
                return false;
            var xelement = item.Attribute(attribute);
            return (xelement != null) && xelement.Value.ToInt() > 0;
        }

        public static int ToIntOrNeg1_old(this XElement item, string attribute)
        {
            if (item == null)
                return -1;
            var xelement = item.Attribute(attribute);
            return (xelement != null) ? xelement.Value.ToInt() : -1;
        }
        public static int ToIntOrNeg1(this XElement item, string attribute)
        {
            if (item == null)
                return -1;
            var xelement = item.Attribute(attribute);
            return (xelement != null) ? int.Parse(xelement.Value)  : -1;
        }
        public static Dictionary<string, string> ToDictionary(this XElement myRow)
        {
            return (myRow != null)
                       ? myRow.Attributes().ToDictionary(item => item.Name.ToString(), item => item.Value)
                       : null;
        }

        public static string ReportAttributes(this XElement myRow, string split = ",")
        {
            var list = new List<string>();

            if (myRow != null)
            {
                list.AddRange(myRow.Attributes().Select(item => item.Name + ":" + item.Value));
            }

            return list.ToCodedArray(split);
        }

        public static string ReportElements(this XElement myRow, string split = ",")
        {
            var list = new List<string>();

            if (myRow != null)
            {
                list.AddRange(myRow.Elements().Select(item => item.Name + ":" + item.Value));
            }

            return list.ToCodedArray(split);
        }
 
        public static IEnumerable<XElement> DescendentsFromString(string code, string name)
        {
            var parse = XDocument.Parse(code);
            return parse.Descendants(name);
        }
    }

 
}
 