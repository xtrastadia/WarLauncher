// Decompiled with JetBrains decompiler
// Type: WarfaceLauncher.ThreadHack
// Assembly: WarfaceLauncher, Version=0.0.2.1, Culture=neutral, PublicKeyToken=null
// MVID: 922C43AD-4664-4436-B548-6B4EFEC3CF52
// Assembly location: C:\Games\Client\n1kodim1.exe

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WarfaceLauncher
{
  public static class ThreadHack
  {
    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenThread(
      ThreadHack.ThreadAccess dwDesiredAccess,
      bool bInheritHandle,
      uint dwThreadId);

    [DllImport("kernel32.dll")]
    private static extern uint SuspendThread(IntPtr hThread);

    [DllImport("kernel32.dll")]
    private static extern int ResumeThread(IntPtr hThread);

    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool CloseHandle(IntPtr handle);

    public static void Suspend(this Process process)
    {
      if (process.ProcessName == string.Empty)
        return;
      foreach (ProcessThread thread in (ReadOnlyCollectionBase) process.Threads)
      {
        IntPtr num1 = ThreadHack.OpenThread(ThreadHack.ThreadAccess.SUSPEND_RESUME, false, (uint) thread.Id);
        if (!(num1 == IntPtr.Zero))
        {
          int num2 = (int) ThreadHack.SuspendThread(num1);
          ThreadHack.CloseHandle(num1);
        }
      }
    }

    public static void Resume(this Process process)
    {
      if (process.ProcessName == string.Empty)
        return;
      foreach (ProcessThread thread in (ReadOnlyCollectionBase) process.Threads)
      {
        IntPtr num = ThreadHack.OpenThread(ThreadHack.ThreadAccess.SUSPEND_RESUME, false, (uint) thread.Id);
        if (!(num == IntPtr.Zero))
        {
          do
            ;
          while (ThreadHack.ResumeThread(num) > 0);
          ThreadHack.CloseHandle(num);
        }
      }
    }

    [Flags]
    public enum ThreadAccess
    {
      TERMINATE = 1,
      SUSPEND_RESUME = 2,
      GET_CONTEXT = 8,
      SET_CONTEXT = 16, // 0x00000010
      SET_INFORMATION = 32, // 0x00000020
      QUERY_INFORMATION = 64, // 0x00000040
      SET_THREAD_TOKEN = 128, // 0x00000080
      IMPERSONATE = 256, // 0x00000100
      DIRECT_IMPERSONATION = 512, // 0x00000200
    }
  }
}
