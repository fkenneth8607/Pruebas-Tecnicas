CREATE database [TEST_ESTAGIRANET]

GO 
USE  [TEST_ESTAGIRANET]
GO
/****** Object:  Table [dbo].[CLIENTES]    Script Date: 19/09/2019 23:31:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19/09/2019 23:31:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_T_usersDelegateAuth] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VENTAS]    Script Date: 19/09/2019 23:31:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VENTAS](
	[COD_CLIENTE] [int] NULL,
	[IMPORTE] [float] NULL
) ON [PRIMARY]

GO
INSERT [dbo].[CLIENTES] ([COD_CLIENTE], [CP_CLIENTE], [NOM_CLIENTE], [LOC_CLIENTE]) VALUES (1, 28001, N'Construcciones  ADA', N'Madrid')
INSERT [dbo].[CLIENTES] ([COD_CLIENTE], [CP_CLIENTE], [NOM_CLIENTE], [LOC_CLIENTE]) VALUES (2, 41001, N'Transportes Laborda', N'Sevilla')
INSERT [dbo].[CLIENTES] ([COD_CLIENTE], [CP_CLIENTE], [NOM_CLIENTE], [LOC_CLIENTE]) VALUES (3, 17001, N'Tintorería Rápida', N'Lugo')
INSERT [dbo].[CLIENTES] ([COD_CLIENTE], [CP_CLIENTE], [NOM_CLIENTE], [LOC_CLIENTE]) VALUES (4, 50001, N'Grandes Almacenes', N'Zaragoza')
INSERT [dbo].[CLIENTES] ([COD_CLIENTE], [CP_CLIENTE], [NOM_CLIENTE], [LOC_CLIENTE]) VALUES (5, 26356, N'Diario de Navarra', N'Barcelona')
INSERT [dbo].[CLIENTES] ([COD_CLIENTE], [CP_CLIENTE], [NOM_CLIENTE], [LOC_CLIENTE]) VALUES (6, 41562, N'Arión Grupo', N'Sevilla')
INSERT [dbo].[CLIENTES] ([COD_CLIENTE], [CP_CLIENTE], [NOM_CLIENTE], [LOC_CLIENTE]) VALUES (8, 28001, N'Banco de Finanzas', N'Madrid')
INSERT [dbo].[CLIENTES] ([COD_CLIENTE], [CP_CLIENTE], [NOM_CLIENTE], [LOC_CLIENTE]) VALUES (9, 28001, N'Seguros TRI', N'Madrid')
INSERT [dbo].[CLIENTES] ([COD_CLIENTE], [CP_CLIENTE], [NOM_CLIENTE], [LOC_CLIENTE]) VALUES (10, 15852, N'Mudanzas Bello', N'Barcelona')
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [LastName], [Address], [CreatedDate], [UpdatedDate]) VALUES (1, N'Kenneth Robert', N'Fonseca Suarez', N'El Roble Sector 2b', CAST(0x0000AACC00EA05E8 AS DateTime), CAST(0x0000AACC00FD0B46 AS DateTime))
INSERT [dbo].[Users] ([Id], [Name], [LastName], [Address], [CreatedDate], [UpdatedDate]) VALUES (2, N'Jose', N'Lopez', N'el manzano', CAST(0x0000AACC00EA5AF5 AS DateTime), CAST(0x0000AACC00EA5AF5 AS DateTime))
INSERT [dbo].[Users] ([Id], [Name], [LastName], [Address], [CreatedDate], [UpdatedDate]) VALUES (3, N'Santiago', N'Fonseca', N'El Roble Sector 2B', CAST(0x0000AACC00F06F5F AS DateTime), CAST(0x0000AACC00F8FC65 AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
INSERT [dbo].[VENTAS] ([COD_CLIENTE], [IMPORTE]) VALUES (4, 0)
INSERT [dbo].[VENTAS] ([COD_CLIENTE], [IMPORTE]) VALUES (5, 0)
INSERT [dbo].[VENTAS] ([COD_CLIENTE], [IMPORTE]) VALUES (6, 0)
INSERT [dbo].[VENTAS] ([COD_CLIENTE], [IMPORTE]) VALUES (8, 0)
INSERT [dbo].[VENTAS] ([COD_CLIENTE], [IMPORTE]) VALUES (9, 768.17)
ALTER TABLE [dbo].[VENTAS]  WITH CHECK ADD  CONSTRAINT [FK_VENTAS_CLIENTES] FOREIGN KEY([COD_CLIENTE])
REFERENCES [dbo].[CLIENTES] ([COD_CLIENTE])
GO
ALTER TABLE [dbo].[VENTAS] CHECK CONSTRAINT [FK_VENTAS_CLIENTES]
GO



 

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
