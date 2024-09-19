using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumCsharpFramework.Utilities
{
    internal class JsonReader
    {
        public string ExtractData(string TokenName)
        {
            string ReadData= File.ReadAllText("C:\\Users\\ugillani\\source\\repos\\AppiumCsharpFramework\\AppiumCsharpFramework\\Utilities\\TestData.json");
            var jsonobject = JToken.Parse(ReadData);
            return jsonobject.SelectToken(TokenName).Value<string>();
        }
    }
}
