using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSharp.Core
{
    public interface IAppendix
    {
        int GetSize();
        int GetFullSize();
        void PutBytes(byte[] bytes);
        byte GetVersion();

        string ToJson();

    }

    public abstract class Appendix : IAppendix
    {
        private byte version;

        public Appendix(byte version)
        {
            this.version = version;

        }
        public Appendix()
        {
            this.version = 1;
        }

        public abstract int GetMySize();
        public abstract int GetMyFullSize();

        public int GetFullSize()
        {
            return GetMyFullSize() + (version > 0 ? 1 : 0);
        }

        public int GetSize()
        {
            return GetSize() + (version > 0 ? 1 : 0);
        }

        public byte GetVersion()
        {
            return version;
        }

        public void PutBytes(byte[] bytes)
        {
            
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
    }
}
