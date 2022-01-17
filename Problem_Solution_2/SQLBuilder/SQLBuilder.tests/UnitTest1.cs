using System;
using Newtonsoft.Json;
using SQLBuilder.models;
using Xunit;

namespace SQLBuilder.tests
{
    public class SQLBuilderTests
    {
        [Fact]
        public void SQLBuilder_When_Valid_Input_Should_Match()
        {
            var input = "{\n  \"table\": \"TestTable\",\n  \"columns\": [\n    {\n      \"operator\": \"IN\",\n      \"fieldName\": \"column1\",\n      \"fieldValue\": \"value1\"\n    },\n    {\n      \"operator\": \"IN\",\n      \"fieldName\": \"column1\",\n      \"fieldValue\": \"value2\"\n    },\n    {\n      \"operator\": \"LIKE\",\n      \"fieldName\": \"column3\",\n      \"fieldValue\": \"value1\"\n    },\n    {\n      \"operator\": \"LIKE\",\n      \"fieldName\": \"column3\",\n      \"fieldValue\": \"value2\"\n    },\n    {\n      \"operator\": \"LIKE\",\n      \"fieldName\": \"column4\",\n      \"fieldValue\": \"value1\"\n    },\n    {\n      \"operator\": \"Equal\",\n      \"fieldName\": \"column2\",\n      \"fieldValue\": \"value1\"\n    },\n    {\n      \"operator\": \"Equal\",\n      \"fieldName\": \"column2\",\n      \"fieldValue\": \"value2\"\n    }\n  ]\n}";
            Queries queryInput = JsonConvert.DeserializeObject<Queries>(input);
            SQLQueryConstructor queryConstructor = new SQLQueryConstructor();
            QueryBuilder sqlBuilder = new SQLQueryBuilder(queryInput);
            queryConstructor.Construct(sqlBuilder, SQLOperators.Operators);
            Assert.Equal("SELECT column1,column3,column4,column2, FROM TestTable WHERE column1 IN (value1,value2) AND (column3 LIKE value1 OR column3 LIKE value2 ) AND (column4 LIKE value1 ) AND (column2 = value1 OR column2 = value2 ) ", sqlBuilder.QueryString);
        }

        [Fact]
        public void SQLBuilder_When_Null_Should_Throw()
        {
            SQLQueryConstructor queryConstructor = new SQLQueryConstructor();
            Assert.Throws(typeof(ArgumentNullException),() => new SQLQueryBuilder(null));
           
        }
    }
}
