using DarkMode.Core.Interfaces.Task;

namespace DarkMode.Core.Services.Task;

public class ChangeWallpaperTask : TaskBase
{
    public override string Name => "ChangeWallpaper";
    protected override void ExecuteTask()
    {
        // TODO: 实现切换壁纸逻辑
        Console.WriteLine("正在切换壁纸...");
    }
}