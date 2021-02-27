using System;


namespace Expelibrum.UI.Events
{
    public class TagCountChangedEventArgs : EventArgs
    {
        public int Count;
    }

    public class TagRemoveRequestedEventArgs : EventArgs
    {
        public int Id;
    }
}
