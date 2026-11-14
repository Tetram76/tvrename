namespace TVRename;

internal class CopyMoveXml(ItemList theActionList) : ActionListXml(theActionList)
{
    public override bool ApplicableFor(TVSettings.ScanType st) => true;

    protected override bool IsOutput(Item a) => a is ActionCopyMoveRename cmr && cmr.Operation != ActionCopyMoveRename.Op.rename && cmr.Episode != null;

    public override bool Active() => TVSettings.Instance.ExportFOXML;

    protected override string Location() => TVSettings.Instance.ExportFOXMLTo;

    protected override string MainXmlElementName() => "FindingAndOrganising";

    protected override string Name() => "CopyMove XML Exporter";
}
