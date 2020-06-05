using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UIReadWriteFile
{
    class Balanced_2
    {
        StackPanel spnl = new StackPanel();
        Methods methods = new Methods();
        public Balanced_2(StackPanel spnl)
        {
            this.spnl = spnl;
        }

        public string fileCuoi;
        public void ReNewFile(int m)
        {
            //tạo file trước
            BinaryWriter bfw;
            BinaryWriter bgw;
            for (int i = 0; i < m; ++i)
            {
                bfw = new BinaryWriter(new FileStream("Sort/Bal_f" + (i + 1) + ".DH19PM", FileMode.Create));
                bgw = new BinaryWriter(new FileStream("Sort/Bal_g" + (i + 1) + ".DH19PM", FileMode.Create));
                bfw.Close(); bgw.Close();
            }
        }

        public void Chia(int m, int max_m, string pathIn)
        {
            var stop = 0;
            var stt = 0;
            int num_1, num_2;
            //số thứ tự của file g
            int stt_g = 0;
            //biến vị trí
            var p = 0;
            try
            {
                //dữ liệu vào
                BinaryReader br = new BinaryReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read));
                //FileStream fr = new FileStream(pathIn, FileMode.Open, FileAccess.Read);
                //dữ liệu ra
                BinaryWriter[] bw = new BinaryWriter[m];
                //FileStream[] fw = new FileStream[m];
                //dữ liệu vị trí
                BinaryWriter bw_fposition = new BinaryWriter(new FileStream("Sort/f.Position", FileMode.Create, FileAccess.Write));
                //FileStream position = new FileStream("Sort/f.Position", FileMode.Create, FileAccess.Write);
                //đọc dữ liệu
                num_1 = br.ReadInt32();
                num_2 = br.ReadInt32();
                //tạo file trước
                for (int i = 0; i < m; ++i)
                {
                    bw[i] = new BinaryWriter(new FileStream("Sort/Bal_f" + (i + 1) + ".DH19PM", FileMode.Create, FileAccess.Write));
                }
                //chia 
                while (stop < max_m)
                {

                    while (stt < m && (stop < max_m))
                    {
                        while (stop < max_m)
                        {
                            ++p; ++stop;
                            if (num_1 <= num_2)
                            {
                                bw[stt].Write(num_1);
                                num_1 = num_2;
                                if (br.BaseStream.Position != br.BaseStream.Length)
                                    num_2 = br.ReadInt32();
                            }
                            else
                            {
                                bw[stt].Write(num_1);
                                num_1 = num_2;
                                if (br.BaseStream.Position != br.BaseStream.Length)
                                    num_2 = br.ReadInt32();
                                break;
                            }
                        }
                        stt++;
                        bw_fposition.Write(p);
                        p = 0;

                    }

                    //Tron(m, stt_g, "f", "Sort/Bal_g");
                    ChangeIndex(ref stt_g, m);
                    stt = 0;
                }
                br.Close();
                bw_fposition.Close();
                for (int i = 0; i < m; ++i)
                {
                    bw[i].Close();
                }
                methods.PrintPanelBalanced(spnl, "f", m);
            }
            catch (Exception e)
            {
                Console.WriteLine("Loi tai ham chia: " + e.Message);
            }
            Tron(m, max_m, "f", "g");
        }

        /// <summary>
        /// <code   
        ///     đọc các file chứa dữ liệu 
        ///     lấy dữ liệu đầu tiên của mỗi file rồi cho vào một mảng "arrVal" sau đó sort mảng
        ///     ghi dữ liệu đầu tiên của mảng
        ///     đọc dữ liệu kế tiếp của file đầu tiên trong mảng "arrVal">
        /// </code>
        /// <Position   
        ///     Xác định độ dài dữ liệu được phép đọc của "m" file
        ///     nếu nếu tất cả "m" file đều chạm đến giới hạn position sẽ đổi file ghi và lấy Position mới>
        /// </Position>
        /// <ĐệQuy Nếu số file có dữ liệu nhiều hơn một thì đệ quy ></ĐệQuy>
        /// </summary>
        /// <param name="m"> Số file chứa dữ liệu </param>
        /// <param name="max_m"> số dữ liệu tối đa cần sắp xếp </param>
        /// <param name="fileIn"> Tên file đưa vào </param>
        /// <param name="fileOut"> Tên file cần xuất ra </param>
        int countStep = 0;
        public void Tron(int m, int max_m, string fileIn, string fileOut)
        {
            int val = -1, stopEndFile = 0, count = 0;
            int i = 0, stop = 0;
            AreaNumber num;
            BinaryReader[] br = new BinaryReader[m]; //Đọc
            BinaryWriter[] bw = new BinaryWriter[m]; //Ghi
            BinaryWriter bw_position = new BinaryWriter(new FileStream("Sort/" + fileOut + ".Position", FileMode.Create, FileAccess.Write));

            for (int j = 0; j < m; j++)
            {
                bw[j] = new BinaryWriter(new FileStream("Sort/Bal_" + fileOut + (j + 1) + ".DH19PM", FileMode.Create, FileAccess.Write));
                br[j] = new BinaryReader(new FileStream("Sort/Bal_" + fileIn + (j + 1) + ".DH19PM", FileMode.Open, FileAccess.Read));
            }
            //Đọc dữ liệu vị trí f.Position
            BinaryReader br_position = new BinaryReader(new FileStream("Sort/" + fileIn + ".Position", FileMode.Open, FileAccess.Read));
            //FileStream po = new FileStream("Sort/" + fileIn + ".Position", FileMode.Open, FileAccess.Read);
            //List<int> listNum = new List<int>();
            List<int> listPosition = new List<int>();

            ArrayList arrVal = new ArrayList();

            int current = 0;
            while (stop < max_m)
            {
                for (int j = 0; j < m; j++)
                {
                    if (br[j].BaseStream.Position != br[j].BaseStream.Length)
                    {
                        val = br[j].ReadInt32();
                        arrVal.Add(new AreaNumber(val, j));
                    }
                    listPosition.Add(0);
                    if (br_position.BaseStream.Position != br_position.BaseStream.Length)
                        listPosition[j] = (br_position.ReadInt32());
                }

                while (arrVal.Count > 0)
                {
                    while (listPosition[i] == 0)
                        ChangeIndex(ref i, m);

                    arrVal.Sort(new SortAreaNumber());
                    num = arrVal[0] as AreaNumber;
                    bw[current].Write(num.Number);
                    count++;
                    stop++;
                    i = num.File;
                    arrVal.RemoveAt(0);
                    listPosition[i]--;
                    if ((br[i].BaseStream.Position != br[i].BaseStream.Length) && listPosition[i] > 0)
                    {
                        val = br[i].ReadInt32();
                        arrVal.Add(new AreaNumber(val, i));
                    }
                }
                ChangeIndex(ref current, m);
                bw_position.Write(count);
                count = 0;
                stopEndFile++;
                listPosition.Clear();
            }

            for (int j = 0; j < m; j++)
            {
                br[j].Close();
                bw[j].Close();
            }
            br_position.Close(); bw_position.Close();
            //Print UI
            countStep++;
            methods.PrintPanelBalanced(spnl, fileOut, m);
            fileCuoi = "Sort/Bal_" + fileOut + "1.DH19PM";
            Console.WriteLine("File cuối là {0}", fileOut);
            if (stopEndFile > 1)
            {
                if (stopEndFile > m)
                    stopEndFile = m;
                Tron((stopEndFile), max_m, fileOut, fileIn);
            }

        }
        /// <summary> 
        /// <A nếu i chạm đến m nó sẽ reset/>
        /// </summary>
        /// <param name="i"> Giá trị cần thay đổi </param>
        /// <param name="m"> Giá trị tối đa </param>
        /// <returns></returns>
        private int ChangeIndex(ref int i, int m)
        {
            ++i;
            if (i == m)
                i = 0;
            return i;
        }

    }

    public class AreaNumber
    {
        private int number;
        private int file;

        public AreaNumber(int number, int file)
        {
            this.Number = number;
            this.File = file;
        }

        public int Number { get => number; set => number = value; }
        public int File { get => file; set => file = value; }
    }

    /// <summary>
    /// Định nghĩa lại hàm Sort cho ArrayList
    /// </summary>
    public class SortAreaNumber : IComparer
    {
        public int Compare(object x, object y)
        {
            AreaNumber num1 = x as AreaNumber;
            AreaNumber num2 = y as AreaNumber;
            if (num1 == null || num2 == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                if (num1.Number > num2.Number)
                {
                    return 1;
                }
                else if (num1.Number == num2.Number)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
