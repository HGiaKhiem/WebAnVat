﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebAnVat.Models;
using System.Drawing;
using System.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.Entity;

namespace WebAnVat.Controllers
{
    public class HomeController : Controller
    {
        public string conStr = "workstation id=BanDoAnVatVer2.mssql.somee.com;packet size=4096;user id=HuyKhiemLong123_SQLLogin_1;pwd=t5fpvoy7nc;data source=BanDoAnVatVer2.mssql.somee.com;persist security info=False;initial catalog=BanDoAnVatVer2;TrustServerCertificate=True";
        // GET: Home
       
        public ActionResult Index()
        {
            List<Mon> ds = new List<Mon>();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                string query = "select * from Mon";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Mon m = new Mon();
                    m.ID_Mon = int.Parse(rd.GetValue(0).ToString());
                    m.TenMon = rd.GetValue(1).ToString();
                    m.GiaBan = int.Parse(rd.GetValue(2).ToString());
                    m.ID_LoaiMonAn = int.Parse(rd.GetValue(3).ToString());
                    m.HinhAnh = rd.GetValue(4).ToString();
                    m.KhuyenMai = int.Parse(rd.GetValue(5).ToString()); 
                    var gskgNumber = int.Parse(rd.GetValue(6).ToString());
                    m.GiaSauKhiGiam = gskgNumber;
                    ds.Add(m);
                }
            }
            var foods = ds.Where(t => t.ID_LoaiMonAn == 103).ToList();
            var drinks = ds.Where(t => t.ID_LoaiMonAn == 101 || t.ID_LoaiMonAn == 102 || t.ID_LoaiMonAn == 100).ToList();

