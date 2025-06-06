using System.Diagnostics.CodeAnalysis;
using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

[SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty")]
internal sealed record ImportNotificationResponse : ImportPreNotification;
