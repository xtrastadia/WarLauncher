// Decompiled with JetBrains decompiler
// Type: WarfaceLauncher.DllInjector
// Assembly: WarfaceLauncher, Version=0.0.2.1, Culture=neutral, PublicKeyToken=null
// MVID: 922C43AD-4664-4436-B548-6B4EFEC3CF52
// Assembly location: C:\Games\Client\n1kodim1.exe

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace WarfaceLauncher
{
  public sealed class DllInjector
  {
    private static readonly IntPtr INTPTR_ZERO = (IntPtr) 0;
    private static DllInjector _instance;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(
      uint dwDesiredAccess,
      int bInheritHandle,
      uint dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern int CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr VirtualAllocEx(
      IntPtr hProcess,
      IntPtr lpAddress,
      IntPtr dwSize,
      uint flAllocationType,
      uint flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern int WriteProcessMemory(
      IntPtr hProcess,
      IntPtr lpBaseAddress,
      byte[] buffer,
      uint size,
      int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateRemoteThread(
      IntPtr hProcess,
      IntPtr lpThreadAttribute,
      IntPtr dwStackSize,
      IntPtr lpStartAddress,
      IntPtr lpParameter,
      uint dwCreationFlags,
      IntPtr lpThreadId);

    public static DllInjector GetInstance
    {
      get
      {
        if (DllInjector._instance == null)
          DllInjector._instance = new DllInjector();
        return DllInjector._instance;
      }
    }

    public static DllInjectionResult Inject(uint _procId, string sDllPath) => File.Exists(sDllPath) ? (_procId != 0U ? (DllInjector.bInject(_procId, sDllPath) ? DllInjectionResult.Success : DllInjectionResult.InjectionFailed) : DllInjectionResult.GameProcessNotFound) : DllInjectionResult.DllNotFound;

    private static unsafe bool bInject(uint pToBeInjected, string sDllPath)
    {
      IntPtr num1 = DllInjector.OpenProcess(1082U, 1, pToBeInjected);
      bool flag;
      if (num1 == DllInjector.INTPTR_ZERO)
      {
        flag = false;
      }
      else
      {
        IntPtr procAddress = DllInjector.GetProcAddress(DllInjector.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
        if (procAddress == DllInjector.INTPTR_ZERO)
        {
          flag = false;
        }
        else
        {
          IntPtr num2 = DllInjector.VirtualAllocEx(num1, (IntPtr) (void*) null, (IntPtr) sDllPath.Length, 12288U, 64U);
          if (num2 == DllInjector.INTPTR_ZERO)
          {
            flag = false;
          }
          else
          {
            byte[] bytes = Encoding.ASCII.GetBytes(sDllPath);
            if (DllInjector.WriteProcessMemory(num1, num2, bytes, (uint) bytes.Length, 0) == 0)
              flag = false;
            else if (DllInjector.CreateRemoteThread(num1, (IntPtr) (void*) null, DllInjector.INTPTR_ZERO, procAddress, num2, 0U, (IntPtr) (void*) null) == DllInjector.INTPTR_ZERO)
            {
              flag = false;
            }
            else
            {
              DllInjector.CloseHandle(num1);
              flag = true;
            }
          }
        }
      }
      return flag;
    }
  }
}
