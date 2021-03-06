using CrossCutting.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogFileAnalyzation.Contract
{
    public interface ILogFileParser
    {
        IReadOnlyCollection<ConnectionInformation> ParseByFileName(string fileName);
        IReadOnlyCollection<ConnectionInformation> Parse(string text);
    }
}
