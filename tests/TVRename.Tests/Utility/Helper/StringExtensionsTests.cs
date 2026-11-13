namespace TVRename.Tests.Utility.Helper;

/// <summary>
/// Pins the normalisation of the names that the application compares with each other (show names, names found in
/// file names and search results), and the removal of a year from such a name.
/// </summary>
public class StringExtensionsTests
{
    [Theory]
    [InlineData("The Office", "the office")]
    [InlineData("  Doctor Who  ", "doctor who")]
    [InlineData("Marvel's Agents of S.H.I.E.L.D.", "marvels agents of s h i e l d")]
    [InlineData("Law & Order: Special Victims Unit", "law and order special victims unit")]
    [InlineData("Pokémon", "pokemon")]
    [InlineData("Who Wants to Be a Millionaire?!", "who wants to be a millionaire")]
    [InlineData("The.Big.Bang.Theory", "the big bang theory")]
    [InlineData("Star_Trek - Discovery", "star trek discovery")]
    public void CompareName_NormalisesTheName(string name, string expected)
        => Assert.Equal(expected, name.CompareName());

    [Theory]
    [InlineData("Doctor Who (2005)", "Doctor Who")]
    [InlineData("Doctor Who [2005]", "Doctor Who")]
    [InlineData("Doctor Who", "Doctor Who")]
    [InlineData("1923", "1923")]
    public void RemoveBracketedYear_RemovesAYearInBrackets(string name, string expected)
        => Assert.Equal(expected, name.RemoveBracketedYear());

    [Theory]
    [InlineData("doctor who 2005", "doctor who")]
    [InlineData("1923", "1923")]
    public void RemoveYearFromEnd_RemovesATrailingYear(string name, string expected)
        => Assert.Equal(expected, name.RemoveYearFromEnd());

    [Theory]
    [InlineData(1, "01")]
    [InlineData(10, "10")]
    [InlineData(100, "100")]
    public void Pad_PadsToTwoDigits(int number, string expected)
        => Assert.Equal(expected, number.Pad());
}
