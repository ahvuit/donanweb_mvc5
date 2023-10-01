/*
Created		2/23/2022
Modified		2/25/2022
Project		
Model			
Company		
Author		
Version		
Database		MS SQL 2005 
*/
SET DATEFORMAT DMY
Create Database ShoesShop
on (name = 'ShoesShop', filename = 'F:\SQL_Data\ShoesShop.mdf')
log on (name = 'ShesShop_log',filename='F:\SQL_Data\ShoesShop.ldf')
go
use ShoesShop

Create table [QUYEN]
(
	[MaQuyen] int identity Primary Key,
	[TenQuyen] Char(6) NOT NULL
)
go

Create table [ACCOUNT]
(
	[IdAccount] int identity Primary Key,
	[UserName] Varchar(30) NOT NULL,
	[Password] Varchar(100) NOT NULL,
	[MaQuyen] int,
)
go
Create table [NHANVIEN]
(
	[MaNV] int identity Primary Key,
	[TenNhanVien] Nvarchar(50) NOT NULL,
	[NgaySinh] Datetime NOT NULL,
	[GioiTinh] nvarchar(5) NOT NULL,
	[CMND] Char(12) NOT NULL, UNIQUE ([CMND]),
	[DiaChi] Nvarchar(30) NOT NULL,
	[SDT] Char(11) NOT NULL,
	[HinhAnh] Text NULL,
	[IdAccount] int,
)


GO
Create table [HANGGIAY]
(
	[MaHang] int identity,
	[TenHang] Nvarchar(50) NOT NULL,
	[ThongTin] Nvarchar(100) NOT NULL,
	[Logo] Text NOT NULL,
Primary Key ([MaHang])
) 
go

Create table [DANHMUC]
(
	[MaDanhMuc] int identity Primary Key,
	[TenDanhMuc] Nvarchar(100) NOT NULL
)
go

Create table [SANPHAM]
(
	[MaSP] int identity Primary Key,
	[MaHang] int NOT NULL,
	[TenSanPham] Nvarchar(30) NOT NULL,
	[NgayCapNhat] Datetime NULL,
	[GiaBan] Integer NOT NULL,
	[HinhAnh] Text NULL,
	[SoLuongTon] Integer NULL,
	[LuotXem] Integer NULL,
	[LuotBinhChon] Integer NULL,
	[SoLuongMua] Integer NULL,
	[Moi] Bit NULL,
	[MaDanhMuc] int NOT NULL,
	[MoTa] Nvarchar(MAX),
	[UpdateBy] nvarchar(150)
) 
go

CREATE TABLE [BANGSIZE]
(
	[MaSize] int identity Primary Key,
	[MaSP] int not null,
	[s38] int,
	[s39] int,
	[s40] int,
	[s41] int,
	[s42] int,
	[s42.5] int,
	[s43] int,
	[s44] int,
	[s45] int,
	[s46] int,
	[s47] int,
	[s48] int
)


Create table [TINHTRANG]
(
	[MaTT] int identity Primary Key,
	[TinhTrangGiao] Nvarchar(100) NOT NULL,
)

go

Create table [DONDATHANG]
(
	[MaDDH] int identity Primary Key,
	[NgayDat] Datetime NULL,
	[MaTT] int,
	[NgayGiao] Datetime NULL,
	[Hoten] Nvarchar(100) NOT NULL,
	[DiaChi] Nvarchar(100) NOT NULL,
	[SDT] Nchar(15) NOT NULL,
	[Email] Nchar(50) NOT NULL,
	[GhiChu] Nvarchar(100) NULL,
	[TongTien] Decimal(18,0) NOT NULL,
	[IdAccount] int,
	[ThanhToan] bit,
)
go

create table [CHITIETDONHANG]
(
	[MaDDH] int,
	[MaSP] int,
	[SoLuong] Integer NOT NULL,
	[Size] char(7) NOT NULL,
	[DonGia] Decimal(18,0) NOT NULL,
	[TongTien] Decimal(18,0) NOT NULL,
)
go

Create table [KHUYENMAI]
(
	[MaKhuyenMai] int identity Primary Key,
	[NgayBatDau] Datetime NULL,
	[NgayKetThuc] Datetime NULL,
	[TenKhuyenMai] Nvarchar(30) NULL,
	[NgayTao] Datetime NULL,
	[NoiDungKM] Nvarchar(100) NULL,
) 
go

Create table [CHITIETKHUYENMAI]
(
	[MaKhuyenMai] int,
	[MaSP] int,
	[GiaKM] decimal NOT NULL,
	[UpdateBy] Nvarchar(150),
	CONSTRAINT PK_CTKM Primary Key ([MaKhuyenMai],[MaSP])
)
go

