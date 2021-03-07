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
        IReadOnlyList<ConnectionInformation> ParseByFileName(string fileName);
        IReadOnlyList<ConnectionInformation> Parse(string text);
        IReadOnlyList<AttackerInfo> GetAttackerInfoList(IEnumerable<ConnectionInformation> connectionInformationList);
    }
}
