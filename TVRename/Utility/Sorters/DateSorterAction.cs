using BrightIdeasSoftware;

namespace TVRename
{
    public class DateSorterAction : ObjectListViewComparer<DateTime>
    {
        public DateSorterAction(int column) : base(column)
        {
        }

        protected override DateTime GetValue(OLVListItem x, int columnId)
        {
            try
            {
                if (x.RowObject is Item a)
                {
                    return a.AirDate ?? DateTime.Now;
                }

                return DateTime.Now;
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}
