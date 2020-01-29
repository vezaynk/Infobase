using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Http;

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
            context.Values["datatool"] = translations.Translate(context.Values["datatool"] as string ?? context.AmbientValues["datatool"] as string);
            context.Values["controller"] = translations.Translate(context.Values["controller"] as string ?? context.AmbientValues["controller"] as string);


            if (languageChanged)
            {
                context.Values["id"] = context.Values["id"] as string ?? context.AmbientValues["id"] as string;
                var qs = context.HttpContext.Request.Query;
                foreach (var q in qs)
                {
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
            context.RouteData.Values["datatool"] = translations.LookupInvariant(context.RouteData.Values["datatool"] as string)?.ToUpper();
            context.RouteData.Values["action"] = translations.LookupInvariant(context.RouteData.Values["action"] as string);
            context.RouteData.Values["controller"] = translations.LookupInvariant(context.RouteData.Values["controller"] as string);

            return base.OnRouteMatched(context);
        }
    }

    public class I18nTransformer : IOutboundParameterTransformer
    {
        public string Culture { get; set; } = "en-ca";
        public bool TranslateMode { get; set; }
        public string TranslateTo { get; set; }
        public readonly IHttpContextAccessor _httpContextAccessor;

        public I18nTransformer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string TransformOutbound(object value)
        {
            var domainMapping = new Dictionary<string, string> {
                        { "english.localhost:5000", "en-ca"},
                        { "french.localhost:5000", "fr-ca"}
                    };

            if (_httpContextAccessor.HttpContext != null && domainMapping.TryGetValue(_httpContextAccessor.HttpContext.Request.Host.ToString(), out var culture))
                Culture = culture;

            var translations = new Dictionary<string, Translations>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "en-ca",
                    new Translations(new (string, string)[]
                    {
                        ("pass", "pass"),
                        ("pass2", "pass2"),
                        ("data-tool", "data-tool"),
                        ("index", "index"),
                        ("indicator-details", "indicator-details")
                    })
                },
                {
                    "fr-ca",
                    new Translations(new (string, string)[]
                    {
                        ("pass", "apcss"),
                        ("pass2", "apcss2"),
                        ("data-tool", "outil-de-donnees"),
                        ("index", "index"),
                        ("indicator-details", "description-de-mesure")
                    })
                },
            };

            return TranslateMode ? translations[TranslateTo].Translate(LookupInvariant(value.ToString())) : translations[Culture].Translate(value.ToString());
            
        }

        public string LookupInvariant(string value) {
            var domainMapping = new Dictionary<string, string> {
                        { "english.localhost:5000", "en-ca"},
                        { "french.localhost:5000", "fr-ca"}
                    };

            if (_httpContextAccessor.HttpContext != null && domainMapping.TryGetValue(_httpContextAccessor.HttpContext.Request.Host.ToString(), out var culture))
                Culture = culture;
            var translations = new Dictionary<string, Translations>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "en-ca",
                    new Translations(new (string, string)[]
                    {
                        ("pass", "pass"),
                        ("pass2", "pass2"),
                        ("data-tool", "data-tool"),
                        ("index", "index"),
                        ("indicator-details", "indicator-details")
                    })
                },
                {
                    "fr-ca",
                    new Translations(new (string, string)[]
                    {
                        ("pass", "apcss"),
                        ("pass2", "apcss2"),
                        ("data-tool", "outil-de-donnees"),
                        ("index", "index"),
                        ("indicator-details", "description-de-mesure")
                    })
                },
            };
            return translations[Culture].LookupInvariant(value);
        }

    }
}
