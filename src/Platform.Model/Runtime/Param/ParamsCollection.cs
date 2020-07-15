using System.Collections.Concurrent;

namespace Platform.Model.Runtime.Param
{
    public class ParamsCollection : ConcurrentDictionary<string, string>, IParamsCollection
    {
    }
}
