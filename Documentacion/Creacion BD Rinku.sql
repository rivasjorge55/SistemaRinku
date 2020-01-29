--Creacion tabla roles
create table roles
(
	id int not null primary key identity,
	nombre varchar(30) not null,
	activo bit not null
);

--creacion tabla tipos
create table tipos
(
	id int not null primary key identity,
	nombre varchar(15),
	activo bit not null
);

--Creacion Tabla trabajadores
Create table trabajadores
(
	id int not null primary key identity,
	nombre varchar(100) not null,
	idrol int not null foreign key references roles(id),
	idtipo int not null foreign key references tipos(id),
	activo bit not null
);

--creacion tabla movimientos

create table movimientos 
(
	id int not null primary key identity,
	idtrabajador int not null foreign key references trabajadores(id),
	fecha date not null,
	idrolpropio int not null foreign key references roles(id), --CAMPO informativo, en prevision de que los empleados puedan cambiar de rol en un futuro
	idrolejecutado int not null foreign key references roles(id), --Este es el que se empleara para hacer el calculo de pago
	entregas int not null default 0
);
go

--Crear Store Procedure que realizara el reporte
create procedure store_nominaMensual @mes int
as
begin
	select 'Este Store Implementara la Logica de negocia del pago de los trabajadores'
end