Create table [DOANHTHUNAM]
(
	[Nam] Char(5),
	[DoanhThu] Decimal NOT NULL,
Primary Key ([Nam])
) 
go
Create table [DOANHTHUTHANG]
(
	[Thang] Char(3),
	[Nam] Char(5),
	[SoLuongGiayBan] Integer NOT NULL,
	[DoanhThu] Decimal NOT NULL,
Primary Key ([Thang],[Nam]),
CONSTRAINT FK_DOANHTHUTHANG_DOANHTHUNAM FOREIGN KEY([NAM]) REFERENCES [DOANHTHUNAM]([NAM])
) 
go
Create table [CHITIETDOANHTHUTHANG]
(
	[MaSP] int,
	[Thang] Char(3),
	[SoLuongBan] Integer NOT NULL,
	[DoanhThu] Decimal NOT NULL,
	[Nam] Char(5),
CONSTRAINT PK_CTDOANHTHUTHANG Primary Key ([MaSP],[Thang],[Nam]),
CONSTRAINT FK_CTDOANHTHUTHANG_DOANHTHUTHANG FOREIGN KEY([THANG], [NAM]) REFERENCES DOANHTHUTHANG([THANG], [NAM]),
CONSTRAINT FK_CTDOANHTHUTHANG_SANPHAM FOREIGN KEY([MaSP]) REFERENCES [SANPHAM]([MaSP])
)
go
Create table [CHUCNANG]
(
	[QlyTaiKhoan] Bit,
	[QlySanPham] Bit,
	[QlySanPhamKM] Bit,
	[QlyDonHang] Bit,
	[ThongKe] Bit,
	[IdAccount] int NOT NULL,
Primary Key ([IdAccount])
)

go
select  Column_name 
from Information_schema.columns 
where Table_name like 'BANGSIZE'


Alter table [CHUCNANG] add  foreign key([IdAccount]) references [ACCOUNT] ([IdAccount])  on update no action on delete no action 
go
Alter table [NHANVIEN] add  foreign key([IdAccount]) references [ACCOUNT] ([IdAccount])  on update no action on delete no action 
go
Alter table [DONDATHANG] add  foreign key([IdAccount]) references [ACCOUNT] ([IdAccount])  on update no action on delete no action 
go
Alter table [DONDATHANG] add  foreign key([MaTT]) references [TINHTRANG] ([MaTT])  on update no action on delete no action 
go
Alter table [CHITIETKHUYENMAI] add  foreign key([MaSP]) references [SANPHAM] ([MaSP])  on update no action on delete no action 
go
Alter table [SANPHAM] add  foreign key([MaHang]) references [HANGGIAY] ([MaHang])  on update no action on delete no action 
go
Alter table [CHITIETKHUYENMAI] add  foreign key([MaKhuyenMai]) references [KHUYENMAI] ([MaKhuyenMai])  on update no action on delete no action 
go
Alter table [ACCOUNT] add  foreign key([MaQuyen]) references [QUYEN] ([MaQuyen])  on update no action on delete no action 
go
Alter table [SANPHAM] add  foreign key([MaDanhMuc]) references [DANHMUC] ([MaDanhMuc])  on update no action on delete no action 
go
Alter table [CHITIETDONHANG] add  foreign key([MaSP]) references [SANPHAM] ([MaSP])  on update no action on delete no action 
go
Alter table [CHITIETDONHANG] add  foreign key([MaDDH]) references [DONDATHANG] ([MaDDH])  on update no action on delete no action 
go
alter table [BANGSIZE] add foreign key([MaSP]) references [SANPHAM] ([MaSP])  on update no action on delete no action 

Set quoted_identifier on
go


Set quoted_identifier off
go

-------------------------TRIGGER------------------------------------------------
CREATE TRIGGER INSERT_BANGSIZE_WHEN_INSERT_SANPHAM
ON SANPHAM
FOR INSERT
AS
BEGIN
	DECLARE @SOLUONGTON INT = (SELECT SOLUONGTON FROM INSERTED)
	DECLARE @MASP INT = (SELECT MASP FROM inserted)
	BEGIN
		INSERT INTO BANGSIZE(MaSP) VALUES(@MASP)
	END
END
GO

