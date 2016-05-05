// Decompiled with JetBrains decompiler
// Type: Ionic.Zlib.WorkItem
// Assembly: PlotStyleViewer2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B42B16-55A3-4EDC-A4BC-2618C307E234
// Assembly location: C:\Setups\PlotStyleViewer2.exe

namespace Ionic.Zlib
{
  internal class WorkItem
  {
    public byte[] buffer;
    public byte[] compressed;
    public int crc;
    public int index;
    public int ordinal;
    public int inputBytesAvailable;
    public int compressedBytesAvailable;
    public ZlibCodec compressor;

    public WorkItem(int size, CompressionLevel compressLevel, CompressionStrategy strategy, int ix)
    {
      this.buffer = new byte[size];
      this.compressed = new byte[size + (size / 32768 + 1) * 5 * 2];
      this.compressor = new ZlibCodec();
      this.compressor.InitializeDeflate(compressLevel, false);
      this.compressor.OutputBuffer = this.compressed;
      this.compressor.InputBuffer = this.buffer;
      this.index = ix;
    }
  }
}
