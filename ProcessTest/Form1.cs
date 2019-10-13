using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace ProcessTest
{
  public partial class Form1 : Form
  {
    private const string MaxCountReg = "^MaxCount:(.+?)$";
    private const string NowCountReg = "^NowCount:(.+?)$";

    /// <summary>
    /// 実行プロセス用最大カウント数
    /// </summary>
    private decimal maxCount = 0;

    /// <summary>
    /// 実行プロセス用現在カウント数
    /// </summary>
    private decimal nowCount = 0;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Form1()
    {
      InitializeComponent();

      // 結果ラベルをクリア
      Result.Text = string.Empty;
    }

    /// <summary>
    /// バッチ実行ボタンクリック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
      // 初期化
      maxCount = -1;
      nowCount = -1;

      // ボタンを無効化
      button1.Enabled = false;

      // Batchを非同期で実行
      Task.Run(() => { ProcessClass.Start("Batch.exe", "", OutputDataReceive); });
    }

    /// <summary>
    /// 「プロセス実行時のコンソール出力」コールバックメソッド
    /// </summary>
    /// <param name="data">出力文字列</param>
    public void OutputDataReceive(string data)
    {
      var callMainThread = false;
      // 最大カウント数取得
      if (!callMainThread && GetInt(data, MaxCountReg, ref maxCount))
      {
        nowCount = 0;
        callMainThread = true;
      }

      // 現在カウント数取得
      if (!callMainThread && GetInt(data, NowCountReg, ref nowCount))
      {
        callMainThread = true;
      }

      if (callMainThread && maxCount > 0 && nowCount >= 0)
      {
        var msg = $"{nowCount}/{maxCount} [{(int)((nowCount/ maxCount)*100)}%]";

        // メインメソッドに出力文字列を渡す
        Invoke(new ProcessClass.CallbackOutput(OutputData), msg);
      }


      // 出力文字列から対象の値を取得
      bool GetInt(string srcData,string regString,ref decimal result)
      {
        var matches = Regex.Matches(srcData, regString);
        if (matches.Count < 1)
        {
          return false;
        }

        var matcheGroup = matches[0].Groups;
        if (matcheGroup.Count < 2)
        {
          return false;
        }

        if (decimal.TryParse(matcheGroup[1].Value,out result))
        {
          return true;
        }

        return false;
      }
    }

    /// <summary>
    /// コンソール出力取得：メインスレッド
    /// </summary>
    /// <param name="data">出力文字列</param>
    private void OutputData(string data)
    {
      // 結果ラベルに設定
      Result.Text = $"[{DateTime.Now.ToLongTimeString()}]callback:\"{data}\"";
    }
  }
}
