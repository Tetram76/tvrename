// Sorting IComparer classes used by the ListViews in UI.cs

namespace TVRename;

public sealed class TextSorter(int column) : ListViewItemSorter(column)
{
    protected override int CompareListViewItem(ListViewItem x, ListViewItem y) =>
        string.Compare(x.SubItems[Col].Text, y.SubItems[Col].Text, StringComparison.OrdinalIgnoreCase);
}
