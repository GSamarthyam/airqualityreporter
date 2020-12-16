using System.Linq;
using System.Text;

namespace AirQualityReport.After
{
    //TODO: Use HTMLWriter instead of explicit HTML tages
    public class HTMLBuilder
    {
        private StringBuilder html = new StringBuilder(); 
        
        public HTMLBuilder AddHeading1(string heading)
        {
            html.Append($"<h1> {heading} </h1>");
            return this;
        }

        public HTMLBuilder AddHeading2(string heading)
        {
            html.Append($"<h2> {heading} </h2>");
            return this;
        }
        
        public HTMLBuilder AddTableHeader(string[] headers)
        {
            html.Append($"<table border=\"1\"><tr> ");
            foreach (var header in headers)
            {
                html.Append($"<th>{header}</th> ");
            }
             
            return this;
        }

        public HTMLBuilder AddTableValues(object tuple)
        {
            html.Append("<tr>");
            var tupleValues = tuple.GetType().GetProperties().Select(property => property.GetValue(tuple));
            foreach (var value in tupleValues)
            {
                html.Append($"<td>{value}</td>");
            }
            html.Append($"</tr>");
            return this; 
        }

        public HTMLBuilder CloseTableHeader()
        {
            html.Append(@"</table>");
            return this; 
        }

        public HTMLBuilder AddLineBreak()
        {
            html.Append("<br>");
            return this;
        }
        
        public HTMLBuilder AddBodyStart()
        {
            html.Append("<body>");
            return this;
        }

        public HTMLBuilder CloseBody()
        {
            html.Append(@"</body>");
            return this; 
        }

        public HTMLBuilder CloseHTML()
        {
            html.Append(@"</html>");
            return this; 
        }
        
        public string GenerateHTMLString()
        {
            return html.ToString(); 
        }
    }
}