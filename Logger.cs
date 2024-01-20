public class Logger
{
    private static readonly string path = Path.ChangeExtension(Application.ExecutablePath, ".log");

    public static void Info(in string message)
    {
        string[] contents = { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff INFO: ") + message };
        File.AppendAllLines(path, contents);
    }
}
