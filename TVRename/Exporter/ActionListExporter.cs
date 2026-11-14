namespace TVRename;

internal abstract class ActionListExporter(ItemList theActionList) : Exporter
{
    protected readonly ItemList TheActionList = theActionList;

    public abstract bool ApplicableFor(TVSettings.ScanType st);
}
