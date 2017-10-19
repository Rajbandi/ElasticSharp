using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElasticSharp.Core.Appendix
{
    public interface IAppendix
    {
        string Name { get; }

        byte Version { get; set; }

        byte[] ToBytes();

        void FromBytes(BinaryReader reader);

        void FromBytes(byte[] bytes);

        int Size { get;  }

        int FullSize { get;  }

        bool IsPhasable { get; set; }

    }
}
