using FluentValidation;

namespace Defra.PhaImportNotifications.Api.Endpoints;

public class ChedReferenceNumberValidator : AbstractValidator<string>
{
    public ChedReferenceNumberValidator()
    {
        RuleFor(x => x).Matches(Regexes.ChedReferenceNumber).OverridePropertyName(nameof(Regexes.ChedReferenceNumber));
    }
}
