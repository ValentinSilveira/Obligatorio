SET IDENTITY_INSERT [dbo].[Pagos] ON
INSERT INTO [dbo].[Pagos] ([Id], [TipoGastoId], [UsuarioId], [Metodo], [Descripcion], [Monto], [SaldoPendiente], [TipoPago], [FechaDesde], [FechaHasta], [FechaPago], [NroRecibo]) VALUES (1, 1, 1, 0, N'Mantenimiento', 10, CAST(0.00 AS Decimal(18, 2)), N'Unico', NULL, NULL, N'2025-10-09 00:00:00', N'123')
SET IDENTITY_INSERT [dbo].[Pagos] OFF
