namespace RifClip;

static class Program
{
    // 削除するクリップボードのフォーマット
    private static readonly string[] RemoveClipboardFormats = {
        "Bitmap",
        "DeviceIndependentBitmap",
        "System.Drawing.Bitmap"
    };
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
                    if (!RemoveClipboardFormats.Contains(format)) {
                        newData.SetData(format, data.GetData(format));
                    }
                }
            }
            Clipboard.SetDataObject(newData);

            trayIconManager.ShowBalloonTip(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                "画像を削除しました！");
        };

        Application.Run();
    }
}
