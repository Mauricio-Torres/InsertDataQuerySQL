USE [Aeropuerto]
GO
/****** Object:  Table [dbo].[Avion]    Script Date: 06/10/2020 19:21:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Capacidad] [int] NOT NULL,
	[Marca] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Avion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IntinerarioAvion]    Script Date: 06/10/2020 19:21:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IntinerarioAvion](
	[IdAvion] [int] NOT NULL,
	[LugarVuelo] [varchar](max) NOT NULL,
	[TiempoSalida] [datetime] NOT NULL,
	[TiempoLLegada] [datetime] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_IntinerarioAvion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Avion] ON 

INSERT [dbo].[Avion] ([Id], [Capacidad], [Marca]) VALUES (1, 30, N'Pegasus')
INSERT [dbo].[Avion] ([Id], [Capacidad], [Marca]) VALUES (2, 40, N'NorPegasus')
SET IDENTITY_INSERT [dbo].[Avion] OFF
GO
SET IDENTITY_INSERT [dbo].[IntinerarioAvion] ON 

INSERT [dbo].[IntinerarioAvion] ([IdAvion], [LugarVuelo], [TiempoSalida], [TiempoLLegada], [Id]) VALUES (2, N'Florencia', CAST(N'2020-10-06T22:48:27.327' AS DateTime), CAST(N'2020-10-06T22:49:27.327' AS DateTime), 2)
INSERT [dbo].[IntinerarioAvion] ([IdAvion], [LugarVuelo], [TiempoSalida], [TiempoLLegada], [Id]) VALUES (1, N'Pasto', CAST(N'2020-12-12T14:00:00.000' AS DateTime), CAST(N'2020-12-12T16:00:00.000' AS DateTime), 3)
INSERT [dbo].[IntinerarioAvion] ([IdAvion], [LugarVuelo], [TiempoSalida], [TiempoLLegada], [Id]) VALUES (2, N'Pasto', CAST(N'2020-05-12T09:00:00.000' AS DateTime), CAST(N'2020-05-12T10:30:00.000' AS DateTime), 5)
SET IDENTITY_INSERT [dbo].[IntinerarioAvion] OFF
GO
ALTER TABLE [dbo].[IntinerarioAvion]  WITH CHECK ADD  CONSTRAINT [FK_IntinerarioAvion_Avion] FOREIGN KEY([IdAvion])
REFERENCES [dbo].[Avion] ([Id])
GO
ALTER TABLE [dbo].[IntinerarioAvion] CHECK CONSTRAINT [FK_IntinerarioAvion_Avion]
GO
/****** Object:  StoredProcedure [dbo].[CiudadesMasVisitadas]    Script Date: 06/10/2020 19:21:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CiudadesMasVisitadas] 

AS
BEGIN
  SELECT [LugarVuelo], count ([LugarVuelo]) as NumVisitas
  FROM [Aeropuerto].[dbo].[IntinerarioAvion]
  group by LugarVuelo
END
GO
/****** Object:  StoredProcedure [dbo].[HorasDeVuelo]    Script Date: 06/10/2020 19:21:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HorasDeVuelo]
@IdAvion int
AS
BEGIN
select sum (MinutosTranscurridos) as MinutosTranscurridos from  ( SELECT DATEDIFF(minute, [TiempoSalida], [TiempoLLegada]) as [MinutosTranscurridos]
FROM [Aeropuerto].[dbo].[IntinerarioAvion] where IdAvion = @IdAvion ) A
END
GO
/****** Object:  StoredProcedure [dbo].[MenorTiempoVuelo]    Script Date: 06/10/2020 19:21:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MenorTiempoVuelo]

AS
BEGIN

select A.IdAvion, A.marca, sum (MinutosTranscurridos) as MinutosTranscurridos
		from  ( 
			SELECT Marca, IdAvion, DATEDIFF(minute, [TiempoSalida], [TiempoLLegada]) as [MinutosTranscurridos]
			FROM [Aeropuerto].[dbo].[IntinerarioAvion] ia inner join Avion a on ia.IdAvion = a.Id
		) A group by IdAvion, Marca 

END


GO
