namespace TVRename;

/// <summary>
/// Summary for CustomNameTagsFloatingWindow
///
/// WARNING: If you change the name of this class, you will need to change the
///          'Resource File Name' property for the managed resource compiler tool
///          associated with all .resx files this class depends on.  Otherwise,
///          the designers will not be able to interact properly with localized
///          resources associated with this form.
/// </summary>
public partial class CustomNameTagsFloatingWindow : Form
{
    public CustomNameTagsFloatingWindow(ProcessedEpisode? pe)
    {
        InitializeComponent(CustomEpisodeName.TAGS, pe != null, s => CustomEpisodeName.NameForNoExt(pe!, s, TargetType.Filename));
    }

    public CustomNameTagsFloatingWindow(ProcessedSeason? pe)
    {
        InitializeComponent(CustomSeasonName.TAGS, pe != null, s => CustomSeasonName.NameFor(pe!, s, TargetType.Filename));
    }

    public CustomNameTagsFloatingWindow(MovieConfiguration? movie)
    {
        InitializeComponent(CustomMovieName.TAGS, movie != null, s => CustomMovieName.NameFor(movie, s, TargetType.Filename));
    }

    public CustomNameTagsFloatingWindow(ShowConfiguration? tvShow)
    {
        InitializeComponent(CustomTvShowName.TAGS, tvShow != null, s => CustomTvShowName.NameFor(tvShow, s, TargetType.Filename));
    }

    private void InitializeComponent(List<string> tags, bool showExamples, Func<string, string> createExampleFromTagFunc)
    {
        InitializeComponent();

        foreach (string s in tags)
        {
            if (showExamples)
            {
                label1.Text += $"{s} - {createExampleFromTagFunc(s)}\r\n";
            }
            else
            {
                label1.Text += $"{s}\r\n";
            }
        }
    }
}
