using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIReadWriteFile
{
    public class Methods
    {
        public int random()
        {
            Random ran = new Random();
            return ran.Next(0, 99);
        }

        public string ChooseFile()
        {
            var fileChoose = new OpenFileDialog();
            fileChoose.Filter = "(*.DH19PM)|*.DH19PM";
            fileChoose.Title = "Chọn file dữ liệu";
            fileChoose.ShowDialog();
            return fileChoose.FileName;
        }

        public string ChooseFileSave()
        {
            var fileChoose = new OpenFileDialog();
            fileChoose.Filter = "(*.txt)|*.txt";
            fileChoose.Title = "Chọn file dữ liệu";
            fileChoose.ShowDialog();
            return fileChoose.FileName;
        }

        public string SaveFile()
        {
            var SaveFile = new SaveFileDialog();
            SaveFile.Filter = "(*.DH19PM)|*.DH19PM";
            SaveFile.Title = "Ghi dữ liệu";
            if(SaveFile.ShowDialog() == true)
                return SaveFile.FileName;
            return "";
        }

        public void WriteByteValue(TextBox txb, string pathIn)
        {
            string s = txb.Text;
            s = s.Trim();
            string ressult = " ";
            //ressult = cn.XoaKhoangTrang(s);
            ressult = ChucNang.XoaKhoangTrang(s);
            Console.Write("*");
            Console.Write(ressult);
            Console.WriteLine("*");

            //cn.TrimString(ressult);
            ChucNang.TrimString(ressult, pathIn);
        }

        public void ReadBinaryValue(TextBox TboxResult, string pathIn)
        {
            using (BinaryReader br = new BinaryReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read)))
            {
                while ( br.BaseStream.Position != br.BaseStream.Length )
                {
                    TboxResult.Text += br.ReadInt32() + "-";
                }
                TboxResult.Text = TboxResult.Text.TrimEnd('-');
            }
        }

        Brush textColor = Brushes.Black;
        int count = 1;
        public void PrintPanelStepRUN(StackPanel spnl)
        {
            TextBlock tblStepCount = new TextBlock();
            tblStepCount.Foreground = Brushes.DarkBlue;
            tblStepCount.Text = "Step: " + count.ToString() ;
            count++;
            spnl.Children.Add(tblStepCount);
        }

        public void PrintPanelRUN(StackPanel spnl, int m, string pathIn, string name)
        {

            TextBlock tblStep = new TextBlock();

            tblStep.Text = "\t" + name + "[ ";
            if(name == "F0: ") 
            {
                tblStep.Foreground = Brushes.DarkViolet;
            }
            else
            {
                textColor = textColor == Brushes.Black? Brushes.Red : Brushes.Black;
                tblStep.Foreground = textColor;
            }

            int i = 0;          
            BinaryReader br = new BinaryReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read));
            while(br.BaseStream.Position != br.BaseStream.Length)
            {
                if(i < m)
                {
                    ++i;
                    tblStep.Text += br.ReadInt32().ToString() + " ";
                }
                else
                {
                    i = 0;
                    tblStep.Text += "] - [ ";
                }
            }
            tblStep.Text += "]";
            spnl.Children.Add(tblStep);
            br.Close();
        }

        //<>Tự nhiên
        public void PrintPanelNatural(StackPanel spnl, string pathIn)
        {
            Console.WriteLine("SORT NATURAL PRINT");
            BinaryReader br = new BinaryReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read));
            BinaryReader br_1 = new BinaryReader(new FileStream("Sort/NT_f1.DH19PM", FileMode.Open, FileAccess.Read));
            BinaryReader br_2 = new BinaryReader(new FileStream("Sort/NT_f2.DH19PM", FileMode.Open, FileAccess.Read));
            BinaryReader br_position = new BinaryReader(new FileStream("Sort/NT_Position", FileMode.Open, FileAccess.Read));
            TextBlock tblStep_f1 = new TextBlock();
            TextBlock tblStep_f2 = new TextBlock();
            TextBlock tblStep_f0 = new TextBlock();
            TextBlock tblstep = new TextBlock(); tblstep.Foreground = Brushes.DarkBlue;
            tblStep_f1.Text = "\tF1: "; tblStep_f1.Foreground = Brushes.Black;
            tblStep_f2.Text = "\tF2: "; tblStep_f2.Foreground = Brushes.Red;
            tblStep_f0.Text = "\tF0: "; tblStep_f0.Foreground = Brushes.DarkViolet;
            int position;
            int i;
            string num;
            tblstep.Text = "Step: " + count.ToString();
            count++;
            while (true)
            {
                i = 0;
                position = br_position.ReadInt32();
                //<> Mở ngoặc
                tblStep_f1.Text += "[ ";
                tblStep_f0.Text += "[ ";
                while (i < position)
                {
                    ++i;
                    num = br.ReadInt32().ToString() + " ";
                    tblStep_f1.Text += br_1.ReadInt32().ToString() + " ";
                    tblStep_f0.Text += num;
                }
                //</>Đóng Ngoặc
                tblStep_f1.Text += "]  ";
                position = br_position.ReadInt32();
                i = 0;
                //<> Mở ngoặc
                tblStep_f2.Text += "[ ";
                while (i < position)
                {
                    ++i;
                    num = br.ReadInt32().ToString() + " ";
                    tblStep_f2.Text += br_2.ReadInt32().ToString() + " "; ;
                    tblStep_f0.Text += num;
                }
                //</>Đóng Ngoặc
                tblStep_f2.Text += "]  ";
                tblStep_f0.Text += "]  ";

                if (br_position.BaseStream.Position == br_position.BaseStream.Length)
                    break;
            }
            spnl.Children.Add(tblstep);
            spnl.Children.Add(tblStep_f1);
            spnl.Children.Add(tblStep_f2);
            spnl.Children.Add(tblStep_f0);
            br.Close(); br_1.Close(); br_2.Close(); br_position.Close();
        }

        //<> in step lên UI Balanced
        public void PrintPanelBalanced( StackPanel spnl, string fileIn, int m)
        {
            TextBlock countStep = new TextBlock();
            countStep.Foreground = Brushes.BlueViolet;
            countStep.Text = "Step: " + count.ToString();
            spnl.Children.Add(countStep);
            count++;
            TextBlock[] tbl = new TextBlock[m];
            BinaryReader[] br = new BinaryReader[m]; //Đọc
            BinaryReader br_position = new BinaryReader(new FileStream("Sort/" + fileIn + ".Position", FileMode.Open, FileAccess.Read));

            for (int j = 0; j < m; j++)
            {
                br[j] = new BinaryReader(new FileStream("Sort/Bal_" + fileIn + (j + 1) + ".DH19PM", FileMode.Open, FileAccess.Read));
                tbl[j] = new TextBlock();
                textColor = textColor == Brushes.Black ? Brushes.Red : Brushes.Black;
                tbl[j].Foreground = textColor;              
                tbl[j].Text = "\tF" + (j + 1) + ": ";
            }


            int i = 0;
            int position;
            int current = -1;

            while (true)
            {

                position = br_position.ReadInt32();
                current = 0;
                tbl[i].Text += "[";
                while(current < position)
                {
                    tbl[i].Text += br[i].ReadInt32() + " ";
                    current++;
                }
                tbl[i].Text += "]  ";
                if (br_position.BaseStream.Position == br_position.BaseStream.Length)
                {
                    break;
                }
                ChangeIndex(ref i, m);
            }

            for (int j = 0; j < m; j++)
            {
                spnl.Children.Add(tbl[j]);
                //Đóng luồng
                br[j].Close();
            }

            br_position.Close();
        }

        private int ChangeIndex(ref int i, int m)
        {
            ++i;
            if (i == m)
                i = 0;
            return i;
        }


    }
}