create TRIGGER UPDATE_SANPHAM_WHEN_UPDATE_BANGSIZE
ON BANGSIZE
FOR UPDATE
AS
BEGIN
	DECLARE @SOLUONGTON INT = (SELECT (COALESCE(s38,0) + COALESCE(s39,0) + COALESCE(s40,0) + COALESCE(s41,0) + COALESCE(s42,0) + COALESCE([s42.5],0) + COALESCE(s43,0) + COALESCE(s44,0) + COALESCE(s45,0) + COALESCE(s46,0) + COALESCE(s47,0) + COALESCE(s48,0)) FROM INSERTED)
	DECLARE @MASP INT = (SELECT MASP FROM inserted)
	BEGIN
		UPDATE SANPHAM SET SoLuongTon = @SOLUONGTON WHERE MaSP = @MASP
	END
END
GO
SELECT (s38+s39+s40+s41+s42+[s42.5]) as soluongton from BANGSIZE where MaSP =10
go


CREATE TRIGGER UPDATE_SANPHAM_WHEN_DELETE_BANGSIZE
ON BANGSIZE
FOR DELETE
AS
BEGIN
	DECLARE @MASP INT = (SELECT MASP FROM deleted)
	BEGIN
		UPDATE SANPHAM SET SoLuongTon = 0 WHERE MaSP = @MASP
	END
END
GO


CREATE TRIGGER INSERT_SANPHAM_WHEN_INSERT_BANGSIZE
ON BANGSIZE
FOR INSERT
AS
BEGIN
	DECLARE @SOLUONGTON INT = (SELECT (COALESCE(s38,0) + COALESCE(s39,0) + COALESCE(s40,0) + COALESCE(s41,0) + COALESCE(s42,0) + COALESCE([s42.5],0) + COALESCE(s43,0) + COALESCE(s44,0) + COALESCE(s45,0) + COALESCE(s46,0) + COALESCE(s47,0) + COALESCE(s48,0) ) FROM INSERTED)
	DECLARE @MASP INT = (SELECT MASP FROM inserted)
	BEGIN
		UPDATE SANPHAM SET SoLuongTon = @SOLUONGTON WHERE MaSP = @MASP
	END
END
GO

CREATE TRIGGER UPDATE_SANPHAM_WHEN_INSERT_CHITIETDONHANG
ON CHITIETDONHANG
FOR INSERT
AS
BEGIN
	DECLARE @MASP INT = (SELECT MASP FROM inserted)
	DECLARE @SOLUONGDAT INT = (SELECT SUM(SoLuong) FROM INSERTED)
	BEGIN
		UPDATE SANPHAM SET SoLuongTon = SoLuongTon-@SOLUONGDAT WHERE MaSP = @MASP
		UPDATE SANPHAM SET SoLuongMua = SoLuongMua+@SOLUONGDAT WHERE MaSP = @MASP
	END
END
GO

CREATE TRIGGER UPDATE_BANGSIZE_WHEN_INSERT_CHITIETDONHANG
ON CHITIETDONHANG
FOR INSERT
AS
BEGIN
	DECLARE @MASP INT = (SELECT MASP FROM inserted)
	DECLARE @Size38 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '38')
	DECLARE @Size39 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '39')
	DECLARE @Size40 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '40')
	DECLARE @Size41 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '41')
	DECLARE @Size42 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '42')
	DECLARE @Size425 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '42.5')
	DECLARE @Size43 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '43')
	DECLARE @Size44 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '44')
	DECLARE @Size45 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '45')
	DECLARE @Size46 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '46')
	DECLARE @Size47 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '47')
	DECLARE @Size48 INT = (SELECT SoLuong FROM INSERTED WHERE Size like '48')
	IF(@Size38!=0)
		UPDATE BANGSIZE SET s38 = s38 - @Size38 WHERE MaSP = @MASP
	IF(@Size39!=0)
		UPDATE BANGSIZE SET s39 = s39 - @Size39 WHERE MaSP = @MASP
	IF(@Size40!=0)
		UPDATE BANGSIZE SET s40 = s40 - @Size40 WHERE MaSP = @MASP
	IF(@Size41!=0)
		UPDATE BANGSIZE SET s41 = s39 - @Size41 WHERE MaSP = @MASP
	IF(@Size42!=0)
		UPDATE BANGSIZE SET s42 = s42 - @Size42 WHERE MaSP = @MASP
	IF(@Size425!=0)
		UPDATE BANGSIZE SET [s42.5] = [s42.5] - @Size425 WHERE MaSP = @MASP
	IF(@Size43!=0)
		UPDATE BANGSIZE SET S43 = s43 - @Size43 WHERE MaSP = @MASP
	IF(@Size44!=0)
		UPDATE BANGSIZE SET s44 = s44 - @Size44 WHERE MaSP = @MASP
	IF(@Size45!=0)
		UPDATE BANGSIZE SET s45 = s45 - @Size45 WHERE MaSP = @MASP
	IF(@Size46!=0)
		UPDATE BANGSIZE SET s46 = s46 - @Size46 WHERE MaSP = @MASP
	IF(@Size47!=0)
		UPDATE BANGSIZE SET s47 = s47 - @Size47 WHERE MaSP = @MASP
	IF(@Size48!=0)
		UPDATE BANGSIZE SET s48 = s48 - @Size48 WHERE MaSP = @MASP
