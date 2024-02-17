namespace RifClip;

public class TrayIconManager
{
    private readonly NotifyIcon notifyIcon = new();
    private Settings settings;

    public TrayIconManager(Settings settings)
    {
        this.settings = settings;
        ContextMenuStrip contextMenu = new();

        ToolStripMenuItem menuNotifyOnRemoveImage = new("画像を削除時に通知する(&N)");
        menuNotifyOnRemoveImage.Click += MenuNotifyOnRemoveImage_Click;
        menuNotifyOnRemoveImage.CheckState = settings.NotifyOnRemoveImage ? CheckState.Checked : CheckState.Unchecked;
        contextMenu.Items.Add(menuNotifyOnRemoveImage);

        contextMenu.Items.Add("-");

        contextMenu.Items.Add("終了(&X)", null, (sender, e) =>
        {
            notifyIcon.Visible = false;
            Application.Exit();
        });

        notifyIcon.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        notifyIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        notifyIcon.ContextMenuStrip = contextMenu;
        notifyIcon.Visible = true;
    }

    public void ShowBalloonTip(string? tipTitle, string? tipText, ToolTipIcon tipIcon = ToolTipIcon.Info)
    {
        tipTitle = tipTitle ?? "";
        tipText = tipText ?? "";
        notifyIcon.ShowBalloonTip(1, tipTitle, tipText, tipIcon);
    }

    private void MenuNotifyOnRemoveImage_Click(object? sender, EventArgs e)
    {
        settings.NotifyOnRemoveImage = !settings.NotifyOnRemoveImage;

        if (sender is null) return;
        ToolStripMenuItem menu = (ToolStripMenuItem)sender;
        menu.CheckState = settings.NotifyOnRemoveImage ? CheckState.Checked : CheckState.Unchecked;
    }
}
