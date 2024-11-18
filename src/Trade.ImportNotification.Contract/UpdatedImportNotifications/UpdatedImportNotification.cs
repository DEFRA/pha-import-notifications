namespace Trade.ImportNotification.Contract.UpdatedImportNotifications;
    
public class UpdatedImportNotification
{
    public ImportNotificationLinks Links { get; set; } = new();
}

public class ImportNotificationLinks
{
    public Uri Documents { get; set; }
    public Uri CustomsMovements { get; set; }
    public Uri GoodsMovements { get; set; }
        
}