END
GO

CREATE TRIGGER UPDATE_DONDATHANG_WHEN_INSERT_CHITIETDONHANG
ON CHITIETDONHANG
FOR INSERT
AS
BEGIN
	DECLARE @MaDDH INT = (SELECT MaDDH FROM inserted)
	DECLARE @TONGTIEN INT = (SELECT TongTien FROM inserted)
	BEGIN
		UPDATE DONDATHANG SET TongTien = TongTien + @TONGTIEN WHERE MaDDH = @MaDDH
	END
END
GO

CREATE TRIGGER INSERT_DOANHTHUNAM_WHEN_INSERT_DONDATHANG
ON DONDATHANG
FOR INSERT
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM INSERTED)
	DECLARE @COUNT INT = (SELECT COUNT(*) FROM DOANHTHUNAM WHERE NAM=@NAM)
	IF(@COUNT=0)
	BEGIN
		INSERT INTO DOANHTHUNAM(NAM, DOANHTHU) VALUES(@NAM, 0)
	END
END
GO

CREATE TRIGGER INSERT_DOANHTHUTHANG_WHEN_INSERT_DONDATHANG
ON DONDATHANG
FOR INSERT
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM INSERTED)
	DECLARE @THANG VARCHAR(5)= (SELECT MONTH(NgayDat) FROM INSERTED)
	DECLARE @COUNT INT = (SELECT COUNT(*) FROM DOANHTHUTHANG WHERE NAM=@NAM AND THANG=@THANG)
	IF(@COUNT=0)
	BEGIN
		INSERT INTO DOANHTHUTHANG(THANG, NAM, SoLuongGiayBan, DOANHTHU) VALUES(@THANG, @NAM, 0, 0)
	END
END
GO

CREATE TRIGGER INSERT_CHITIETDOANHTHUTHANG_WHEN_INSERT_CHITIETDONHANG
ON CHITIETDONHANG
FOR INSERT
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM DONDATHANG, inserted WHERE DONDATHANG.MaDDH = inserted.MaDDH)
	DECLARE @THANG VARCHAR(5)= (SELECT MONTH(NgayDat) FROM DONDATHANG,inserted WHERE DONDATHANG.MaDDH=inserted.MaDDH)
	DECLARE @MaSP INT =(SELECT MaSP FROM inserted)
	DECLARE @SL INT = (SELECT SoLuong FROM inserted)
	DECLARE @DOANHTHU DECIMAL = (SELECT TongTien FROM inserted)
	DECLARE @COUNT INT = (SELECT COUNT(*) FROM CHITIETDOANHTHUTHANG WHERE Nam = @NAM AND @THANG = Thang AND MaSP = @MaSP)
	IF(@COUNT=0)
	BEGIN
		INSERT INTO CHITIETDOANHTHUTHANG(MaSP, Thang, SoLuongBan, DoanhThu, Nam) VALUES(@MaSP, @THANG, @SL,@DOANHTHU, @NAM)
	END
	ELSE
	BEGIN
		UPDATE CHITIETDOANHTHUTHANG SET SoLuongBan +=@SL, DoanhThu += @DOANHTHU where Nam = @NAM AND @THANG = Thang AND MaSP = @MaSP
	END
END
GO

create TRIGGER UPDATE_DOANHTHUTHANG_WHEN_INSERT_CHITIETDONHANG
ON CHITIETDONHANG
FOR insert
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM DONDATHANG, inserted WHERE DONDATHANG.MaDDH = inserted.MaDDH)
	DECLARE @THANG VARCHAR(5)= (SELECT MONTH(NgayDat) FROM DONDATHANG,inserted WHERE DONDATHANG.MaDDH=inserted.MaDDH)
	DECLARE @SL INT = (SELECT SoLuong FROM inserted)
	DECLARE @DOANHTHU DECIMAL = (SELECT TongTien FROM inserted)
	UPDATE DOANHTHUTHANG SET DoanhThu = @DOANHTHU + DoanhThu, SoLuongGiayBan = SoLuongGiayBan + @SL WHERE Nam = @NAM AND Thang = @THANG
END
GO

