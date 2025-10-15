SET IDENTITY_INSERT [dbo].[Usuarios] ON
INSERT INTO [dbo].[Usuarios] ([Id], [Email], [RolId], [EquipoId], [Nombre], [Apellido], [Password_Valor]) VALUES (1, N'1@1.com', 1, 5, N'Valentin', N'Silveira', N'1')
INSERT INTO [dbo].[Usuarios] ([Id], [Email], [RolId], [EquipoId], [Nombre], [Apellido], [Password_Valor]) VALUES (2, N'2@1.com', 2, 6, N'X', N'X', N'1')
INSERT INTO [dbo].[Usuarios] ([Id], [Email], [RolId], [EquipoId], [Nombre], [Apellido], [Password_Valor]) VALUES (3, N'3@1.com', 3, 2, N'D', N'D', N'1')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
