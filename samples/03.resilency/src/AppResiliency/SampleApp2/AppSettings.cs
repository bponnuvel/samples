namespace SampleApp2
{
    public class AppSettings
    {
        public string AndroidApiBaseUri { get; set; }
        public Logging Logging { get; set; }
    }

    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Debug Debug { get; set; }
        public Console Console { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }

    public class Debug
    {
        public LogLevel LogLevel { get; set; }
    }

    public class Console
    {
        public LogLevel LogLevel { get; set; }
    }


}