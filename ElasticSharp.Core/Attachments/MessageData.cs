/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: MessageData.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSharp.Core.Attachments
{
    /// <summary>
    /// This class is used to represent message attachment data.
    /// EncryptedMessage and PrunableEncryptedMessage 
    /// </summary>
    [DataContract]
    public class MessageData : IJsonObject
    {
        [DataMember(Name = "data")]
        public string Data { get; set; }
        
        [DataMember(Name = "nonce")]
        public string Nonce { get; set; }

        [DataMember(Name = "isText")]
        public bool IsText { get; set; }

        [DataMember(Name = "isCompressed")]
        public bool IsCompressed { get; set; }


        public static MessageData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MessageData>(json);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
