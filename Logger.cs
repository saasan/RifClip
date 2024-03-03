public class Logger
{
    private static readonly string path = Path.ChangeExtension(Application.ExecutablePath, ".log");

    public static void Info(in string message)
    {
        string content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " INFO: " + message;
        System.Diagnostics.Debug.Print(content);
        File.AppendAllText(path, content + Environment.NewLine);
    }
}
