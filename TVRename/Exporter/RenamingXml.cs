namespace TVRename;

internal class RenamingXml(ItemList theActionList) : ActionListXml(theActionList)
{
    protected override bool IsOutput(Item a) => a is ActionCopyMoveRename { Operation: ActionCopyMoveRename.Op.rename, Episode: { } };

    public override bool ApplicableFor(TVSettings.ScanType st) => true;

    public override bool Active() => TVSettings.Instance.ExportRenamingXML;

    protected override string Location() => TVSettings.Instance.ExportRenamingXMLTo;

    protected override string MainXmlElementName() => "Renaming";
    protected override string Name() => "Renaming XML Exporter";
}
