using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace SapXep
{
    public class FileDS
    {
        public int SoLuong { get; set; }
        public IList<DanhSach> DanhSachHocSinh  { get; set; }
}
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

             
            String Json_data = File.ReadAllText(@"C:\Users\Administrator\Desktop\DanhSachHocSinh.json");
            FileDS Input = JsonConvert.DeserializeObject<FileDS>(Json_data);
            
            foreach (DanhSach DS in Input.DanhSachHocSinh)
            {
                string[] cut = DS.HoTen.Split(' ');
                DS.Ten = cut[cut.Length - 1];
            }
            //Console.WriteLine("\n--------Danh Sach--------\n");
            //InDanhSach(Input.DanhSachHocSinh);

            // Dùng LINQ :
            //IList<DanhSach> SapXep = Input.OrderBy(x => x.GioiTinh).ThenBy(x => x.Ten).ToList();
            //InDanhSach(SapXep);

            IList<DanhSach> Output = new List<DanhSach>();
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
                InDanhSach(SoSanhTheoTuoi(Input.DanhSachHocSinh));
            }
            else if (lua_chon == "2")
            {
                InDanhSach(SoSanhTheoTen(Input.DanhSachHocSinh));
            }
            else if (lua_chon == "3")
            {
                InDanhSach(SoSanhTheoHoTen(Input.DanhSachHocSinh));
            }
            else if (lua_chon == "4")
            {
                InDanhSach(SoSanhTheoGioiTinh(Input.DanhSachHocSinh));
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

        static IList<DanhSach> TenNam(IList<DanhSach> Input)
        {
            IList<DanhSach> Output = new List<DanhSach>();
            for (int a = 0; a < Input.Count; a++)
            {
                if (Input[a].GioiTinh == "Nu")
                {
                    Input.Remove(Input[a]);
                    a--;
                }
            }
            return Input;
        }
        static IList<DanhSach> TenNu(IList<DanhSach> Input)
        {
            IList<DanhSach> Output = new List<DanhSach>();
            for (int a = 0; a < Input.Count; a++)
            {
                if (Input[a].GioiTinh == "Nam")
                {
                    Input.Remove(Input[a]);
                    a--;
                }
            }
            return Input;
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
        static String SoSanhString(string _a, string _b)
        {
            string a = _a ?? "";
            string b = _b ?? "";
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
            return b;
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
