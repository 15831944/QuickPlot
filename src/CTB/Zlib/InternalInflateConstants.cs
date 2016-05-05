// Decompiled with JetBrains decompiler
// Type: Ionic.Zlib.InternalInflateConstants
// Assembly: PlotStyleViewer2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B42B16-55A3-4EDC-A4BC-2618C307E234
// Assembly location: C:\Setups\PlotStyleViewer2.exe

namespace Ionic.Zlib
{
  internal static class InternalInflateConstants
  {
    internal static readonly int[] InflateMask = new int[17]
    {
      0,
      1,
      3,
      7,
      15,
      31,
      63,
      (int) sbyte.MaxValue,
      (int) byte.MaxValue,
      511,
      1023,
      2047,
      4095,
      8191,
      16383,
      (int) short.MaxValue,
      (int) ushort.MaxValue
    };
  }
}
