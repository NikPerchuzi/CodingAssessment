namespace CoddingAssesment.Application.Commands
{
    public class NotLunchTimeException : Exception
    {
        public TimeSpan CurrentTime { get; }

        public NotLunchTimeException(TimeSpan interval)
        {
            CurrentTime = interval;
        }
    }
}
