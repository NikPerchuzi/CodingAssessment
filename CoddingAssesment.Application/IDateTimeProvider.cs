namespace CoddingAssesment.Application
{
    internal interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}