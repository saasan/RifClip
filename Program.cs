using NLog;

namespace RifClip;

static class Program
{
    // 再コピーするクリップボードのフォーマット
    private static readonly string[] CopyClipboardFormats = {
        "Star Embed Source (XML)",
        "Star Object Descriptor (XML)",
        "HTML (HyperText Markup Language)",
        "HTML Format",
        "SymbolicLink",
        "Link",
        "DataInterchangeFormat",
        "System.String",
        "UnicodeText",
        "Text",
        "Rich Text Format",
        "Richtext Format"
    };
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private const string MutexName = "RifClipMutex";

    [STAThread]
    static void Main()
    {
        using Mutex singleInstanceMutex = new(true, MutexName, out bool createdNew);
        if (!createdNew)
        {
            // 既に起動中の場合、アプリケーションを終了
            return;
        }

        LogManager.Setup().LoadConfiguration(builder => {
            builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToConsole();
            builder.ForLogger().FilterMinLevel(LogLevel.Error).WriteToFile(fileName: "log.txt");
        });

        ApplicationConfiguration.Initialize();

        TrayIconManager trayIconManager = new();
        trayIconManager.Initialize();

        using ClipboardListener clipboardListener = new();
        clipboardListener.ClipboardUpdate += (sender, e) =>
        {
            if (!Clipboard.ContainsImage() || !Clipboard.ContainsText()) return;

            // 画像のみを削除することはできないため新しいDataObjectへコピーする
            DataObject newData = new();
            IDataObject data = Clipboard.GetDataObject();
            if (data != null)
            {
                foreach (string format in data.GetFormats())
                {
                    Logger.Info(format);
                    if (CopyClipboardFormats.Contains(format)) {
                        newData.SetData(format, data.GetData(format));
                    }
                }
            }
            Clipboard.SetDataObject(newData);
        };

        Application.Run();
    }
}
