using System.Runtime.InteropServices;

public class ClipboardListener : Form
{
    private const int WM_CLIPBOARDUPDATE = 0x031D;

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AddClipboardFormatListener(IntPtr hwnd);
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

    public event EventHandler? ClipboardUpdate;

    public ClipboardListener()
    {
        AddClipboardFormatListener(Handle);
    }

    protected override void Dispose(bool disposing)
    {
        RemoveClipboardFormatListener(Handle);
        base.Dispose(disposing);
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_CLIPBOARDUPDATE)
        {
            OnClipboardUpdate();
        }

        base.WndProc(ref m);
    }

    private void OnClipboardUpdate()
    {
        ClipboardUpdate?.Invoke(this, EventArgs.Empty);
    }
}
