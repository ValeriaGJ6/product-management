CREATE DATABASE ProductManagementDB
GO

USE ProductManagementDB
GO

CREATE TABLE [dbo].[Products] (
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(255) NULL,
	[Price] DECIMAL(10, 2) NOT NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
)
GO

INSERT [dbo].[Products] ([Name], [Description], [Price]) VALUES 
(N'Lenovo IdeaPad 3 (15.6")', N'Portátil versátil con procesador AMD, ideal para tareas diarias.', 1828900),
(N'Monitor Samsung 24" FHD IPS', N'Pantalla Full HD con panel IPS, ideal para oficina y entretenimiento.', 535713),
(N'Mouse Logitech G502 Hero RGB', N'Mouse gamer con 11 botones programables y retroiluminación RGB.', 205000),
(N'Teclado Corsair K95 RGB Platinum XT', N'Teclado mecánico con teclas Cherry MX Speed y retroiluminación RGB.', 1170000),
(N'Disco Duro Externo Seagate 2TB', N'Almacenamiento portátil con conexión USB 3.0.', 451900),
(N'Tarjeta Gráfica NVIDIA RTX 3060 Ti 8GB', N'Tarjeta gráfica de alto rendimiento para gaming y edición.', 2783900),
(N'Laptop HP Pavilion x360 (14")', N'Portátil 2 en 1 con pantalla táctil y procesador Intel Core i5.', 5199900),
(N'Impresora HP DeskJet Ink Advantage 2875', N'Impresora multifuncional para hogar y oficina.', 489900),
(N'Auriculares Sony WH-1000XM4', N'Auriculares con cancelación de ruido y sonido de alta calidad.', 1200000),
(N'Webcam Logitech C920 HD Pro', N'Cámara web HD con micrófono incorporado, ideal para videollamadas.', 350000),
(N'SSD Portátil NVMe 1TB – DtechCo', N'Almacenamiento rápido y portátil, ideal para profesionales y gamers.', 480000),
(N'Mouse Inalámbrico Lenovo Essential 2.4GHz', N'Mouse óptico inalámbrico de diseño ernómico.', 96800),
(N'Smartwatch Apple Watch SE 44mm GPS 32GB', N'Reloj inteligente con GPS y almacenamiento de 32GB.', 1602400),
(N'Power Bank Adata 10000mAh con Triple USB', N'Cargador portátil con múltiples puertos USB.', 97000),
(N'Combo Teclado y Mouse Logitech Pop Icon', N'Combo de teclado y mouse con diseño moderno.', 310000),
(N'Audífonos Logitech USB Headset H390', N'Diadema con micrófono y conexión USB.', 120000),
(N'Monitor Samsung LS22D310 22” FHD 75Hz', N'Pantalla plana con resolución Full HD y frecuencia de 75Hz.', 353000),
(N'Teclado Mecánico Logitech G Pro X', N'Teclado mecánico con switches intercambiables.', 1200000),
(N'Cámara Web Logitech C920 HD Pro', N'Cámara web HD con micrófono incorporado.', 350000),
(N'Smartphone ZTE nubia Music 2 – 256GB', N'Smartphone con pantalla de 6.7", 14GB de RAM y cámara de 50MP.', 500000),
(N'Teclado Mecánico Redran K552 Kumara', N'Teclado mecánico con retroiluminación LED y switches Outemu.', 180000),
(N'Audífonos Inalámbricos JBL Tune 125TWS', N'Audífonos inalámbricos con sonido JBL Pure Bass y hasta 32 horas de reproducción.', 300000),
(N'Monitor Curvo Samsung LC27G55TQWNXZA – 27"', N'Monitor curvo con resolución Full HD y tasa de refresco de 144Hz.', 1200000),
(N'Cargador Portátil Anker PowerCore 10000mAh', N'Cargador portátil compacto con tecnología PowerIQ.', 150000),
(N'Webcam Logitech C270 HD', N'Cámara web HD con micrófono integrado y enfoque automático.', 120000),
(N'Smartwatch Xiaomi Mi Band 7', N'Pulsera inteligente con monitoreo de salud y pantalla AMOLED.', 250000),
(N'Router TP-Link Archer C6 AC1200', N'Router inalámbrico de doble banda con 4 puertos LAN.', 180000),
(N'Lector de Códigos de Barras Vsupra Omnidireccional', N'Lector de códigos de barras omnidireccional para entornos comerciales.', 308500),
(N'Disco Duro Externo Seagate Expansion 2TB', N'Disco duro externo portátil con conexión USB 3.0.', 400000),
(N'Impresora Multifuncional HP DeskJet Plus 4130', N'Impresora multifuncional que permite imprimir, escanear y copiar documentos. Compatible con impresión móvil y ofrece conectividad Wi-Fi para facilitar la impresión desde dispositivos móviles.', 350000)
