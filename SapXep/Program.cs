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



            Console.WriteLine("\n1.Sap xep theo Tuoi");
            Console.WriteLine("2.Sap xep theo Ten");
            Console.WriteLine("3.Sap xep theo Ho Ten");
            Console.WriteLine("4.Sap xep theo Gioi Tinh");
            Console.WriteLine("5.Sap xep Ten theo Gioi Tinh");
            Console.WriteLine("6.Thoat");
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
                InDanhSach(SapXepTenTheoGT(Input.DanhSachHocSinh));
            }
            else if (lua_chon == "6")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Khong hop le !");
            }
            mStopwatch.Stop();
            Console.WriteLine("Thoi gian thuc hien: {0} ms", mStopwatch.ElapsedMilliseconds);

            IList<DanhSach> Output = new List<DanhSach>();

            ////InsertionSort
            //mStopwatch.Start();
            //SoSanhTheoTen(Input.DanhSachHocSinh);
            //mStopwatch.Stop();
            //Console.WriteLine("Thoi gian thuc hien InsertionSort: {0} ms", mStopwatch.ElapsedMilliseconds);

            ////BubbleSort
            //mStopwatch.Reset();
            //mStopwatch.Start();
            //BubbleSort_Ten(Input.DanhSachHocSinh);
            //mStopwatch.Stop();
            //Console.WriteLine("Thoi gian thuc hien BubbleSort: {0} ms", mStopwatch.ElapsedMilliseconds);

            ////SelectionSort
            //mStopwatch.Reset();
            //mStopwatch.Start();
            //SelectionSort_Ten(Input.DanhSachHocSinh);
            //mStopwatch.Stop();
            //Console.WriteLine("Thoi gian thuc hien SelectionSort: {0} ms", mStopwatch.ElapsedMilliseconds);

            Console.ReadKey();
        }

        static IList<DanhSach> BubbleSort_Ten(IList<DanhSach> Input)
        {
            
            for (int i = 0; i < Input.Count; i++)
            {
                for (int j = Input.Count - 1; j > i ; j--)
                {
                    if (SoSanhDanhSach(Input[j], Input[j - 1], SoSanh.TEN) == Input[j].ID)
                    {
                        DanhSach tp = Input[j];
                        Input[j] = Input[j - 1];
                        Input[j - 1] = tp;
                    }
                }
            }
            return Input;
        }
        public static IList<DanhSach> SelectionSort_Ten(IList<DanhSach> Input)
        {
            int min;
            for (int i = 0; i < Input.Count; i++)
            {
                min = i;
                for (int j = i + 1; j < Input.Count; j++)
                {
                    if (SoSanhDanhSach(Input[j], Input[min], SoSanh.TEN) == Input[j].ID)
                    {
                        min = j;
                    }
                }
                DanhSach temp = Input[i];
                Input[i] = Input[min];
                Input[min] = temp;
            }
            return Input;
        }

        static IList<DanhSach> TenNam(IList<DanhSach> Input)
        {
            IList<DanhSach> Output = new List<DanhSach>();
            for (int a = 0; a < Input.Count; a++)
            {
                if (Input[a].GioiTinh == "Nam")
                {
                    Output.Add(Input[a]);
                }
            }
            return Output;
        }
        static IList<DanhSach> TenNu(IList<DanhSach> Input)
        {
            IList<DanhSach> Output = new List<DanhSach>();
            for (int a = 0; a < Input.Count; a++)
            {
                if (Input[a].GioiTinh == "Nu")
                {
                    Output.Add(Input[a]);
                }
            }
            return Output;
        }
        static IList<DanhSach> SapXepTenTheoGT(IList<DanhSach> Input)
        {
           IList<DanhSach> Output = SoSanhTheoTen(TenNam(Input));
            foreach(DanhSach Tnu in SoSanhTheoTen(TenNu(Input)))
            {
                Output.Add(Tnu);
            }
            return Output;
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
