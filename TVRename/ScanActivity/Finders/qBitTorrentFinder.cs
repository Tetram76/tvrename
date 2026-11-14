namespace TVRename;

// ReSharper disable once InconsistentNaming
internal class qBitTorrentFinder(TVDoc doc, TVDoc.ScanSettings settings) : DownloadingProviderFinder(doc, new qBitTorrent(), settings)
{
    public override bool Active() => TVSettings.Instance.CheckqBitTorrent;
}
