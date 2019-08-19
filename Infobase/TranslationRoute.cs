using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Infobase
{
    public class TranslationRoute : Route
    {
        private readonly Dictionary<string, Translations> _translations;
        private readonly Dictionary<string, string> _domainLanguageMapping;
        private string _defaultLanguage;


        public TranslationRoute(
            Dictionary<string, Translations> translations,
            Dictionary<string, string> domainLanguageMapping,
            string defaultLanguage,
            IRouter target,
            string routeName,
            string routeTemplate,
            RouteValueDictionary defaults,
            IDictionary<string, object> constraints,
            RouteValueDictionary dataTokens,
            IInlineConstraintResolver inlineConstraintResolver)
            : base(target, routeName, routeTemplate, defaults, constraints, dataTokens, inlineConstraintResolver)
        {
            _translations = translations;
            _defaultLanguage = defaultLanguage;
            _domainLanguageMapping = domainLanguageMapping;
        }

        public override VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            var newLanguage = context.Values["language"] as string;
            var pageLanguage = context.AmbientValues["language"] as string;
            var languageChanged = newLanguage != pageLanguage && newLanguage != null;
            var language = newLanguage ?? pageLanguage;
        
            // Forces the matching route to be used
            // context.Values["language"] = language;
            context.Values.Remove("language");
            // context.Values["host"] = translations.LookupInvariant(context.Values["host"] as string);
            // context.Values["area"] = translations.Translate(context.Values["area"] as string ?? context.AmbientValues["area"] as string);

            _translations.TryGetValue(language, out var translations);

            context.Values["action"] = translations.Translate(context.Values["action"] as string ?? context.AmbientValues["action"] as string);
            context.Values["controller"] = translations.Translate(context.Values["controller"] as string ?? context.AmbientValues["controller"] as string);
            context.Values["id"] = context.Values["id"] as int? ?? context.AmbientValues["id"] as int?;

            if (languageChanged) {
                var qs = context.HttpContext.Request.Query;
                foreach (var q in qs) {
                    context.Values[q.Key] = q.Value;
                }
            }

            var virtualPathData = base.GetVirtualPath(context);
            return virtualPathData;
        }
        protected override Task OnRouteMatched(RouteContext context)
        {
            string host = context.HttpContext.Request.Host.ToString();
            string language = _domainLanguageMapping.GetValueOrDefault(host, _defaultLanguage);

            _translations.TryGetValue(language, out var translations);
            context.RouteData.Values["language"] = language;
            context.RouteData.Values["action"] = translations.LookupInvariant(context.RouteData.Values["action"] as string);
            context.RouteData.Values["controller"] = translations.LookupInvariant(context.RouteData.Values["controller"] as string);

            return base.OnRouteMatched(context);
        }
    }
}
