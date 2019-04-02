using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SapXep
{
    public class DanhSach
    {
        public int ID { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public int Tuoi { get; set; }
        public string Ten { get; set; }
        public DanhSach(int _ID, string _HoTen, string _GioiTinh, int _Tuoi)
        {
            ID = _ID;
            HoTen = _HoTen;
            GioiTinh = _GioiTinh;
            Tuoi = _Tuoi;
        }
    }
    public enum SoSanh
    {
        TUOI = 1, TEN = 2, HOTEN = 3, GIOITINH = 4,
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch mStopwatch = new Stopwatch();

            IList<DanhSach> Input = new List<DanhSach>();
            IList<DanhSach> Output = new List<DanhSach>();
            Input.Add(new DanhSach(0123,"Nguyen Van A", "Nam", 20));
            Input.Add(new DanhSach(2656,"Hoang Quoc Viet", "Nam", 20));
            Input.Add(new DanhSach(1256,"Nguyen Hoang N", "Nam", 19));
            Input.Add(new DanhSach(1542,"Hoang Thi Thuy Linh", "Nu", 18));
            Input.Add(new DanhSach(7445,"Phan Van An", "Nu", 25));
            Input.Add(new DanhSach(2353,"Pham Thuy Linh", "Nu", 21));
            Input.Add(new DanhSach(1344,"Bui Van Quang", "Nam", 21));
            Input.Add(new DanhSach(5478,"Le Xuan Nam", "Nam", 20));
            Input.Add(new DanhSach(1246,"Nguyen Cong Trinh", "Nam", 18));
            Input.Add(new DanhSach(0787,"Tran Van Xuan", "Nu", 20));
            foreach (DanhSach DS in Input)
            {
                string[] cut = DS.HoTen.Split(' ');
                DS.Ten = cut[cut.Length - 1];
            }

            Console.WriteLine("\n--------Danh Sach--------\n");
            InDanhSach(Input);

            Console.WriteLine("\n1.Sap xep theo Tuoi");
            Console.WriteLine("2.Sap xep theo Ten");
            Console.WriteLine("3.Sap xep theo Ho Ten");
            Console.WriteLine("4.Sap xep theo Gioi Tinh");
            Console.WriteLine("5.Thoat");
            Console.Write("\nChon cach sap xep: ");
            string lua_chon = Console.ReadLine();
            mStopwatch.Start();
            if (lua_chon == "1")
            {
                InDanhSach(SoSanhTheoTuoi(Input));
            }
            else if(lua_chon == "2")
            {
                InDanhSach(SoSanhTheoTen(Input));
            }
            else if (lua_chon == "3")
            {
                InDanhSach(SoSanhTheoHoTen(Input));
            }
            else if (lua_chon == "4")
            {
                InDanhSach(SoSanhTheoGioiTinh(Input));
            }
            else if (lua_chon == "5")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Khong hop le !");
            }
            mStopwatch.Stop();
            Console.WriteLine("Thoi gian thuc hien: {0} ms", mStopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
        static IList<DanhSach> SoSanhTheoTuoi(IList<DanhSach> Input)
        {
            IList<DanhSach> Output = new List<DanhSach>();
            for (int i = 0; i < Input.Count; i++)
            {
                int x = -1;

                for (int j = 0; j < Output.Count; j++)
                {
                    if (SoSanhDanhSach(Input[i], Output[j], SoSanh.TUOI) == Input[i].ID)
                    {
                        x = j;
                        break;
                    }
                }
                if (x >= 0)
                {
                    Output.Insert(x, Input[i]);
                }
                else
                {
                    Output.Add(Input[i]);
                }
            }
            return Output;
        }
        static IList<DanhSach> SoSanhTheoGioiTinh(IList<DanhSach> Input)
        {
            IList<DanhSach> Output = new List<DanhSach>();
            for (int i = 0; i < Input.Count; i++)
            {
                int x = -1;

                for (int j = 0; j < Output.Count; j++)
                {
                    if (SoSanhDanhSach(Input[i], Output[j], SoSanh.GIOITINH) == Input[i].ID)
                    {
                        x = j;
                        break;
                    }
                }
                if (x >= 0)
                {
                    Output.Insert(x, Input[i]);
                }
                else
                {
                    Output.Add(Input[i]);
                }
            }
            return Output;
        }
        static IList<DanhSach> SoSanhTheoTen(IList<DanhSach> Input)
        {
            IList<DanhSach> Output = new List<DanhSach>();
            for (int i = 0; i < Input.Count; i++)
            {
                int x = -1;

                for (int j = 0; j < Output.Count; j++)
                {
                    if (SoSanhDanhSach(Input[i], Output[j], SoSanh.TEN) == Input[i].ID)
                    {
                        x = j;
                        break;
                    }
                }
                if (x >= 0)
                {
                    Output.Insert(x, Input[i]);
                }
                else
                {
                    Output.Add(Input[i]);
                }
            }
            return Output;
        }
        static IList<DanhSach> SoSanhTheoHoTen(IList<DanhSach> Input)
        {
            IList<DanhSach> Output = new List<DanhSach>();
            for (int i = 0; i < Input.Count; i++)
            {
                int x = -1;

                for (int j = 0; j < Output.Count; j++)
                {
                    if (SoSanhDanhSach(Input[i], Output[j], SoSanh.HOTEN) == Input[i].ID)
                    {
                        x = j;
                        break;
                    }
                }
                if (x >= 0)
                {
                    Output.Insert(x, Input[i]);
                }
                else
                {
                    Output.Add(Input[i]);
                }
            }
            return Output;
        }

        static int SoSanhDanhSach(DanhSach a, DanhSach b, SoSanh ThuocTinh)
        {
            if (ThuocTinh == SoSanh.TUOI)
            {
                if(a.Tuoi < b.Tuoi)
                {
                    return a.ID;
                }
            }
            else if (ThuocTinh == SoSanh.HOTEN)
            {
                if(SoSanhString(a.HoTen , b.HoTen) == a.HoTen)
                {
                    return a.ID;
                }
            }
            else if (ThuocTinh == SoSanh.TEN)
            {
                if(SoSanhString(a.Ten , b.Ten) == a.Ten)
                {
                    return a.ID;
                }
            }
            else if (ThuocTinh == SoSanh.GIOITINH)
            {
                if (SoSanhString(a.GioiTinh, b.GioiTinh) == a.GioiTinh)
                {
                    return a.ID;
                }
            }
            return b.ID;
        }
        static String SoSanhString(string a, string b)
        {
            for (int i = 0; i < Math.Min(a.Length, b.Length); i++)
            {
                if (a[i] < b[i])
                {
                    return a;
                }
                else if (a[i] > b[i])
                {
                    return b;
                }
                else if (a[i] == b[i])
                {
                    return b;
                }
            }
            return a;
        }
        static void InDanhSach(IList<DanhSach> KetQua)
        {
            foreach (DanhSach DS in KetQua)
            {
                Console.WriteLine(DS.ID + " ..... " + DS.HoTen + " ..... " + DS.Tuoi + " ..... " + DS.GioiTinh);
            }
        }
    }

}
