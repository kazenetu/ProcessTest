using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessTest
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      // 結果Textをクリア
      Result.Text = string.Empty;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Task.Run(() => { ProcessClass.Start("Batch.exe", "", OutputDataReceive); });
    }

    public bool OutputDataReceive(string data)
    {
      Invoke(new ProcessClass.OutputDataReceive(OutputData), data);
      return true;
    }

    private void OutputData(string data)
    {
      Result.Text = data;
    }
  }
}
