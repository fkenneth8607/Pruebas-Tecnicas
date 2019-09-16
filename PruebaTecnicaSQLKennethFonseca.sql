 --CREAMOS LA TABLA CLIENTES
 IF (OBJECT_ID(N'[CLIENTES]') IS NULL) 
BEGIN

 CREATE TABLE [dbo].[CLIENTES](
	[COD_CLIENTE] [int] NOT NULL,
	[CP_CLIENTE] [int] NULL,
	[NOM_CLIENTE] [varchar](50) NULL,
	[LOC_CLIENTE] [varchar](500) NULL,
 CONSTRAINT [PK_CLIENTES] PRIMARY KEY CLUSTERED 
(
	[COD_CLIENTE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

 
END
 
  --CREAMOS LA TABLA VENTAS\

   IF (OBJECT_ID(N'[VENTAS]') IS NULL) 
BEGIN
 CREATE TABLE [dbo].[VENTAS](
	[COD_CLIENTE] [int] NULL,
	[IMPORTE] [float] NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[VENTAS]  WITH CHECK ADD  CONSTRAINT [FK_VENTAS_CLIENTES] FOREIGN KEY([COD_CLIENTE])
REFERENCES [dbo].[CLIENTES] ([COD_CLIENTE])


ALTER TABLE [dbo].[VENTAS] CHECK CONSTRAINT [FK_VENTAS_CLIENTES]
 

END


 
 --INSERTAMOS EN LA TABLA CLIENTES
	  DELETE FROM VENTAS -- BORRAMOS DE VENTAS POR RELACION FK
	  DELETE FROM CLIENTES
 
	 INSERT INTO CLIENTES (COD_CLIENTE, NOM_CLIENTE,CP_CLIENTE,LOC_CLIENTE) VALUES 
	('1', 'Construcciones  ADA',28001, 'Madrid'),
	('2', 'Transportes Laborda' ,41001, 'Sevilla'),
	('3', 'Tintorería Rápida', 17001, 'Lugo'),
	('4', 'Grandes Almacenes', 50001, 'Zaragoza'),
	('5', 'Diario de Navarra', 26356, 'Barcelona'),
	('6', 'Arión Grupo', 41562, 'Sevilla'),
	('8', 'Banco de Finanzas', 28500, 'Madrid'),
	('9', 'Seguros TRI', 41200, 'Madrid'),
	('10', 'Mudanzas Bello', 15852, 'Barcelona')

 --INSERTAMOS EN LA TABLA VENTAS
	 DELETE FROM VENTAS

	INSERT INTO VENTAS (COD_CLIENTE, IMPORTE) VALUES

	(4 ,CONVERT(float,'406.78')),
	(5 ,CONVERT(float,'807.84')),
	(6 ,CONVERT(float,'27.95')),
	(8 ,CONVERT(float,'666.28')),
	(9 ,CONVERT(float,'768.17'))

 --1.- Seleccionar todos los clientes ordenados por nombre del cliente

 SELECT *FROM CLIENTES ORDER BY NOM_CLIENTE;

 --2.- Seleccionar los clientes cuya localidad sea Madrid
  
  SELECT *FROM CLIENTES WHERE LOC_CLIENTE='Madrid';

 --3.- Seleccionar todos los clientes cuyo código postal pertenezca a la provincia de Sevilla (los que comienzan por 41)

  SELECT *FROM CLIENTES WHERE CP_CLIENTE LIKE '41%';

 --4.- Seleccionar todos los clientes cuyo cósigo sea inferior a 10 o cuyo nombre de cliente comience por la letra A
 
  SELECT *FROM CLIENTES WHERE (COD_CLIENTE< 10 OR NOM_CLIENTE LIKE 'A%');

--5.- Contar cuántos clientes existen por cada localidad

 SELECT LOC_CLIENTE, COUNT(*) Cantidad FROM CLIENTES GROUP BY LOC_CLIENTE;

 --6.- Borrar los clientes cuyo nombre comience por la letra Z y termine por la letra Z

 DELETE FROM CLIENTES WHERE NOM_CLIENTE LIKE  'Z%' OR NOM_CLIENTE LIKE '%Z'

 --7.- Modificar el valor del código postal de los clientes cuya localidad sea Madrid con el valor 28001

  UPDATE CLIENTES SET CP_CLIENTE=28001  WHERE LOC_CLIENTE='Madrid';

 --8.- Seleccionar los clientes cuyo importe de sus VENTAS sea distinto de cero.

 SELECT C.* FROM CLIENTES C INNER JOIN VENTAS V ON V.COD_CLIENTE= C.COD_CLIENTE WHERE V.IMPORTE<>0;

-- 9.- Seleccionar todos los clientes junto con el importe de las ventas de cada uno.

 SELECT C.COD_CLIENTE, C.NOM_CLIENTE, V.IMPORTE FROM CLIENTES C INNER JOIN VENTAS V ON V.COD_CLIENTE= C.COD_CLIENTE;

--10.- Modificar el importe de las ventas de los clientes a cero para aquellos clientes cuyo nombre contenga al menos una letra A.

UPDATE VENTAS  SET  IMPORTE= 0 WHERE COD_CLIENTE IN (SELECT COD_CLIENTE FROM CLIENTES WHERE NOM_CLIENTE LIKE '%A%');