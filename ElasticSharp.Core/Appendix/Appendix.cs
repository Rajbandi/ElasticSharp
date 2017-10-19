using System.IO;

namespace ElasticSharp.Core.Appendix
{
    public abstract class Appendix : IAppendix
    {
        protected Appendix(int version)
        {
            Version = (byte) version;
        }
        protected Appendix()
        {
            Version = 1;
        }

        public abstract string Name { get; }

        public byte Version { get; set; }

        public abstract byte[] ToBytes();

        public abstract void FromBytes(BinaryReader reader);

        public void FromBytes(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                using (var io = new BinaryReader(ms))
                {
                    FromBytes(io);
                }
            }
        }

        public int Size { get; }
        public int FullSize { get; }
        public bool IsPhasable { get; set; }
    }
}
