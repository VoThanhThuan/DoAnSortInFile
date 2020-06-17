using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIReadWriteFile
{
    class RUN
    {
        Methods methods = new Methods();
        StackPanel spnl;
        public RUN(StackPanel stackPanel)
        {
            spnl = stackPanel;
        }
        public void Chia(string pathIn, int m, int max_m)
        {
            try
            {
                // luồn đọc ghi dữ liệu
                BinaryReader br = new BinaryReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read));
                BinaryWriter bw_1 = new BinaryWriter(new FileStream("Sort/RUN_f1.DH19PM", FileMode.Create, FileAccess.Write));
                BinaryWriter bw_2 = new BinaryWriter(new FileStream("Sort/RUN_f2.DH19PM", FileMode.Create, FileAccess.Write));
                //đọc dữ liệu
                int dem;
                int stop = 0;
                int num = 0;
                //61 47 33 38 62 48 18 20 25 
                while (stop < max_m)
                {
                    //Ghi vào F1
                    dem = 0;
                    while (stop < max_m && dem < m)
                    {
                        num = br.ReadInt32();
                        bw_1.Write(num);
                        ++dem; ++stop;
                    }
                    //Ghi vào F2
                    dem = 0;
                    while (stop < max_m && dem < m)
                    {
                        num = br.ReadInt32();
                        bw_2.Write(num);
                        ++dem; ++stop;
                    }
                }
                //đóng luồn
                bw_1.Close();
                bw_2.Close();
                br.Close();
                //<> Print lên UI
                methods.PrintPanelStepRUN(spnl);
                methods.PrintPanelRUN(spnl, m, "Sort/RUN_f1.DH19PM", "F1: ");
                methods.PrintPanelRUN(spnl, m, "Sort/RUN_f2.DH19PM", "F2: ");

            }
            catch (Exception e)
            {
                Console.WriteLine("Chia: " + e.Message);
            }

        }

        private int Sort(int num_L, int num_R)
        {
            if (num_L <= num_R)
                return -1;
            else
                return 1;
        }

        public void SapXep(int m, int max_m, string pathOut)
        {
            try
            {
                BinaryReader br_1 = new BinaryReader(new FileStream("Sort/RUN_f1.DH19PM", FileMode.Open, FileAccess.Read));
                BinaryReader br_2 = new BinaryReader(new FileStream("Sort/RUN_f2.DH19PM", FileMode.Open, FileAccess.Read));
                BinaryWriter bw = new BinaryWriter(new FileStream(pathOut, FileMode.Create, FileAccess.Write));
                int stop = 0, let = 0, right = 0, num_L = -1, num_R = -1;
                bool check = false;
                //đọc dữ liệu từ 2 file 
                num_L = br_1.ReadInt32();
                num_R = br_2.ReadInt32();
                while (stop < max_m)
                {
                    let = 0; right = 0;
                    while ((let < m && right < m) && (br_1.BaseStream.Position <= br_1.BaseStream.Length) && (br_2.BaseStream.Position <= br_2.BaseStream.Length))
                    {
                        if (Sort(num_L, num_R) < 0) // nếu num_L nhỏ hơn num_R thì ghi L vào trước
                        {
                            let++; stop++;
                            bw.Write(num_L);
                            if (br_1.BaseStream.Position != br_1.BaseStream.Length) // kiểm tra hết dữ liệu chưa
                                num_L = br_1.ReadInt32();
                            else
                                break;
                        }
                        else //ngược lại ghi R vào trước
                        {
                            right++; stop++;
                            bw.Write(num_R);
                            if (br_2.BaseStream.Position != br_2.BaseStream.Length) // kiểm tra hết dữ liệu chưa
                                num_R = br_2.ReadInt32();
                            else
                            {
                                check = true;
                                break;
                            }
                        }
                    }
                    //copy những số không sắp xếp
                    while (let < m && stop < max_m )
                    {
                        bw.Write(num_L);
                        let++; stop++;
                        if (br_1.BaseStream.Position != br_1.BaseStream.Length)
                            num_L = br_1.ReadInt32();
                    }
                    while (right < m && stop < max_m )
                    {
                        bw.Write(num_R);
                        right++; stop++;
                        if (br_2.BaseStream.Position != br_2.BaseStream.Length)
                            num_R = br_2.ReadInt32();
                        else
                            check = true;
                    }

                    if(check == true)
                    {
                        while (stop < max_m && (br_1.BaseStream.Position <= br_1.BaseStream.Length))
                        {
                            bw.Write(num_L);
                            stop++;
                            if (br_1.BaseStream.Position != br_1.BaseStream.Length)
                                num_L = br_1.ReadInt32();
                            else
                            {
                                break;
                            }
                        }
                    }

                }
                br_1.Close(); br_2.Close(); bw.Close();

                methods.PrintPanelRUN(spnl, m * 2, pathOut, "F0: ");
            }
            catch (Exception e)
            {
                Console.WriteLine("Sap Xep: " + e.Message);
            }
        }

    }

}
