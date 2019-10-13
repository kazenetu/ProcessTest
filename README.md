# ProcessTest
C#のProcessテスト

## 仕様
* 0.5秒間隔で10カウントする「Batch.exe」を別スレッドで実行するサンプルです。
* 実行対象(Batch.exe)が標準出力するごとに指定したコールバックメソッドを呼び出します。

## ビルド環境
* Windows10
* Visual Studio 2017以降
* .NET Framework 4.6.1以降

## フォルダ構成
```
./
│
├─Batch
│   Batch.csproj
│   Program.cs
│
└─ProcessTest
    Form1.cs
    Form1.Designer.cs
    Form1.resx
    ProcessClass.cs
    ProcessTest.csproj
    Program.cs
```
* Batchプロジェクト：実行対象(コンソールプログラム)
   * Program.cs：  
     0.5秒間隔で10カウントを実行  
     カウントごとに標準出力にその旨を出力

* ProcessTestプロジェクト：実行対象実行(フォームプログラム)
   * Form1：  
     ProcessClass経由で実行対象(Batch.exe)を別スレッドで実行  
     標準出力をラベルに表示
   * ProcessClass.cs：  
     実行対象(Batch.exe)を別スレッドで実行  
     標準出力ごとにコールバックメソッドを呼び出し
