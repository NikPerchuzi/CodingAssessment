namespace CodingAssessment.Persistence
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class DatabaseTypeAttribute : Attribute
    {
        public DatabaseTypeAttribute(DatabaseType type)
        {
            Type = type;
        }

        public DatabaseType Type { get; set; }
    }
}
