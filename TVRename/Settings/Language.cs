namespace TVRename;

public class Language(int tvdbid, string abbreviation, string threeAbbreviation, string englishName, string localName, string isoDialectAbbreviation, bool isPrimary)
{
    public int TvdbId { get; set; } = tvdbid;
    public string Abbreviation { get; set; } = abbreviation;
    public string ThreeAbbreviation { get; set; } = threeAbbreviation;

    // ReSharper disable once InconsistentNaming
    public string ISODialectAbbreviation { get; set; } = isoDialectAbbreviation;

    public bool IsPrimary { get; set; } = isPrimary;
    public string LocalName { get; set; } = localName;
    public string EnglishName { get; set; } = englishName;
    public override string ToString() => $"{LocalName} ({EnglishName} - '{ISODialectAbbreviation}')";
}
