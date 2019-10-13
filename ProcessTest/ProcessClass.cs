using System;
using System.Diagnostics;

namespace ProcessTest
{
  /// <summary>
  /// Processクラス
  /// </summary>
  /// <remarks>Batchを実行</remarks>
  public static class ProcessClass
  {
    /// <summary>
    /// 実行プロセスコンソール出力のコールバック
    /// </summary>
    /// <param name="data">出力文字列</param>
    public delegate void CallbackOutput(string data);

    /// <summary>
    /// プロセス実行
    /// </summary>
    /// <param name="filePath">ファイル名</param>
    /// <param name="args">引数</param>
    /// <param name="outputDataReceived"></param>
    public static void Start(string filePath,string args, CallbackOutput outputDataReceived)
    {
      using (var process = new Process())
      {
        // ファイルとパラメータを追加
        process.StartInfo.FileName = filePath;
        process.StartInfo.Arguments = args;

        // 実行環境を設定
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;

        // 実行プロセスコンソール出力のイベントを追加
        process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
        {
          // 値なしの場合は終了
          if (string.IsNullOrEmpty(e.Data))
          {
            return;
          }

          // 出力内容をコールバックメソッドに渡す
          outputDataReceived(e.Data);
        });

        // プロセス実行
        process.Start();

        // 実行プロセスコンソール出力のイベントを開始
        process.BeginOutputReadLine();

        // プロセス終了まで待つ
        process.WaitForExit();
      }
    }

  }
}
