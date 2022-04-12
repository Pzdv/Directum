using System;
using System.Collections.Generic;
using System.Globalization;
namespace ClassLibrary1
{
    public class LocalizationFactory
    {
        private readonly List<ISource> _sources;

        public LocalizationFactory()
        {
            _sources = new List<ISource>();
        }

        public string GetString(string stringCode, CultureInfo cultureInfo)
        {
            if(cultureInfo == null)
            {
                cultureInfo = CultureInfo.CurrentCulture;
            }

            var targetSources = _sources.Where(x => x.SourceCulture == cultureInfo);

            if(!targetSources.Any())
            {
                throw new ArgumentException($"Для переданного объекта {cultureInfo} Name = {cultureInfo.Name} нет зарегистрированных источников");
            }

            var localizationString = targetSources.Select(x => x.GetStringByCode(stringCode)).FirstOrDefault() ?? string.Empty;

            return localizationString;
        }

        public void RegisterSource(ISource source)
        {
            if(!_sources.Contains(source))
            {
                _sources.Add(source);
            }
        }
    }
}
