namespace TVRename;

public class DateSorterWtw(int column) : ListViewItemDateSorter(column)
{
    protected override DateTime? GetDate(ListViewItem lvi)
    {
        try
        {
            return ((ProcessedEpisode?)lvi.Tag)?.GetAirDateDt();
        }
        catch
        {
            return null;
        }
    }
}
