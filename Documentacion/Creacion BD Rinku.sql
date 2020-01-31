--drop table movimientos
--drop table trabajadores
--drop table roles
--drop table tipos
--Creacion tabla roles
create table roles  
(
	id int not null primary key identity,
	nombre varchar(30) not null,
	salario decimal(6,2) not null,
	bono decimal(6,2) not null default 0,
	activo bit not null
);

--creacion tabla tipos
create table tipos  --Agregar porcentaje de vales
(
	id int not null primary key identity,
	nombre varchar(15),
	porcentajevale decimal (4,2) not null default 0,
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
	idrol int not null foreign key references roles(id), 
	entregas int not null default 0
);
go

create function Fn_DameISR (@importe decimal(10,2))
returns decimal(10,2)
as
begin
	return iif(@importe <16000,@importe/100*9,@importe/100*12)
end
go
-- select dbo.Fn_DameISR(20000)

--Crear Store Procedure que realizara el reporte
alter procedure store_nominaMensual @mes int
as
begin

	if OBJECT_ID('tempdb..#nomina') is not null
	begin
		drop table #nomina
	end

	create table #nomina 
	(
	--informacion peproceso
		idtrabajador int,
		nombre varchar(100),
		tipo int,
		rol int,
		salario decimal(10,2),
		bono decimal(10,2),
		porcentajevale decimal(10,2),
		dias_trabajados int,
		entregas int,
	--informacion posproceso
		Isalario decimal(10,2),
		Ibonos decimal(10,2),
		Ientregas decimal(10,2),
		Ivales decimal(10,2),
		ISR decimal(10,2),
		Neto decimal(10,2)
	)

	insert into #nomina (idtrabajador,nombre,tipo,rol, salario,bono,porcentajevale,  dias_trabajados,entregas)
	select t.id,t.nombre, t.idtipo, m.idrol, r.salario, r.bono,tp.porcentajevale,  count(*) as dias_trabajados, sum(m.entregas ) as 'entregasMes'
	from movimientos m	inner join trabajadores t on (m.idtrabajador = t.id) 
						inner join roles		r on (m.idrol=r.id)
						inner join tipos		tp on (t.idtipo=tp.id)
	where month(m.fecha)=@mes
	group by t.id,t.nombre, t.idtipo, m.idrol, r.salario, r.bono,tp.porcentajevale
	

	update #nomina 
	set Isalario	= salario*dias_trabajados*8,
		Ibonos		= bono*dias_trabajados*8,
		Ientregas	= entregas*5

	update #nomina 
	set Ivales =IIF(porcentajevale =0,0,(Isalario+Ibonos+Ientregas)*porcentajevale)/100,
		ISR = dbo.Fn_DameISR(Isalario+Ibonos+Ientregas)
		
	update #nomina 
	set Neto = Isalario+Ibonos+Ientregas -ISR

	select n.idtrabajador, n.nombre, 	sum( Isalario  ) as [$Salario],	sum( Ibonos  ) as [$bono],	sum( Ientregas  ) as [$Entregas],	sum( Ivales  ) as [$Vales],	sum( ISR  ) as [$ISR],	sum(neto) as [$Neto]
	from #nomina n
	group by n.idtrabajador, n.nombre

		if OBJECT_ID('tempdb..#nomina') is not null
	begin
		drop table #nomina
	end

end
--Ejecucion de prueba
-- exec store_nominaMensual 1
insert into tipos values('interno',4,1)
insert into tipos values('externo',0,1)

insert into roles values('chofer',30,10,1)
insert into roles values('cargador',30,5,1)
insert into roles values('auxiliar',30,0,1)

insert into trabajadores values('juan',1,1,1)
insert into trabajadores values('jose',2,1,1)
insert into trabajadores values('jesus',3,1,1)
 
 set dateformat YMD
 insert into movimientos values(1,'2020/01/28',1,20)
 insert into movimientos values(2,'2020/01/28',2,20)
 insert into movimientos values(3,'2020/01/28',3,20)
 insert into movimientos values(3,'2020/01/28',1,20)

 select * from movimientos
