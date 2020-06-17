using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UIReadWriteFile
{
    class Nature
    {
        #region UI

                Methods methods = new Methods();

        StackPanel spnl;

        public Nature(StackPanel spnl)
        {
            this.spnl = spnl;
        }

            #endregion


        public void Chia(int max_m, string pathIn)
        {
            //biến đếm số lượng phần tử đã đọc để dừng chương trình
            int stop = 0;
            //không dùng    /* Boolean stopSort = true;
            //biến chứa dữ liệu
            var num_1 = -1;
            var num_2 = -1;
            //biến vị trí
            var p1 = 0;
            var p2 = 0;
            try
            {
                //lệnh ghi ra ô cứng file của f1 và f2.
                BinaryWriter bw_1 = new BinaryWriter(new FileStream("Sort/NT_f1.DH19PM", FileMode.Create, FileAccess.Write));
                BinaryWriter bw_2 = new BinaryWriter(new FileStream("Sort/NT_f2.DH19PM", FileMode.Create, FileAccess.Write));
                //dữ liệu vị trí
                BinaryWriter bw_position = new BinaryWriter(new FileStream("Sort/NT_Position", FileMode.Create, FileAccess.Write));
                //lệnh đọc file f0.
                BinaryReader br = new BinaryReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read));

                //đọc dữ liệu
                num_1 = br.ReadInt32();
                num_2 = br.ReadInt32();
                //cần gạt để chuyển file ghi
                Boolean lever = true;

                //vòng lặp để đọc file và so sánh file
                while (stop < max_m)
                {
                    //vòng lặp ghi vào f1
                    while (stop < max_m && lever == true)
                    {
                        ++stop; ++p1; //p1: số lượng các số tăng tự nhiên VD: 1 2 9  8 thì p1 = 3
                        //kiểm tra số có tăng tự nhiên hay không (số tăng tự nhiên là số trước nhỏ hơn số sau)
                        if (num_1 <= num_2)
                        {
                            /* 1 2 9 8 7
                             * a           b
                             * [1]        [2]
                             * so sánh a và b, thấy a nhỏ hơn thì ghi a, rồi gán a = b = 2
                             * tiếp tục đọc file b suy ra b = 9
                             * a        b
                             * [2]        [9]
                             */
                            bw_1.Write(num_1); 
                            num_1 = num_2;     
                            if (br.BaseStream.Position != br.BaseStream.Length) //kiểm tra đã đọc đến cuối mảng chưa ?
                                num_2 = br.ReadInt32(); //đọc số tiếp theo 
                        }
                        else
                        {
                            bw_1.Write(num_1);
                            num_1 = num_2;
                            if (br.BaseStream.Position != br.BaseStream.Length) //kiểm tra đã đọc đến cuối mảng chưa ?
                                num_2 = br.ReadInt32();
                            lever = false;
                        }
                    }
                    bw_position.Write(p1);
                    //vòng lặp ghi vào f2
                    while (stop < max_m && lever == false)
                    {
                        ++stop; ++p2;
                        if (num_1 <= num_2)
                        {
                            bw_2.Write(num_1);
                            num_1 = num_2;
                            if (br.BaseStream.Position != br.BaseStream.Length)
                                num_2 = br.ReadInt32();
                        }
                        else
                        {
                            bw_2.Write(num_1);
                            num_1 = num_2;
                            if (br.BaseStream.Position != br.BaseStream.Length)
                                num_2 = br.ReadInt32();
                            lever = true;
                        }
                    }
                    bw_position.Write(p2);
                    p1 = 0; p2 = 0;
                }
                bw_1.Close(); bw_2.Close(); br.Close(); bw_position.Close();//đóng luồng auto phải có 
            }
            catch (Exception e)
            {
                Console.WriteLine("Chia: " + e.Message);
            }
            //--return stopSort;
        }

        public Boolean SapXep(string pathOut)
        {
            int num_f1, num_f2;
            int stop = 0;
            //biến lưu vị trí từ file positon
            var p1 = 1;
            var p2 = 1;
            //biến nhân biết vị trí hiện tại của file fr_f1, fr_f2
            int CurrentPosition_1;
            int CurrentPosition_2;
            try
            {
                //file đầu vào
                BinaryReader br_1 = new BinaryReader(new FileStream("Sort/NT_f1.DH19PM", FileMode.Open, FileAccess.Read));
                BinaryReader br_2 = new BinaryReader(new FileStream("Sort/NT_f2.DH19PM", FileMode.Open, FileAccess.Read));

                if (br_2.BaseStream.Length == 0)
                {
                    br_1.Close(); br_2.Close();
                    methods.PrintPanelNatural(spnl, pathOut);
                    return true;
                }

                //file dữ liệu vị trí
                BinaryReader br_position = new BinaryReader(new FileStream("Sort/NT_Position", FileMode.Open, FileAccess.Read));
                //file ra
                BinaryWriter bw = new BinaryWriter(new FileStream(pathOut, FileMode.Create, FileAccess.Write));

                //đọc dữ liệu
                num_f1 = br_1.ReadInt32();
                num_f2 = br_2.ReadInt32();

                //so sánh rồi đưa vào file f0
                while (br_position.BaseStream.Position != br_position.BaseStream.Length)//kt ? cuối mảng
                {
                    //đọc vị trí nhóm các số tăng tự nhiên đã gom
                    p1 = br_position.ReadInt32();
                    p2 = br_position.ReadInt32();
                    ++stop;
                    CurrentPosition_1 = 0; CurrentPosition_2 = 0; // vị trí hiện tại
                    while (CurrentPosition_1 < p1 && CurrentPosition_2 < p2)  //p = số lượng phần tử đã chia nhóm
                    {
                        if (num_f1 < num_f2)
                        {
                            bw.Write(num_f1);
                            ++CurrentPosition_1;
                            if (br_1.BaseStream.Position != br_1.BaseStream.Length)
                                num_f1 = br_1.ReadInt32();

                        }
                        else
                        {
                            bw.Write(num_f2);
                            ++CurrentPosition_2;
                            if (br_2.BaseStream.Position != br_2.BaseStream.Length)
                                num_f2 = br_2.ReadInt32();
                        }
                    }
                    //chép dữ liệu còn thừa ở "f1" vào f0
                    while (CurrentPosition_1 < p1)
                    {
                        bw.Write(num_f1);
                        ++CurrentPosition_1;
                        if (br_1.BaseStream.Position != br_1.BaseStream.Length)
                            num_f1 = br_1.ReadInt32();
                    }
                    //chép dữ liệu còn thừa ở "f2" vào f0
                    while (CurrentPosition_2 < p2)
                    {
                        bw.Write(num_f2);
                        ++CurrentPosition_2;
                        if (br_2.BaseStream.Position != br_2.BaseStream.Length)
                            num_f2 = br_2.ReadInt32();
                    }
                    
                }

                bw.Close(); br_1.Close(); br_2.Close(); br_position.Close();
                methods.PrintPanelNatural(spnl, pathOut);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sap Xep: " + e.Message);
            }
            if (stop > 1)
                return false;
            return true;
        }

        //hàm để class khác sử dụng
/*        public void CallSort(string pathIn, string pathOutTemp, int max_m)
        {
            Boolean stop = false;
            Chia(max_m, pathIn);
            stop = SapXep(pathOutTemp);
            do
            {
                Chia(max_m, pathOutTemp);
                stop = SapXep(pathOutTemp);
            } while (stop == false);
        }*/

    }
}
