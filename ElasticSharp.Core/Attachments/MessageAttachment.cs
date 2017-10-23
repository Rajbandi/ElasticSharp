/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: MessageAttachment.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ElasticSharp.Core.Attachments
{
    [DataContract]
    public class MessageAttachment : Attachment
    {
        #region public properties

        public override string Name { get; }

        public override int Size { get; }

        public override int FullSize { get; }

        [DataMember(Name = "version.ArbitraryMessage")]
        public int ArbitraryMessageVersion { get; set; }

        #endregion

        #region public methods

        public void Encrypt(string secretPhrase)
        {
        }

        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public override void FromBytes(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion

    }
}
