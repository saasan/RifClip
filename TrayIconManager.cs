namespace RifClip;

public class TrayIconManager
{
    private readonly NotifyIcon notifyIcon = new();

    public void Initialize()
    {
        ContextMenuStrip contextMenu = new();

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

    public void ShowBalloonTip(string tipTitle, string tipText, ToolTipIcon tipIcon = ToolTipIcon.Info) {
        notifyIcon.ShowBalloonTip(1, tipTitle, tipText, tipIcon);
    }
}
