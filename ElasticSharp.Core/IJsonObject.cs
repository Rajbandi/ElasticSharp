using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSharp.Core
{
    public interface IJsonObject
    {
        void FromJson(string json);
        string ToJson();
    }
}
