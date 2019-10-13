using System;

/// <summary>
/// バッチプログラム
/// </summary>
/// <remarks>内部実行用</remarks>
namespace Batch
{
  class Program
  {
    static void Main(string[] args)
    {
      // 最大値
      var maxCount = 10;
      Console.WriteLine($"MaxCount:{maxCount}");

      // 実測値
      for (int i = 0; i <= maxCount; i++)
      {
        Console.WriteLine($"NowCount:{i}");

        // 0.5秒待つ
        System.Threading.Thread.Sleep(500);
      }
    }
  }
}