            ViewBag.listDrink = drinks;
            ViewBag.listFood = foods;
            return View();
        }

        public ActionResult searchByName(string tenmon)
        {
            List<Mon> ds = new List<Mon>();
            using(SqlConnection conn = new SqlConnection(conStr))
            {
                string query = "select * from Mon where TenMon like '%' +@tenmon+ '%'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenmon", tenmon);
                SqlDataReader rd = cmd.ExecuteReader();
                while(rd.Read())
                {
                    Mon m = new Mon();
                    m.TenMon = rd.GetValue(1).ToString();
                    m.GiaBan = int.Parse(rd.GetValue(2).ToString());
                    m.ID_LoaiMonAn = int.Parse(rd.GetValue(3).ToString());
                    m.HinhAnh = rd.GetValue(4).ToString();
                    m.KhuyenMai = int.Parse(rd.GetValue(5).ToString());

                    var gskgNumber = int.Parse(rd.GetValue(6).ToString());
                    m.GiaSauKhiGiam = gskgNumber;
                    ds.Add(m);
                }    

            }    
            return View(ds);
        }

        public ActionResult detailProducts(int id)
        {   
            //Trả về sản phẩm vừa click vào bằng id
            Mon monDetail = null;
            string query1 = "select * from Mon m where m.ID_Mon = @id";
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query1, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    monDetail = new Mon();
                    monDetail.ID_Mon = int.Parse(rd.GetValue(0).ToString());
                    monDetail.TenMon = rd.GetValue(1).ToString();
                    monDetail.GiaBan = int.Parse(rd.GetValue(2).ToString());
                    monDetail.ID_LoaiMonAn = int.Parse(rd.GetValue(3).ToString());
                    monDetail.HinhAnh = rd.GetValue(4).ToString();
                    monDetail.KhuyenMai= int.Parse(rd.GetValue(5).ToString());
                    monDetail.GiaSauKhiGiam = decimal.Parse(rd.GetValue(6).ToString());
                    
                }
            }
            //Lấy ra Size và giá tăng theo size 
            List<ChiTietSize> s = new List<ChiTietSize>();
            string query3 = "select * from ChiTietSize";
            using(SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query3, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while(rd.Read())
                {
                    ChiTietSize cts = new ChiTietSize();
                    cts.ID_Size = int.Parse(rd.GetValue(0).ToString());
                    cts.Loai_Size = rd.GetValue(1).ToString();
                    cts.GiaTang = int.Parse(rd.GetValue(2).ToString());
                    s.Add(cts);
                }    
            }

            var btnSizes = s;
            ViewBag.btnSizes = btnSizes;

            //Lấy ra danh sách topping theo Loai_Topping
            List<ChiTietTopping> ds = new List<ChiTietTopping>();
            string query2 = "select * from ChiTietTopping";
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query2, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChiTietTopping cttp = new ChiTietTopping();
                    cttp.ID_Topping = int.Parse(rd.GetValue(0).ToString());
                    cttp.Loai_Topping = rd.GetValue(1).ToString();
                    cttp.Ten_Topping = rd.GetValue(2).ToString();
                    cttp.GiaTopp = int.Parse(rd.GetValue(3).ToString());
                    ds.Add(cttp);
                }    
            } 

            var toppTraSua = ds.Where(t => t.Loai_Topping == "Trà sữa" || t.Loai_Topping == "Nước").ToList();
            var toppTra = ds.Where(t => t.Loai_Topping == "Trà" || t.Loai_Topping == "Nước").ToList();
            var toppAnVat = ds.Where(t => t.Loai_Topping == "Ăn Vặt").ToList();
            ViewBag.toppTraSua = toppTraSua;
            ViewBag.toppTra = toppTra;
            ViewBag.toppAnVat = toppAnVat;

            return View(monDetail);
        }

       
        [HttpPost]
        public ActionResult addToCart(productData data)
        {
            List<productData> cart = Session["Cart"] as List<productData>;
            if (cart == null)
            {
                // Nếu giỏ hàng chưa có, tạo mới
                cart = new List<productData>();
            }
            if (data.ID_LoaiMonAn != 100) // kh phải là cf thì sẽ có topping 
                data.Price += GetTotalPrice(data.Topping, data.Size);
            else
                data.Price += GetPriceForCaffe(data.Size);
            data.Price *=(decimal)data.Quantity;
            cart.Add(data); // Thêm sản phẩm vào giỏ hàng

            // Lưu giỏ hàng lại vào session
            Session["Cart"] = cart;
            return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng" });

        }
        public decimal GetPriceForCaffe(string size)
        {
            decimal sizeSum = 0;
            string querysize = "select GiaTang from ChiTietSize where Loai_Size=@loaisize";
            using (SqlConnection conn1 = new SqlConnection(conStr))
            {
                conn1.Open();
                using (SqlCommand cmd = new SqlCommand(querysize, conn1))
                {
                    cmd.Parameters.AddWithValue("@loaisize", size);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            decimal price = int.Parse(rd.GetValue(0).ToString());
                            sizeSum = price;
                        }
                    }
                }
            }
            return sizeSum;
        }
        public decimal GetTotalPrice(List<toppingData> Topping, string size)
        {
            decimal toppingSum = 0;
            decimal sizeSum = 0;

            foreach (var topping in Topping)
            {
                string querytopping = "select GiaTopp from ChiTietTopping where Ten_Topping=@tentopp";
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(querytopping, conn))
                    {
                        cmd.Parameters.AddWithValue("@tentopp", topping.Name); // sửa tham số ở đây
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                decimal price = int.Parse(rd.GetValue(0).ToString());
                                toppingSum += price * topping.Quantity;
                            }
                        }
                    }
                }
            }

            string querysize = "select GiaTang from ChiTietSize where Loai_Size=@loaisize";
            using (SqlConnection conn1 = new SqlConnection(conStr))
            {
                conn1.Open();
                using (SqlCommand cmd = new SqlCommand(querysize, conn1))
                {
                    cmd.Parameters.AddWithValue("@loaisize", size);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            decimal price = int.Parse(rd.GetValue(0).ToString());
                            sizeSum = price;
                        }
                    }
                }
            }

            return toppingSum + sizeSum;
        }

        public ActionResult loadCart()
        {
            List<productData> cart = Session["Cart"] as List<productData>;
            if (cart == null || cart.Count == 0)
            {
                return View(new List<productData>()); // Trả về giỏ hàng rỗng nếu không có sản phẩm
            }
            decimal sum = 0;
            foreach(var item in cart)
            {
                sum+= item.Price;
            }
            decimal tamtinh = sum;
            decimal thanhtien = sum + 15000m;
            decimal shipp = 15000m;
            ViewBag.PhiGH = shipp.ToString("N0");
            ViewBag.slsp=cart != null ? cart.Count : 0;
            ViewBag.tamtinh = tamtinh.ToString("N0");
            ViewBag.thanhtien = thanhtien.ToString("N0");
            return View(cart);
        }

        //Thêm một sản phẩm từ trang chi tiết sản phẩm vào bảng GioHang
        //[HttpPost]
        //public ActionResult addToCart(productData data)
        //{
        //    int idUser = Convert.ToInt32(Session["ID"]);
        //    Session["Id_Mon"] = data.Id;
        //    if (data != null)
        //    {
        //        using (SqlConnection conn = new SqlConnection(conStr))
        //        {
        //            conn.Open();
        //            string query = "insert into GioHang(ID_NgMua, ID_Mon, TenMon, SoLuong, Size, Sweet, Tea, Ice, topping) values(@idNgMua, @idMon, @tenmon, @sl, @size, @sweet, @tea, @ice, @topping)";
        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@idNgMua", idUser);
        //            cmd.Parameters.AddWithValue("@idMon", data.Id);
        //            cmd.Parameters.AddWithValue("@tenmon", data.Name);
        //            cmd.Parameters.AddWithValue("@sl", data.Quantity);
        //            cmd.Parameters.AddWithValue("@size", data.Size);
        //            cmd.Parameters.AddWithValue("@sweet", data.Sweet ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@tea", data.Tea ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@ice", data.Ice ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@topping", JsonConvert.SerializeObject(data.Topping) ?? (object)DBNull.Value);
        //            cmd.ExecuteNonQuery();
        //        }
        //        ViewBag.Message = "Sản phẩm đã được thêm vào giỏ hàng";
        //        return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng" });
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Không thể thêm sản phẩm";
        //        return Json(new { success = false, message = "Không nhận được dữ liệu" });
        //    }
        //}

        //load sản phẩm từ bảng GioHang lên trang giỏ hàng
        //public ActionResult loadCart()
        //{
        //    List<loadCart> ds = new List<loadCart>();
        //    string query = "select * from GioHang g join Mon m on g.ID_Mon = m.ID_Mon where ID_NgMua = @idUser";
        //    int idUser = Convert.ToInt32(Session["ID"]);
        //    decimal sum = 0;
        //    int slsp = 0;
        //    using (SqlConnection conn = new SqlConnection(conStr))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@idUser", idUser);
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            loadCart a = new loadCart();
        //            a.ID_GioHang = int.Parse(rd.GetValue(0).ToString());
        //            a.ID_Mon = int.Parse(rd.GetValue(2).ToString());
        //            a.Name = rd.GetValue(3).ToString();
        //            a.Quantity = int.Parse(rd.GetValue(4).ToString());
        //            a.Size = rd.GetValue(5).ToString();
        //            a.Sweet = rd.GetValue(6).ToString();
        //            a.Tea = rd.GetValue(7).ToString();
        //            a.Ice = rd.GetValue(8).ToString();
        //            a.Topping = rd.GetValue(9).ToString();
        //            a.ID_LoaiMonAn = int.Parse(rd.GetValue(13).ToString());
        //            a.Img = rd.GetValue(14).ToString();

        //            decimal priceNum = decimal.Parse(rd.GetValue(16).ToString());
        //            a.price = TinhTien(a.ID_GioHang, a.Size)+ priceNum;
        //            sum += a.price;
        //            ds.Add(a);
        //        }
        //    }

        //    //Tổng tiền tạm tính khi chưa cộng phí ship
        //    var tamtinh = sum;
        //    //Tổng tiền sau khi đã cộng phí ship
        //    var thanhtien = sum + 15000;

        //    ViewBag.tamtinh = tamtinh.ToString("N0");
        //    ViewBag.thanhtien = thanhtien.ToString("N0");
        //    return View(ds);
        //}


        //public decimal TinhTien(int id_giohang,string size )
        //{
        //    string querytopping = "SELECT ID_GioHang,JSONData.Name,JSONData.Quantity,t.GiaTopp FROM GioHang g CROSS APPLY  " +
        //        "OPENJSON(COALESCE(Topping, '[]'))  WITH (Name NVARCHAR(100),Quantity INT) AS JSONData join ChiTietTopping t on  JSONData.Name=t.Ten_Topping " +
        //        "where ID_GioHang=@id_giohang";
        //    decimal price_topping=0;
        //    using (SqlConnection conn1 = new SqlConnection(conStr))
        //    {
        //        conn1.Open();
        //        using (SqlCommand cmd=new SqlCommand(querytopping,conn1))
        //        {
        //            cmd.Parameters.AddWithValue("@id_giohang", id_giohang);
        //            using (SqlDataReader rd=cmd.ExecuteReader())
        //            {
        //                while (rd.Read())
        //                {
        //                    int soluong= int.Parse(rd.GetValue(2).ToString());
        //                    decimal gia= decimal.Parse(rd.GetValue(3).ToString());
        //                    price_topping += (soluong * gia);
        //                }
        //            }
        //        }
        //    }

        //    string querysize = "select GiaTang from ChiTietSize where Loai_size=@size";
        //    decimal price_size=0;
        //    using (SqlConnection conn2 = new SqlConnection(conStr))
        //    {
        //        conn2.Open();
        //        using (SqlCommand cmd = new SqlCommand(querysize, conn2))
        //        {
        //            cmd.Parameters.AddWithValue("@size", size);
        //            using (SqlDataReader rd = cmd.ExecuteReader())
        //            {
        //                while (rd.Read())
        //                {
        //                    price_size= rd.GetDecimal(0);
        //                }
        //            }
        //        }
        //    }
        //    return price_topping + price_size;
        //}


        public ActionResult mobileFilterFood()
        {
            List<Mon> ds = new List<Mon>();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                string query = "select * from Mon where ID_LoaiMonAn = 103";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Mon m = new Mon();
                    m.ID_Mon = int.Parse(rd.GetValue(0).ToString());
                    m.TenMon = rd.GetValue(1).ToString();
                    m.GiaBan = int.Parse(rd.GetValue(2).ToString());
                    m.ID_LoaiMonAn = int.Parse(rd.GetValue(3).ToString());
                    m.HinhAnh = rd.GetValue(4).ToString();
                    m.KhuyenMai = int.Parse(rd.GetValue(5).ToString());

                    var gskgNumber = int.Parse(rd.GetValue(6).ToString());
                    m.GiaSauKhiGiam = gskgNumber;
                    ds.Add(m);
                }
            }
            return View(ds);
        }

        public ActionResult mobileFilterDrink()
        {
            List<Mon> ds = new List<Mon>();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                string query = "select * from Mon where ID_LoaiMonAn = 100 or ID_LoaiMonAn = 101 or ID_LoaiMonAn = 102";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Mon m = new Mon();
                    m.ID_Mon = int.Parse(rd.GetValue(0).ToString());
                    m.TenMon = rd.GetValue(1).ToString();
                    m.GiaBan = int.Parse(rd.GetValue(2).ToString());
                    m.ID_LoaiMonAn = int.Parse(rd.GetValue(3).ToString());
                    m.HinhAnh = rd.GetValue(4).ToString();
                    m.KhuyenMai = int.Parse(rd.GetValue(5).ToString());

                    var gskgNumber = int.Parse(rd.GetValue(6).ToString());
                    m.GiaSauKhiGiam = gskgNumber;
                    ds.Add(m);
                }
            }
            return View(ds);
        }
    }
}