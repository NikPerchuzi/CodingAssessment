namespace CoddingAssesment.Application.Commands
{
    public class LateEventException : Exception
    {
        public TimeSpan Interval { get; }

        public LateEventException(TimeSpan interval)
        {
            Interval = interval;
        }
    }
}
