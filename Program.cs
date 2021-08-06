using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using WarfaceLauncher.Properties;

namespace WarfaceLauncher
{
  internal class Program
  {
    private static string path = "Bin32\\";

    private static void Main()
    {
      Console.Title = "OneDubLauncher";
      Console.Write("Launcher ");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("OneDub");
      Console.WriteLine();
      Console.ResetColor();
      if (!File.Exists("Bin32\\Game.exe"))
      {
        if (!File.Exists("Bin32Release\\Game.exe"))
        {
          Console.ForegroundColor = ConsoleColor.DarkRed;
          Console.WriteLine("Could not find Game.exe");
          Thread.Sleep(4000);
          return;
        }
        Program.path = "Bin32Release\\";
      }
      if (Program.isDedicated())
        Program.StartDedicated();
      else
        Program.StartGame();
      Console.WriteLine("Starting......");
      Thread.Sleep(1000);
    }

    private static bool isDedicated()
    {
      bool flag = false;
      Console.ResetColor();
      Console.WriteLine("Is Dedicated?: y/n");
      switch (Console.ReadLine())
      {
        case "y":
          flag = true;
          break;
        case "n":
          flag = false;
          break;
      }
      return flag;
    }

    private static string username()
    {
      if (Settings.Default["nickname"].ToString() != "")
      {
        Console.Write("Use nick: ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("'" + Settings.Default["nickname"].ToString() + "'");
        Console.ResetColor();
        Console.WriteLine(" ? y/n");
        switch (Console.ReadLine())
        {
          case "y":
            return Settings.Default["nickname"].ToString();
          case "n":
            Console.Write("Nickname: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Settings.Default["nickname"] = (object) Console.ReadLine();
            Settings.Default.Save();
            Console.ResetColor();
            return Settings.Default["nickname"].ToString();
        }
      }
      else
      {
        Console.Write("Nickname: ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Settings.Default["nickname"] = (object) Console.ReadLine();
        Settings.Default.Save();
        Console.ResetColor();
      }
      return Settings.Default["nickname"].ToString();
    }

    private static string password()
    {
      if (Settings.Default[nameof (password)].ToString() != "")
      {
        Console.Write("Use Password: ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("'" + Settings.Default[nameof (password)].ToString() + "'");
        Console.ResetColor();
        Console.WriteLine(" ? y/n");
        switch (Console.ReadLine())
        {
          case "y":
            return Settings.Default[nameof (password)].ToString();
          case "n":
            Console.Write("Password: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Settings.Default[nameof (password)] = (object) Console.ReadLine();
            Settings.Default.Save();
            Console.ResetColor();
            return Settings.Default[nameof (password)].ToString();
        }
      }
      else
      {
        Console.Write("Password: ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Settings.Default[nameof (password)] = (object) Console.ReadLine();
        Settings.Default.Save();
        Console.ResetColor();
      }
      return Settings.Default[nameof (password)].ToString();
    }

    private static string ip()
    {
      if (Settings.Default[nameof (ip)].ToString() != "")
      {
        Console.Write("Use IP: ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("'" + Settings.Default[nameof (ip)].ToString() + "'");
        Console.ResetColor();
        Console.WriteLine(" ? y/n");
        switch (Console.ReadLine())
        {
          case "y":
            return Settings.Default[nameof (ip)].ToString();
          case "n":
            Console.Write("IP: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Settings.Default[nameof (ip)] = (object) Console.ReadLine();
            Settings.Default.Save();
            Console.ResetColor();
            return Settings.Default[nameof (ip)].ToString();
        }
      }
      else
      {
        Console.Write("IP: ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Settings.Default[nameof (ip)] = (object) Console.ReadLine();
        Settings.Default.Save();
        Console.ResetColor();
      }
      return Settings.Default[nameof (ip)].ToString();
    }

    private static void StartOldDedic()
    {
      Process process = new Process();
      process.StartInfo.FileName = Program.path + "Game.exe";
      process.StartInfo.Arguments = "-devmode -uid ded -token ded +online_server=127.0.0.1 -dedicated +sv_port=63000 +online_server_port 5222 +online_check_certificate 0 +online_dedicated_id dedicated +online_verbose 1 +online_reconnect_timeout 1 +online_masterserver_resource=cicada +online_use_protect 1";
      process.Start();
      Thread.Sleep(2000);
      int num = (int) DllInjector.Inject((uint) process.Id, Program.path + "ServerHelper.dll");
    }

    private static void StartOldGame() => new Process()
    {
      StartInfo = {
        FileName = (Program.path + "Game.exe"),
        Arguments = ("-devmode +online_check_certificate 0 +online_use_protect 1 -bootstrap row_emul +online_server " + Program.ip())
      }
    }.Start();

    private static void StartGame()
    {
      Process process = new Process()
      {
        StartInfo = {
          FileName = Program.path + "Game.exe",
          Arguments = "-devmode -new +online_check_certificate 0 +online_use_protect 1 -uid " + Program.username() + " -token " + Program.password() + " +online_server " + Program.ip()
        }
      };
      process.Start();
      process.Suspend();
      int num = (int) DllInjector.Inject((uint) process.Id, Program.path + "aclazy.dll");
      process.Resume();
    }

    private static void StartDedicated()
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo()
      {
        FileName = Program.path + "Game.exe",
        Arguments = "-devmode -uid ded -token ded +online_server=" + Program.ip() + " -dedicated +sv_port=63000 +online_server_port 5222 +online_check_certificate 0 +online_dedicated_id dedicated +online_verbose 1 +online_reconnect_timeout 1 +online_masterserver_resource=cicada +online_use_protect 1"
      };
      process.Start();
      process.Suspend();
      int num1 = (int) DllInjector.Inject((uint) process.Id, Program.path + "aclazy.dll");
      process.Resume();
      Thread.Sleep(40000);
      int num2 = (int) DllInjector.Inject((uint) process.Id, Program.path + "ServerHelper.dll");
    }
  }
}
