using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SQLBuilder.models
{
    public class Column
    {
        [JsonProperty("operator")]
        public string operatorname { get; set; }
        [JsonProperty("fieldName")]
        public string fieldName { get; set; }
        [JsonProperty("fieldValue")]
        public string fieldValue { get; set; }
    }

    public class Queries
    {
        [JsonProperty("table")]
        public string tableName { get; set; }
        [JsonProperty("columns")]
        public List<Column> columns { get; set; }
    }
}