create TRIGGER UPDATE_DOANHTHUNAM_WHEN_INSERT_CHITIETDONHANG
ON CHITIETDONHANG
FOR insert
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM DONDATHANG, inserted WHERE DONDATHANG.MaDDH = inserted.MaDDH)
	DECLARE @THANG VARCHAR(5)= (SELECT MONTH(NgayDat) FROM DONDATHANG,inserted WHERE DONDATHANG.MaDDH=inserted.MaDDH)
	DECLARE @DOANHTHU DECIMAL = (SELECT TongTien FROM inserted)
	UPDATE DOANHTHUNAM SET DoanhThu = @DOANHTHU + DoanhThu WHERE Nam = @NAM
END
GO
---------------------------------------------------------------

CREATE TRIGGER UPDATE_DOANHTHUNAM_WHEN_UPDATE_DONDATHANG
ON DONDATHANG
FOR UPDATE
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM INSERTED)
	DECLARE @DOANHTHU INT = (SELECT TongTien FROM inserted)
	DECLARE @MATT INT = (SELECT MaTT FROM inserted)
	IF(@MATT = 5)
	BEGIN
		UPDATE DOANHTHUNAM SET DoanhThu = DoanhThu -  @DOANHTHU WHERE Nam = @NAM
	END
END
GO

CREATE TRIGGER UPDATE_DOANHTHUTHANG_WHEN_UPDATE_DONDATHANG
ON DONDATHANG
FOR UPDATE
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM INSERTED)
	DECLARE @THANG VARCHAR(5)= (SELECT MONTH(NgayDat) FROM INSERTED)
	DECLARE @DOANHTHU INT = (SELECT TongTien FROM inserted)
	DECLARE @SOLUONG INT = (SELECT SUM(CHITIETDONHANG.SoLuong) FROM inserted, CHITIETDONHANG WHERE inserted.MaDDH = CHITIETDONHANG.MaDDH)
	DECLARE @MATT INT = (SELECT MaTT FROM inserted)
	IF(@MATT = 5)
	BEGIN
		UPDATE DOANHTHUTHANG SET SoLuongGiayBan = SoLuongGiayBan - @SOLUONG, DoanhThu = DoanhThu -  @DOANHTHU WHERE Nam = @NAM AND Thang = @THANG
	END
END
GO





----------------------CHUA LAM DUOC-----------------

drop TRIGGER UPDATE_DOANHTHUTHANG_WHEN_INSERT_DONDATHANG
ON DONDATHANG
AFTER INSERT
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM INSERTED)
	DECLARE @THANG VARCHAR(5)= (SELECT MONTH(NgayDat) FROM INSERTED)
	DECLARE @MaDHH INT = (SELECT MaDDH FROM inserted)
	BEGIN
		UPDATE CHITIETDOANHTHUTHANG SET SoLuongBan = SoLuongBan 
	END
END
GO

drop TRIGGER UPDATE_CHITIETDOANHTHUTHANG_WHEN_UPDATE_DONDATHANG
ON DONDATHANG
AFTER UPDATE
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM INSERTED)
	DECLARE @THANG VARCHAR(5)= (SELECT MONTH(NgayDat) FROM INSERTED)
	DECLARE @MaDHH INT = (SELECT MaDDH FROM inserted)
	DECLARE @MaTT INT = (SELECT MaTT FROM inserted)
	IF(@MaTT = 5)
	BEGIN
		UPDATE CHITIETDOANHTHUTHANG SET SoLuongBan = SoLuongBan 
	END
END
GO

drop TRIGGER UPDATE_CHITIETDOANHTHUTHANG_WHEN_DELETE_CHITIETDONHANG
ON CHITIETDONHANG
for DELETE
AS
BEGIN
	DECLARE @NAM VARCHAR(5)= (SELECT YEAR(NgayDat) FROM DONDATHANG, deleted WHERE DONDATHANG.MaDDH = deleted.MaDDH)
	DECLARE @THANG VARCHAR(5)= (SELECT MONTH(NgayDat) FROM DONDATHANG,deleted WHERE DONDATHANG.MaDDH= deleted.MaDDH)
	DECLARE @MaSP INT =(SELECT MaSP FROM deleted)
	DECLARE @SL INT = (SELECT SoLuong FROM deleted)
	DECLARE @DOANHTHU DECIMAL = (SELECT TongTien FROM deleted)
	UPDATE CHITIETDOANHTHUTHANG SET SoLuongBan =SoLuongBan - @SL, DoanhThu =DoanhThu- @DOANHTHU where Nam = @NAM AND @THANG = Thang AND MaSP = @MaSP
END
GO

select C.MaSP from CHITIETDONHANG C, CHITIETDOANHTHUTHANG D
where C.MaSP = D.MaSP
GO