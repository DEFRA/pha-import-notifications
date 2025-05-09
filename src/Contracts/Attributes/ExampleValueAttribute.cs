namespace Defra.PhaImportNotifications.Contracts;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class ExampleValueAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}
