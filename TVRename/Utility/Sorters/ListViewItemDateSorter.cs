namespace TVRename;

public abstract class ListViewItemDateSorter(int column) : ListViewItemSorter(column)
{
    protected override int CompareListViewItem(ListViewItem x, ListViewItem y)
    {
        DateTime? d1 = GetDate(x);
        DateTime? d2 = GetDate(y);

        if (d1 is null && d2 is null)
        {
            return 0;
        }

        if (d1 is null)
        {
            return -1;
        }

        if (d2 is null)
        {
            return 1;
        }

        return d1.Value.CompareTo(d2.Value);
    }

    protected abstract DateTime? GetDate(ListViewItem lvi);
}
