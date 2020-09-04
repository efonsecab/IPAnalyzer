using System;
using System.Collections.Generic;
using System.Text;

namespace IPAnalyzer.Services.Models.GetDomainRdapInfo
{

    public class GetDomainRdapInfoResponse
    {
        public string objectClassName { get; set; }
        public string handle { get; set; }
        public string ldhName { get; set; }
        public Link[] links { get; set; }
        public string[] status { get; set; }
        public Entity[] entities { get; set; }
        public Event[] events { get; set; }
        public Securedns secureDNS { get; set; }
        public Nameserver[] nameservers { get; set; }
        public string[] rdapConformance { get; set; }
        public Notice[] notices { get; set; }
    }

    public class Securedns
    {
        public bool delegationSigned { get; set; }
    }

    public class Link
    {
        public string value { get; set; }
        public string rel { get; set; }
        public string href { get; set; }
        public string type { get; set; }
    }

    public class Entity
    {
        public string objectClassName { get; set; }
        public string handle { get; set; }
        public string[] roles { get; set; }
        public Publicid[] publicIds { get; set; }
        public object[] vcardArray { get; set; }
        public Entity1[] entities { get; set; }
    }

    public class Publicid
    {
        public string type { get; set; }
        public string identifier { get; set; }
    }

    public class Entity1
    {
        public string objectClassName { get; set; }
        public string[] roles { get; set; }
        public object[] vcardArray { get; set; }
    }

    public class Event
    {
        public string eventAction { get; set; }
        public DateTime eventDate { get; set; }
    }

    public class Nameserver
    {
        public string objectClassName { get; set; }
        public string ldhName { get; set; }
    }

    public class Notice
    {
        public string title { get; set; }
        public string[] description { get; set; }
        public Link1[] links { get; set; }
    }

    public class Link1
    {
        public string href { get; set; }
        public string type { get; set; }
    }

}
