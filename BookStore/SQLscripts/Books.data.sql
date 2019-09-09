USE [Books]
GO
/****** Object:  Table [Books]    Script Date: 06.09.2019 15:16:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Author] [nvarchar](max) NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Purchases]    Script Date: 06.09.2019 15:16:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Purchases](
	[PurchaseId] [int] IDENTITY(1,1) NOT NULL,
	[Person] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[BookId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Purchases] PRIMARY KEY CLUSTERED 
(
	[PurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [Books] ON 
GO
INSERT [Books] ([Id], [Name], [Author], [Price]) VALUES (1, N'Война и мир', N'Л.Н. Толстой', 260)
GO
INSERT [Books] ([Id], [Name], [Author], [Price]) VALUES (2, N'Отцы и дети', N'И. Тургенев', 180)
GO
INSERT [Books] ([Id], [Name], [Author], [Price]) VALUES (3, N'Чайка', N'А. Чехов', 150)
GO
INSERT [Books] ([Id], [Name], [Author], [Price]) VALUES (4, N'Когда проснётся Марс', N'В. Огнева', 350)
GO
SET IDENTITY_INSERT [Books] OFF
GO
SET IDENTITY_INSERT [Purchases] ON 
GO
INSERT [Purchases] ([PurchaseId], [Person], [Address], [BookId], [Date]) VALUES (1, N'фффффффффф', N'яяяяя', 1, CAST(N'2019-09-05T16:24:35.430' AS DateTime))
GO
INSERT [Purchases] ([PurchaseId], [Person], [Address], [BookId], [Date]) VALUES (2, N'Ввввв', N'вава вава', 1, CAST(N'2019-09-06T10:37:27.990' AS DateTime))
GO
INSERT [Purchases] ([PurchaseId], [Person], [Address], [BookId], [Date]) VALUES (3, N'Www123', N'www124', 4, CAST(N'2019-09-06T11:18:42.840' AS DateTime))
GO
SET IDENTITY_INSERT [Purchases] OFF
GO
