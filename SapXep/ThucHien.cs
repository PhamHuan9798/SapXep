using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SapXep
{
    public class ThucHien
    {
       public static void TH()
        {
            Stopwatch mStopwatch = new Stopwatch();

            //String Json_data = File.ReadAllText(@"C:\Users\Administrator\Desktop\DanhSach.json");
            //FileDS Input = JsonConvert.DeserializeObject<FileDS>(Json_data);

            List<DanhSach> Input = new List<DanhSach>();

            //Đọc file XML
            string doc = File.ReadAllText(@"C:\Users\Administrator\Desktop\DanhSachHocSinh.xml");
            XmlSerializer DanhSach = new XmlSerializer(typeof(FileDS));
            FileDS Output = new FileDS();
            using (TextReader read = new StringReader(doc))
            {
                Output = (FileDS)DanhSach.Deserialize(read);
            }
            Input = Output.DanhSachHocSinh;
            Console.WriteLine("\n--------Danh Sach--------\n");
            foreach (DanhSach DS in Input)
            {
                string[] cut = DS.HoTen.Split(' ');
                DS.Ten = cut[cut.Length - 1];
            }

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
                InDanhSach(ThuatToan.SoSanhTheoTuoi(Input));
                Export_XML1(ThuatToan.SoSanhTheoTuoi(Input));
            }
            else if (lua_chon == "2")
            {
                InDanhSach(ThuatToan.SoSanhTheoTen(Input));
                Export_XML1(ThuatToan.SoSanhTheoTen(Input));
            }
            else if (lua_chon == "3")
            {
                InDanhSach(ThuatToan.SoSanhTheoHoTen(Input));
                Export_XML1(ThuatToan.SoSanhTheoHoTen(Input));
            }
            else if (lua_chon == "4")
            {
                InDanhSach(ThuatToan.SoSanhTheoGioiTinh(Input));
                Export_XML1(ThuatToan.SoSanhTheoGioiTinh(Input));
            }
            else if (lua_chon == "5")
            {
                InDanhSach(ThuatToan.SapXepTenTheoGT(Input));
                Export_XML1(ThuatToan.SapXepTenTheoGT(Input));
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
        }
        static void Export_XML1(List<DanhSach> Input)
        {
            FileDS mDanhSach = new FileDS();
            mDanhSach.DanhSachHocSinh = Input;
            XmlSerializer serializer = new XmlSerializer(typeof(FileDS));

            using (TextWriter textWriter = new StreamWriter(@"C:\Users\Administrator\Desktop\A.xml"))
            {
                serializer.Serialize(textWriter, mDanhSach);
                textWriter.Close();
            }
        }
        static void Export_XML2(List<DanhSach> Input)
        {
            // Export XML
            string str = "<? xml version = '1.0' ?>\n<DanhSachHocSinh>\n";
            int x = 0;
            foreach (DanhSach DS in Input)
            {
                str += "<HocSinh>\n" + "   <ID>" + Input[x].ID + "</ID>\n" + "   <HoTen>" + Input[x].HoTen + "</HoTen>\n" + "   <Tuoi>" + Input[x].Tuoi + "</Tuoi>\n" + "   <GioiTinh>" + Input[x].GioiTinh + "</GioiTinh>\n" + "</HocSinh>\n";
                x++;
            }
            str += "</DanhSachHocSinh>\n";
            Console.WriteLine(str);
            File.WriteAllText(@"C:\Users\Administrator\Desktop\a.xml", str);
        }
        static void InDanhSach(List<DanhSach> KetQua)
        {
            foreach (DanhSach DS in KetQua)
            {
                Console.WriteLine(DS.ID + " ..... " + DS.HoTen + " ..... " + DS.Tuoi + " ..... " + DS.GioiTinh);
            }
        }
    }
}
