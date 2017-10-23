/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: EncryptedMessageAttachment.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace ElasticSharp.Core.Attachments
{
    [DataContract]
    public class EncryptedMessageAttachment : MessageAttachment
    {
        public EncryptedMessageAttachment()
        {
            Version = 1;
        }

        public EncryptedMessageAttachment(int version)
        {
            Version = version;
        }

        public override string Name => "EncryptedMessage";

        
        public override int Size { get; }
        public override int FullSize { get; }

        [DataMember(Name= "encryptedMessage")]
        public MessageData Data { get; set; }

        [DataMember(Name= "version.EncryptedMessage")]
        public int Version { get; set; }

        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public override void FromBytes(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

    }
}
