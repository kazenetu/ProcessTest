using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessTest
{
  public partial class Form1 : Form
  {
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
      // Batchを非同期で実行
      Task.Run(() => { ProcessClass.Start("Batch.exe", "", OutputDataReceive); });
    }

    /// <summary>
    /// 「プロセス実行時のコンソール出力」コールバックメソッド
    /// </summary>
    /// <param name="data">出力文字列</param>
    public void OutputDataReceive(string data)
    {
      // メインメソッドに出力文字列を渡す
      Invoke(new ProcessClass.CallbackOutput(OutputData), data);
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
