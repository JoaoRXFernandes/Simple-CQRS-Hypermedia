using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace Web.Website.Infrastructure
{
    public class RazorConfiguration : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "System";
            yield return "System.Configuration";

            yield return "System.Web";

            yield return "Nancy.ViewEngines.Razor";

            yield return "Web";
            yield return "Queries";
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "System";
            yield return "System.Configuration";
            yield return "System.Collections";

            yield return "System.Web";

            yield return "Nancy.ViewEngines.Razor";

            yield return "Web";
            yield return "Queries";
        }

        public bool AutoIncludeModelNamespace => true;
    }
}