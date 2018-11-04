using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace yasuiViewer2
{
    [JsonObject("item")]
    class item
    {

        [JsonProperty("RESULT")]
        public String RESULT { get; set; }
        [JsonProperty("JAN")]
        public String JAN { get; set; }
        [JsonProperty("PRODUCT")]
        public String PRODUCT { get; set; }
        [JsonProperty("URL")]
        public String URL { get; set; }
        [JsonProperty("PRICE")]
        public String PRICE { get; set; }
    }
}
