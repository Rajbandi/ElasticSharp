using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElasticSharp.Core.Appendix
{
    public class MessageAppendix : IAppendix
    {
        public string Name { get; set; }
        public byte Version { get; set; }
        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public void FromBytes(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public void FromBytes(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public int Size { get; }
        public int FullSize { get; }
        public bool IsPhasable { get; set; }
    }
}
