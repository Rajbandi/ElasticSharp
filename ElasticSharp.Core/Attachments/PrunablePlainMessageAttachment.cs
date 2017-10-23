/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: PrunablePlainMessageAttachment.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ElasticSharp.Core.Attachments
{
    [DataContract]
    public class PrunablePlainMessageAttachment : MessageAttachment
    {
        public override string Name { get; }
        public override int Size { get; }
        public override int FullSize { get; }

        [DataMember(Name= "version.PrunablePlainMessage")]
        public int Version { get; set; }

        [DataMember(Name = "messageIsText")]
        public bool IsMessageText { get; set; }

        [DataMember(Name = "messageHash")]
        public string MessageHash { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

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
