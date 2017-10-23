/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: Attachment.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ElasticSharp.Core.Attachments
{
    /// <summary>
    /// This class represents base class for all transaction attachments. 
    /// </summary>
    public abstract class Attachment : IAttachment, IJsonObject
    {
        #region public properties
        /// <summary>
        /// Attachment name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the attachment size
        /// </summary>
        public abstract int Size { get; }

        /// <summary>
        /// Gets the attachment full size
        /// </summary>
        public abstract int FullSize { get; }

        /// <summary>
        /// Denotes attachment phasable or not
        /// </summary>
        public bool IsPhasable { get; set; }

        #endregion

        #region public methods
        /// <summary>
        /// Gets attachment bytes
        /// </summary>
        /// <returns></returns>
        public abstract byte[] ToBytes();

        /// <summary>
        /// Loads attachment from byte reader
        /// </summary>
        /// <param name="reader"></param>
        public abstract void FromBytes(BinaryReader reader);

        /// <summary>
        /// Loads attachment from bytes
        /// </summary>
        /// <param name="bytes"></param>
        public void FromBytes(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                using (var br = new BinaryReader(ms))
                {
                    FromBytes(br);
                }
            }
        }

        /// <summary>
        /// Gets the attachment json
        /// </summary>
        /// <returns></returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }


        public static string ToJson(IEnumerable<IAttachment> attachments)
        {
            var json = new JObject();
            
            foreach (var attachment in attachments)
            {
                var obj = JObject.FromObject(attachment);
                json.Merge(obj);
            }

            return json.ToString();
        }
        #endregion
    }
}
