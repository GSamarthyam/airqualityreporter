using System;
using System.Collections.Generic;

namespace AirQualityReport.After
{
    public interface IFieldFormatter
    {
        //TODO: Support for 4 columns for now
        IEnumerable<KeyValuePair<string, List<Tuple<int, int, int, string>>>> Values { get; }
        string LastUpdatedTime { get; }
        string[] Headers { get; }
    }
}