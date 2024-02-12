using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CommonUtils.DateTime;

public class DateOnlySchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(DateOnly))
        {
            schema.Properties.Clear();
            schema.Properties.Add("year", new OpenApiSchema() { Type = "integer" });
            schema.Properties.Add("month", new OpenApiSchema() { Type = "integer" });
            schema.Properties.Add("day", new OpenApiSchema() { Type = "integer" });
        }
    }  
}
