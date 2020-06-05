using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIReadWriteFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Methods methods = new Methods();
        public MainWindow()
        {
            InitializeComponent();
        }

        string pathIn;
        int tabIndex; //0:HOME//1:RUN//2:NATURAL//3:BALANCED

        #region event
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btChooseFile_Click(object sender, RoutedEventArgs e)
        {
            pathIn = methods.ChooseFile();
            if (File.Exists(pathIn))
            {
                MessageBoxResult result =  MessageBox.Show("Sắp xếp file " + pathIn, "Bạn muốn sắp xếp file này ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    TboxInputNumberBalance.Text = pathIn;
                    switch (tabIndex)
                    {
                        case 1:
                            RunSort();
                            break;
                        case 2:
                            NaturalSort();
                            break;
                        case 3:
                            BalanceSort();
                            break;
                        default:
                            RunSort();
                            break;
                    }
                }
            }
        }
        //<>Random 
        private void randomRUN_Click(object sender, RoutedEventArgs e)
        {
            TboxInputNumberRUN.Text += methods.random().ToString() + " ";
        }

        private void btRandomNatural_Click(object sender, RoutedEventArgs e)
        {
            TboxInputNumberNatural.Text += methods.random().ToString() + " ";
        }

        private void btRandomBalance_Click(object sender, RoutedEventArgs e)
        {
            TboxInputNumberBalance.Text += methods.random().ToString() + " ";
        }
        //</>Random 
        //<>Sort
        private void btSortRun_Click(object sender, RoutedEventArgs e)
        {
            spSorStepRUN.Children.Clear();
            methods.WriteByteValue(TboxInputNumberRUN, pathIn);
            RunSort();
            TboxResultRUN.Clear();
            methods.ReadBinaryValue(TboxResultRUN, pathIn);
        }

        private void btSortNatural_Click(object sender, RoutedEventArgs e)
        {
            spSorStepNatural.Children.Clear();//UI
            methods.WriteByteValue(TboxInputNumberNatural, pathIn);
            NaturalSort();
            TboxResultNatural.Clear();
            methods.ReadBinaryValue(TboxResultNatural, pathIn);
        }

        private void btSortBalance_Click(object sender, RoutedEventArgs e)
        {
            spSorStepBalance.Children.Clear();
            methods.WriteByteValue(TboxInputNumberBalance, pathIn);
            string filePath = BalanceSort();
            if(filePath != "0")
            {
                TboxResultBalance.Clear();
                methods.ReadBinaryValue(TboxResultBalance, filePath);
            }

        }
        //</>Sort
        //<> Load
        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> listNumber = new List<int>() { 3, 4, 5 };
            cbboxNumberFile.ItemsSource = listNumber;
            cbboxNumberFile.SelectedIndex = 0;
            new Thread(() =>
            {
                upListCombobox();
            })
            { IsBackground = true }.Start();
        }

        private void tabSortRun_MouseEnter(object sender, MouseEventArgs e)
        {
            pathIn = "Sort/RUN_f0.DH19PM";
            tabIndex = 1;
            Console.WriteLine("RUN");
        }

        private void tabSortNatural_MouseEnter(object sender, MouseEventArgs e)
        {
            pathIn = "Sort/NATURAL_f0.DH19PM";
            tabIndex = 2;
            Console.WriteLine("NATURAL");
        }

        private void tabSortBalanced_MouseEnter(object sender, MouseEventArgs e)
        {
            pathIn = "Sort/BALANCED_f0.DH19PM";
            tabIndex = 3;
        }
        private void tabSortBalanced_Loaded(object sender, RoutedEventArgs e)
        {
            upListCombobox();
        }

        //</> Load
        private void btReadFileTool_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Tool\\ReadBinaryConsole.exe";
                String pathRead = methods.ChooseFile();
                Process process = new Process();
                if (File.Exists(pathRead))
                {
                    process.StartInfo.FileName = path;
                    process.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
                    process.StartInfo.Arguments = pathRead;
                    process.Start();
                }
                else
                {
                    MessageBox.Show("File không tồn tại");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR READ FILE");
            }

        }

        private void btWriteFileTool_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Tool\\WriteBinaryConsole.exe";
                string pathIn = methods.ChooseFileSave();
                Console.WriteLine("Path In {0}", pathIn);
                Process process = new Process();
                if (File.Exists(pathIn))
                {
                    string pathOut = methods.SaveFile();
                    if (pathOut != "")
                    {
                        process.StartInfo.FileName = path;
                        process.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
                        process.StartInfo.Arguments = pathIn + "*" + pathOut;
                        process.Start();
                    }
                }
                else
                {
                    MessageBox.Show("File không tồn tại");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("ERROR WRITE FILE");
            }

        }

        private void TboxInputNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btn_fbThuan_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/anome69/");
        }

        private void btn_fbSon_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/profile.php?id=100012216780649");
        }

        private void btn_fbToan_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/profile.php?id=100011405461763");
        }

        #endregion </Event> </---------------------------------------------------------------------------------------------------->

        #region Methods <---------------------------------------------------------------------------------------------------->


        private void RunSort()
        {
            RUN run = new RUN(spSorStepRUN);
            int max_m = ChucNang.DoDaiDuLieu(pathIn);
            int m = 1, count = 1;
            while (m < max_m)
            {
                run.Chia(pathIn, m, max_m);
                run.SapXep(m, max_m, pathIn);
                m *= 2;
                count++;
            }
        }
        private void NaturalSort()
        {
            int max_m = ChucNang.DoDaiDuLieu(pathIn);
            Nature nt = new Nature(spSorStepNatural);
            Boolean stop = false;
            do
            {
                nt.Chia(max_m, pathIn);
                stop = nt.SapXep(pathIn);
            } while (stop == false);
        }

        private string BalanceSort()
        {
            int max_m = ChucNang.DoDaiDuLieu(pathIn);
            if (max_m < 3)
            {
                MessageBox.Show("Cần tối thiểu 3 số để thực hiện giải thuật này", Title = "Không thể thực hiện", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                Balanced_2 balanced_2 = new Balanced_2(spSorStepBalance);
                int m = (int)cbboxNumberFile.SelectedItem;
                balanced_2.ReNewFile(m);
                balanced_2.Chia(m, max_m, pathIn);
                return balanced_2.fileCuoi;
            }
            return "0";
        }

        private void upListCombobox()
        {
            List<int> listNumber = new List<int>() { 3, 4, 5 };
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                for (int i = 6; i < 99; i++)
                {
                    listNumber.Add(i);
                }
                cbboxNumberFile.ItemsSource = listNumber;               
            }));

        }



        #endregion <---------------------------------------------------------------------------------------------->

    }
}
