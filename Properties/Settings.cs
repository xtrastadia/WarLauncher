// Decompiled with JetBrains decompiler
// Type: WarfaceLauncher.Properties.Settings
// Assembly: WarfaceLauncher, Version=0.0.2.1, Culture=neutral, PublicKeyToken=null
// MVID: 922C43AD-4664-4436-B548-6B4EFEC3CF52
// Assembly location: C:\Games\Client\n1kodim1.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WarfaceLauncher.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.1.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default => Settings.defaultInstance;

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string nickname
    {
      get => (string) this[nameof (nickname)];
      set => this[nameof (nickname)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string password
    {
      get => (string) this[nameof (password)];
      set => this[nameof (password)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string ip
    {
      get => (string) this[nameof (ip)];
      set => this[nameof (ip)] = (object) value;
    }
  }
}
