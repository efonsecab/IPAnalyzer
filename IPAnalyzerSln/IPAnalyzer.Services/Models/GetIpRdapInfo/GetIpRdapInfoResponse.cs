using System;
using System.Collections.Generic;
using System.Text;

namespace IPAnalyzer.Services.Models.GetIpRdapInfo
{

    public class GetIpRdapInfoResponse
    {
        public string objectClassName { get; set; }
        public string handle { get; set; }
        public string parentHandle { get; set; }
        public string startAddress { get; set; }
        public string endAddress { get; set; }
        public string ipVersion { get; set; }
        public string type { get; set; }
        public Entity[] entities { get; set; }
        public Event1[] events { get; set; }
        public Link1[] links { get; set; }
        public Remark[] remarks { get; set; }
        public string lacnic_originAutnum { get; set; }
        public string lacnic_legalRepresentative { get; set; }
        public object[] lacnic_reverseDelegations { get; set; }
        public Cidr0_Cidrs[] cidr0_cidrs { get; set; }
        public string[] rdapConformance { get; set; }
        public Notice[] notices { get; set; }
        public string port43 { get; set; }
    }

    public class Entity
    {
        public string objectClassName { get; set; }
        public string handle { get; set; }
        public string[] roles { get; set; }
        public object[] vcardArray { get; set; }
        public Event[] events { get; set; }
        public Link[] links { get; set; }
    }

    public class Event
    {
        public string eventAction { get; set; }
        public DateTime eventDate { get; set; }
    }

    public class Link
    {
        public string value { get; set; }
        public string rel { get; set; }
        public string type { get; set; }
        public string href { get; set; }
    }

    public class Event1
    {
        public string eventAction { get; set; }
        public DateTime eventDate { get; set; }
    }

    public class Link1
    {
        public string value { get; set; }
        public string rel { get; set; }
        public string type { get; set; }
        public string href { get; set; }
    }

    public class Remark
    {
        public object[] description { get; set; }
        public string title { get; set; }
    }

    public class Cidr0_Cidrs
    {
        public string v4prefix { get; set; }
        public int length { get; set; }
    }

    public class Notice
    {
        public string title { get; set; }
        public string[] description { get; set; }
        public Link2[] links { get; set; }
    }

    public class Link2
    {
        public string value { get; set; }
        public string rel { get; set; }
        public string type { get; set; }
        public string href { get; set; }
    }

}
