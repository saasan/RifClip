namespace RifClip;

public class TrayIconManager
{
    public void Initialize()
    {
        NotifyIcon notifyIcon = new();
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
}
