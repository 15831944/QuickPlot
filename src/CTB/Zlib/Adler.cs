﻿// Decompiled with JetBrains decompiler
// Type: Ionic.Zlib.Adler
// Assembly: PlotStyleViewer2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B42B16-55A3-4EDC-A4BC-2618C307E234
// Assembly location: C:\Setups\PlotStyleViewer2.exe

using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
  [ComVisible(true)]
  public sealed class Adler
  {
    private static readonly uint BASE = 65521;
    private static readonly int NMAX = 5552;

    public static uint Adler32(uint adler, byte[] buf, int index, int len)
    {
      if (buf == null)
        return 1;
      uint num1 = adler & (uint) ushort.MaxValue;
      uint num2 = adler >> 16 & (uint) ushort.MaxValue;
      while (len > 0)
      {
        int num3 = len < Adler.NMAX ? len : Adler.NMAX;
        len -= num3;
        while (num3 >= 16)
        {
          uint num4 = num1 + (uint) buf[index++];
          uint num5 = num2 + num4;
          uint num6 = num4 + (uint) buf[index++];
          uint num7 = num5 + num6;
          uint num8 = num6 + (uint) buf[index++];
          uint num9 = num7 + num8;
          uint num10 = num8 + (uint) buf[index++];
          uint num11 = num9 + num10;
          uint num12 = num10 + (uint) buf[index++];
          uint num13 = num11 + num12;
          uint num14 = num12 + (uint) buf[index++];
          uint num15 = num13 + num14;
          uint num16 = num14 + (uint) buf[index++];
          uint num17 = num15 + num16;
          uint num18 = num16 + (uint) buf[index++];
          uint num19 = num17 + num18;
          uint num20 = num18 + (uint) buf[index++];
          uint num21 = num19 + num20;
          uint num22 = num20 + (uint) buf[index++];
          uint num23 = num21 + num22;
          uint num24 = num22 + (uint) buf[index++];
          uint num25 = num23 + num24;
          uint num26 = num24 + (uint) buf[index++];
          uint num27 = num25 + num26;
          uint num28 = num26 + (uint) buf[index++];
          uint num29 = num27 + num28;
          uint num30 = num28 + (uint) buf[index++];
          uint num31 = num29 + num30;
          uint num32 = num30 + (uint) buf[index++];
          uint num33 = num31 + num32;
          num1 = num32 + (uint) buf[index++];
          num2 = num33 + num1;
          num3 -= 16;
        }
        if (num3 != 0)
        {
          do
          {
            num1 += (uint) buf[index++];
            num2 += num1;
          }
          while (--num3 != 0);
        }
        num1 %= Adler.BASE;
        num2 %= Adler.BASE;
      }
      return num2 << 16 | num1;
    }
  }
}