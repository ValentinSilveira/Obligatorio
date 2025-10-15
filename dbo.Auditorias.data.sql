SET IDENTITY_INSERT [dbo].[Auditorias] ON
INSERT INTO [dbo].[Auditorias] ([Id], [Usuario], [Entidad], [Operacion], [Fecha], [Detalle]) VALUES (1, N'Desconocido', N'Gasto', N'Create', N'2025-10-09 22:00:37', N'Se creó el gasto ''Afters''')
INSERT INTO [dbo].[Auditorias] ([Id], [Usuario], [Entidad], [Operacion], [Fecha], [Detalle]) VALUES (2, N'Desconocido', N'Gasto', N'Delete', N'2025-10-09 22:00:50', N'Se eliminó el gasto '''' (ID: 2)')
INSERT INTO [dbo].[Auditorias] ([Id], [Usuario], [Entidad], [Operacion], [Fecha], [Detalle]) VALUES (3, N'Desconocido', N'Gasto', N'Update', N'2025-10-09 22:15:07', N'Se modificó el gasto ''Auto'' (ID: 1)')
INSERT INTO [dbo].[Auditorias] ([Id], [Usuario], [Entidad], [Operacion], [Fecha], [Detalle]) VALUES (4, N'2@1.com', N'Gasto', N'Create', N'2025-10-09 22:20:32', N'Se creó el gasto ''Electricista''')
INSERT INTO [dbo].[Auditorias] ([Id], [Usuario], [Entidad], [Operacion], [Fecha], [Detalle]) VALUES (5, N'2@1.com', N'Gasto', N'Update', N'2025-10-14 19:56:26', N'Se modificó el gasto ''Vehículos '' (ID: 1)')
INSERT INTO [dbo].[Auditorias] ([Id], [Usuario], [Entidad], [Operacion], [Fecha], [Detalle]) VALUES (6, N'2@1.com', N'Gasto', N'Update', N'2025-10-14 20:38:40', N'Se modificó el gasto ''Vehículos'' (ID: 1)')
INSERT INTO [dbo].[Auditorias] ([Id], [Usuario], [Entidad], [Operacion], [Fecha], [Detalle]) VALUES (7, N'2@1.com', N'Gasto', N'Create', N'2025-10-14 21:01:18', N'Se creó el gasto ''Limpieza''')
INSERT INTO [dbo].[Auditorias] ([Id], [Usuario], [Entidad], [Operacion], [Fecha], [Detalle]) VALUES (8, N'2@1.com', N'Gasto', N'Update', N'2025-10-14 21:01:36', N'Se modificó el gasto ''Limpieza General'' (ID: 5)')
INSERT INTO [dbo].[Auditorias] ([Id], [Usuario], [Entidad], [Operacion], [Fecha], [Detalle]) VALUES (9, N'2@1.com', N'Gasto', N'Delete', N'2025-10-14 21:01:44', N'Se eliminó el gasto '''' (ID: 5)')
SET IDENTITY_INSERT [dbo].[Auditorias] OFF
