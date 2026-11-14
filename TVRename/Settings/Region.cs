namespace TVRename;

public class Region(int id, string abbreviation, string threeAbbreviation, string localName, string? englishName)
{
    public int Id { get; set; } = id;
    public string Abbreviation { get; set; } = abbreviation;
    public string ThreeAbbreviation { get; set; } = threeAbbreviation;
    public string LocalName { get; set; } = localName;
    public string? EnglishName { get; set; } = englishName;
}
