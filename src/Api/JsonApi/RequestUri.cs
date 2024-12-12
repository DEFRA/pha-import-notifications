using System.Text;

namespace Defra.PhaImportNotifications.Api.JsonApi;

public record RequestUri(string Path, FilterExpression? Filter = null, FieldExpression[]? Fields = null)
{
    public override string ToString()
    {
        var result = new StringBuilder();

        result.Append(Path);

        var queryIncluded = false;

        if (Filter is not null)
        {
            result.Append("?filter=");
            result.Append(Uri.EscapeDataString(Filter.ToString()));

            queryIncluded = true;
        }

        if (Fields is not null)
        {
            foreach (var field in Fields)
            {
                var fieldAsString = field.ToString();

                if (string.IsNullOrEmpty(fieldAsString))
                    continue;

                if (!queryIncluded)
                {
                    result.Append('?');
                    queryIncluded = true;
                }
                else
                {
                    result.Append('&');
                }

                result.Append(Uri.EscapeDataString(fieldAsString));
            }
        }

        return result.ToString();
    }
}
