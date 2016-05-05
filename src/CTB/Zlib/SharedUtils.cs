// Decompiled with JetBrains decompiler
// Type: Ionic.Zlib.SharedUtils
// Assembly: PlotStyleViewer2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B42B16-55A3-4EDC-A4BC-2618C307E234
// Assembly location: C:\Setups\PlotStyleViewer2.exe

using System.IO;
using System.Text;

namespace Ionic.Zlib
{
  internal class SharedUtils
  {
    public static int URShift(int number, int bits)
    {
      return (int) ((uint) number >> bits);
    }

    public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
    {
      if (target.Length == 0)
        return 0;
      char[] buffer = new char[target.Length];
      int num = sourceTextReader.Read(buffer, start, count);
      if (num == 0)
        return -1;
      for (int index = start; index < start + num; ++index)
        target[index] = (byte) buffer[index];
      return num;
    }

    internal static byte[] ToByteArray(string sourceString)
    {
      return Encoding.UTF8.GetBytes(sourceString);
    }

    internal static char[] ToCharArray(byte[] byteArray)
    {
      return Encoding.UTF8.GetChars(byteArray);
    }
  }
}
