namespace EventSlice.Interfaces
{
    public interface i_Module
    {
        /// <summary>
        /// This should return a short, human-readable name for the module.  Note:  Not guaranteed to uniquely identify a
        /// module in a running system.
        /// </summary>
        string getModuleName();
        /// <summary>
        /// This is the handler each module should implement for events sent from the event dispatcher.
        /// </summary>
        /// <param name="realTimeEvent">The real time event to be handled.</param>
        void onRealTimeEvent(Interfaces.RealTimeEvent realTimeEvent);
    }
}
