using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCopy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //定義されたキーが押された場合の処理
            if (e.KeyCode == Keys.V && e.Control == true)
            {
                //ctrl+vを押した場合は、クリップボードをlistBoxに設定
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                foreach (string ln in lines)
                {
                    listBox1.Items.Add(ln.Trim());
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                //deleteが押された場合は、指定された範囲を削除
                while (listBox1.SelectedIndex > -1)
                {
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
            }
            else if (e.KeyCode == Keys.A && e.Control == true)
            {
                //ctrl+aが押された場合は、全てのリストを選択
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    listBox1.SetSelected(i, true);
                }
            }
            else { 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "フォルダを指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            fbd.SelectedPath = @"C:";
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                textBox1.Text=fbd.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "フォルダを指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            fbd.SelectedPath = @"C:";
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                textBox2.Text = fbd.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //コピー対象のファイル名を取得
            string[] targets = this.listBox1.Items.Cast<string>().ToArray();

            //コピー元に存在するファイルをすべて取得
            IEnumerable<string> ExistFiles = System.IO.Directory.EnumerateFiles(textBox1.Text, "*", System.IO.SearchOption.AllDirectories);

            //コピー対象のファイルに対し逐次処理
            foreach (string target in targets)
            {
                //存在するファイルに対し逐次処理
                foreach (string ExistFile in ExistFiles)
                {
                    //ファイルが一致
                    string ExistFileName = System.IO.Path.GetFileName(ExistFile);
                    if (target == ExistFileName)
                    {
                        //コピー先のフォルダにファイルをコピー
                        System.IO.File.Copy(ExistFile, textBox2.Text +"\\"+ ExistFileName,true);
                    } 
                }
            }
        }
    }
}
