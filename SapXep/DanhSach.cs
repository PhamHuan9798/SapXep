using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class FileDS
    {
        public int SoLuong { get; set; }
        public IList<DanhSach> DanhSachHocSinh { get; set; }
    }
}
