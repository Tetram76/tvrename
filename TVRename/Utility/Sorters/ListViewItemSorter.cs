namespace TVRename;

public abstract class ListViewItemSorter(int column) : IComparer
{
    protected readonly int Col = column;

    /// <exception cref="InvalidOperationException">Condition.</exception>
    public int Compare(object? x, object? y)
    {
        if (x is not ListViewItem lvix)
        {
            throw new InvalidOperationException();
        }

        if (y is not ListViewItem lviy)
        {
            throw new InvalidOperationException();
        }

        return CompareListViewItem(lvix, lviy);
    }

    protected abstract int CompareListViewItem(ListViewItem x, ListViewItem y);
}
