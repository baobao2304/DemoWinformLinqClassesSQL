create database QLTV

create table NXB(
	manxb char(10) not null,
	tennxb nvarchar(50),
	diachi nvarchar(50),
    constraint PK_NXB  primary key (manxb)
)

create table Sach(
	masach char(10) not null,
	tensach nvarchar(50),
	tacgia nvarchar(50),
	manxb char(10),
	soluong int ,
	 constraint PK_Sach  primary key (masach),
	 constraint FK_Sach_NXB foreign key (manxb) references NXB(manxb)
)

insert into nxb values('NXB001','Nhi Dong','SO 1 ');
insert into nxb values('NXB002','Kim Dong','SO 2 ');
insert into nxb values('NXB003','Tieu Dong','SO 3 ');

insert into Sach values('S001','Sach so 1','Nguyen Cao','NXB003');
insert into Sach values('S002','Sach so 2','Bao Pham','NXB001');
insert into Sach values('S003','Sach so 3','Pham Quyen','NXB002');