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
      // 最大カウント数
      var maxCount = 10;
      Console.WriteLine($"MaxCount:{maxCount}");

      // 実測値
      for (int i = 0; i <= maxCount; i++)
      {
        // 現在カウント数
        Console.WriteLine($"NowCount:{i}");

        // ダミーメッセージ
        Console.WriteLine($"Dummy:{i}");

        // 0.5秒待つ
        System.Threading.Thread.Sleep(500);

        // ダミーメッセージ
        Console.WriteLine($"AAAAAAAA");
      }
    }
  }
}
