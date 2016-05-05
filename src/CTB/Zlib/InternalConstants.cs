// Decompiled with JetBrains decompiler
// Type: Ionic.Zlib.InternalConstants
// Assembly: PlotStyleViewer2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B42B16-55A3-4EDC-A4BC-2618C307E234
// Assembly location: C:\Setups\PlotStyleViewer2.exe

namespace Ionic.Zlib
{
  internal static class InternalConstants
  {
    internal static readonly int MAX_BITS = 15;
    internal static readonly int BL_CODES = 19;
    internal static readonly int D_CODES = 30;
    internal static readonly int LITERALS = 256;
    internal static readonly int LENGTH_CODES = 29;
    internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;
    internal static readonly int MAX_BL_BITS = 7;
    internal static readonly int REP_3_6 = 16;
    internal static readonly int REPZ_3_10 = 17;
    internal static readonly int REPZ_11_138 = 18;
  }
}
