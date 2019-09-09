USE [SoccerInfo]
GO
/****** Object:  Table [Players]    Script Date: 06.09.2019 15:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Players](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Position] [nvarchar](50) NOT NULL,
	[TeamId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Teams]    Script Date: 06.09.2019 15:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Teams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Coach] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Players] ON 
GO
INSERT [Players] ([Id], [Name], [Age], [Position], [TeamId]) VALUES (1, N'Месси', 26, N'Нападающий', 2)
GO
INSERT [Players] ([Id], [Name], [Age], [Position], [TeamId]) VALUES (3, N'Роналду', 29, N'Нападающий', 1)
GO
INSERT [Players] ([Id], [Name], [Age], [Position], [TeamId]) VALUES (4, N'Неймар', 22, N'Нападающий', 2)
GO
INSERT [Players] ([Id], [Name], [Age], [Position], [TeamId]) VALUES (5, N'Бейл', 24, N'Полузащитник', 1)
GO
INSERT [Players] ([Id], [Name], [Age], [Position], [TeamId]) VALUES (6, N'Иньеста', 29, N'Полузащитник', 2)
GO
INSERT [Players] ([Id], [Name], [Age], [Position], [TeamId]) VALUES (7, N'Рибери', 30, N'Полузащитник', 3)
GO
SET IDENTITY_INSERT [Players] OFF
GO
SET IDENTITY_INSERT [Teams] ON 
GO
INSERT [Teams] ([Id], [Name], [Coach]) VALUES (1, N'Реал Мадрид', N'Анчелотти')
GO
INSERT [Teams] ([Id], [Name], [Coach]) VALUES (2, N'Барселона', N'Мартино')
GO
INSERT [Teams] ([Id], [Name], [Coach]) VALUES (3, N'Бавария', N'Гуардиола')
GO
INSERT [Teams] ([Id], [Name], [Coach]) VALUES (4, N'Боруссия', N'Клопп')
GO
SET IDENTITY_INSERT [Teams] OFF
GO
ALTER TABLE [Players]  WITH CHECK ADD  CONSTRAINT [FK_Players_Teams] FOREIGN KEY([TeamId])
REFERENCES [Teams] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [Players] CHECK CONSTRAINT [FK_Players_Teams]
GO
