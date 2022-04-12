using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface ISource
    {
        CultureInfo SourceCulture { get; }
        string GetStringByCode(string code);
    }
}
