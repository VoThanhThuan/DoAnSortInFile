using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIReadWriteFile
{
    class ChucNang
    {
        //BinaryReader br;
        //BinaryWriter bw;
        public static void TrimString(string s, string pathIn)
        {
            BinaryWriter bw;
            //ghi dữ liệu
            try
            {
                //FileStream F = new FileStream("binary.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //bw = new BinaryWriter(new FileStream("RUN_f0.DH19PM", FileMode.Create));
                bw = new BinaryWriter(new FileStream(pathIn, FileMode.Create, FileAccess.Write));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "Không thể tạo file");
                return;
            }

            string[] trimS = s.Split(' ');

            int number = -1;
            //string sn = "";
            foreach (var item in trimS)
            {
                // Console.WriteLine(item);
                int.TryParse(item, out number);
                bw.Write(number);
            }


            #region for
           
            #endregion

            //}
            //bw.Write("");
            //bw.Close();
            bw.Close();
            //Đọc dữ liệu
            //BinaryReader br;
            #region Đọc dữ liệu
            /*            FileStream fr;
                        try
                        {
                            //br = new BinaryReader(new FileStream("RUN_f0.DH19PM", FileMode.Open) );
                            fr = new FileStream(pathIn, FileMode.Open, FileAccess.Read);
                            int n = -1;

                            while ( ( n = fr.ReadByte() ) != -1  )
                            {
                                Console.Write(n + " ");
                            }
                            Console.WriteLine("- Kết Thúc -");
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message + "Khong the doc file");
                            return;
                        }
                        fr.Close();*/
            #endregion
        }

        #region Xóa Khoảng trắng
        /*
        //-------------------------xóa ký tự
        public static string Xoa(string s, int viTriXoa)
        {
            int n = (s.Length); // n = số lượng ký tự trong chuỗi
            char[] c = s.ToCharArray();

            for (int i = viTriXoa; i < n-1; i++)
            {
                c[i] = c[i + 1];
                //Console.WriteLine(c[i]);
            }
            c[n - 1] = '\0'; //kết thúc
            s = "";
            foreach (var item in c)
            {
                s += item;
            }

            //for(int i=0; i<c.Length-1; i++)
            //{
            //    s += c[i];
            //}

            return s;
        }
        */
        //---------------xóa khoảng trắng
        public static string XoaKhoangTrang(string s)
        {
            // 1 2 3     4 4      5 54
            while (s.IndexOf("  ") != -1)
            {
                s = s.Replace("  ", " ");
            }
            #region thủ công
            /*for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == ' ' && s[i + 1] == ' ')
                {
                    s = Xoa(s, i);
                    i--;
                }

            }

            //xóa khoảng trắng đầu chuỗi
            if (s[0] == ' ')
            {
                s = Xoa(s, 0);
            }
            //xóa khoảng trắng cuối chuỗi
            if(s[s.Length-1] == ' ')
            {
                s = Xoa(s, s.Length-1);
            }

            return s + " ";*/
            #endregion

            return s;

        }
        #endregion

        //đo độ dài của file
        #region Đo độ dài dữ liệu
        public static int DoDaiDuLieu(string pathIn)
        {
            int dem = 0;
            try
            {
                BinaryReader bw = new BinaryReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read));
                while (bw.BaseStream.Position != bw.BaseStream.Length)
                {
                    bw.ReadInt32();
                    ++dem;
                }
                bw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dem;
        }
        #endregion

    }
}
