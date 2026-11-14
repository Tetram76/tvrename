using BrightIdeasSoftware;

namespace TVRename;

public class DateActionSorter(int column) : ListViewItemDateSorter(column)
{
    protected override DateTime? GetDate(ListViewItem lvi)
    {
        try
        {
            if (lvi is OLVListItem olvi)
            {
                return ((Item)olvi.RowObject).AirDate;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
}
