using System;
using System.Runtime.Serialization;
using System.Globalization;

namespace WebAPIClient
{
    [DataContract(Name="repo")]
    public class Repository
    {
        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="full_name")]
        public string FullName { get; set; }

        [DataMember(Name="size")]
        public int Size { get; set; }

        [DataMember(Name="description")]
        public string Description { get; set; }

        [DataMember(Name="git_url")]
        public Uri GitUrl { get; set; }

        [DataMember(Name="pushed_at")]
        private string JsonDate {get; set;}

        [IgnoreDataMember]
        public DateTime LastPush
        {
            get
            { 
                return DateTime.ParseExact(JsonDate,"yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            }
        }
    }
}