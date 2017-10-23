/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: IAttachment.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System.IO;

namespace ElasticSharp.Core.Attachments
{
    public interface IAttachment
    {
        string Name { get; }

        int Size { get; }

        int FullSize { get; }

        bool IsPhasable { get; set; }

        byte[] ToBytes();

        void FromBytes(BinaryReader reader);

        void FromBytes(byte[] bytes);


    }
}
