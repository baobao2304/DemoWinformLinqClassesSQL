create database QuanLyHangHoa7

create table NhaCungCap(
	MaNCC char(50) not null,
	TenNCC nvarchar(50) ,
	DiaChi nvarchar(100),
	constraint PK_NhaCungCap primary key(MaNCC)

)

create table LoaiSanPham(
	MaLoaiSanPham char(50) not null,
	TenLoaiSanPham nvarchar(100) ,
	constraint PK_LoaiSanPham primary key(MaLoaiSanPham)

)
create table SanPham(
	MaSanPham char(50) not null,
	TenSanPham nvarchar(100) ,
	MaLoaiSanPham char(50) not null,
	SoLuong int,
	DonGia money,

	constraint PK_SanPham primary key(MaSanPham,MaLoaiSanPham),
	constraint FK_SanPham_LoaiSanPham foreign key(MaLoaiSanPham) references LoaiSanPham(MaLoaiSanPham),
)
create table PhieuNhap(
	MaPhieuNhap char(50) not null,
	MaNCC char(50) not null,
	NgayNhap datetime,
	ThanhTien money,

	constraint PK_PhieuNhap primary key(MaPhieuNhap,MaNCC),
	constraint FK_PhieuNhap_NhaCungCap foreign key(MaNCC) references NhaCungCap(MaNCC),
)
create table ChiTietPhieuNhap(
	MaPhieuNhap char(50) not null,
	MaSanPham char(50) not null,
	SoLuong int,
	DonGia money,

	constraint PK_ChiTietPhieuNhap primary key(MaPhieuNhap,MaSanPham),
	--	constraint FK_ChiTietPhieuNhap_PhieuNhap foreign key(MaPhieuNhap) references PhieuNhap(MaPhieuNhap),
	--constraint FK_ChiTietPhieuNhap_SanPham foreign key(MaSanPham) references SanPham(MaSanPham),

)

insert into NhaCungCap values('NCC1','ABCNCC','QƯENCC');
insert into NhaCungCap values('NCC2','DFGNCC','PO0NCC');
insert into NhaCungCap values('NCC3','OKJNCC','ỤMCC');

insert into LoaiSanPham values('Type PR 1','Ten SP 1');
insert into LoaiSanPham values('Type PR 2','Ten SP 2');
insert into LoaiSanPham values('Type PR 3','Ten SP 3');

insert into SanPham values('CODE PR 1','Name PR 1','Type PR 3',6,1231560);
insert into SanPham values('CODE PR 2','Name PR 2','Type PR 1',8,489142);
insert into SanPham values('CODE PR 3','Name PR 3','Type PR 2',9,256146);

insert into PhieuNhap values('CODE Bill 1','NCC1','6/19/2020',1231560);
insert into PhieuNhap values('CODE Bill 2','NCC3','3/5/2019',489142);
insert into PhieuNhap values('CODE Bill 3','NCC2','6/6/2018',256146);

insert into ChiTietPhieuNhap values('CODE Bill 1','CODE PR 1',1231560,6);
insert into ChiTietPhieuNhap values('CODE Bill 1','CODE PR 2',1231560,6);
insert into ChiTietPhieuNhap values('CODE Bill 1','CODE PR 3',1231560,6);
insert into ChiTietPhieuNhap values('CODE Bill 2','CODE PR 3',489142,8);
insert into ChiTietPhieuNhap values('CODE Bill 3','CODE PR 2',256146,3);
