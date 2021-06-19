USE [invoicedb]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 6/19/2021 11:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[Id] [varchar](36) NOT NULL,
	[CustomerName] [varchar](225) NOT NULL,
	[CustomerAddress] [varchar](225) NULL,
	[CUstomerLogo] [varchar](225) NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_customers_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[invoiceDetails]    Script Date: 6/19/2021 11:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoiceDetails](
	[Id] [varchar](36) NOT NULL,
	[InvoiceId] [varchar](36) NOT NULL,
	[UomId] [varchar](36) NOT NULL,
	[InvoiceDetailName] [text] NOT NULL,
	[UomName] [varchar](225) NOT NULL,
	[InvoiceDetailRate] [decimal](18, 2) NOT NULL,
	[InvoiceDetailQty] [decimal](18, 2) NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NOT NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_invoiceDetails_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[invoices]    Script Date: 6/19/2021 11:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoices](
	[Id] [varchar](36) NOT NULL,
	[PurchaseOrderId] [varchar](36) NOT NULL,
	[CustomerId] [varchar](36) NOT NULL,
	[CurrencyId] [varchar](36) NOT NULL,
	[LanguageId] [varchar](36) NOT NULL,
	[PurchaseOrderNumber] [varchar](36) NOT NULL,
	[CustomerName] [varchar](225) NULL,
	[InvoiceNumber] [varchar](36) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[InvoiceDue] [datetime] NOT NULL,
	[InvoiceFrom] [varchar](225) NULL,
	[InvoiceDisc] [decimal](18, 2) NOT NULL,
	[InvoiceStatus] [varchar](225) NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NOT NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Invoices_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[purchaseOrders]    Script Date: 6/19/2021 11:37:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchaseOrders](
	[Id] [varchar](36) NOT NULL,
	[PurchaseOrderNumber] [varchar](36) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PIC] [varchar](225) NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_purchaseOrders_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[staticDatas]    Script Date: 6/19/2021 11:37:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[staticDatas](
	[Id] [varchar](36) NOT NULL,
	[StaticCode] [varchar](225) NOT NULL,
	[StaticName] [varchar](225) NOT NULL,
	[StaticGroup] [varchar](225) NULL,
 CONSTRAINT [PK_staticDatas_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[invoiceDetails] ADD  DEFAULT ((0)) FOR [InvoiceDetailRate]
GO
ALTER TABLE [dbo].[invoiceDetails] ADD  DEFAULT ((0)) FOR [InvoiceDetailQty]
GO
ALTER TABLE [dbo].[invoices] ADD  DEFAULT (getdate()) FOR [InvoiceDate]
GO
ALTER TABLE [dbo].[invoices] ADD  DEFAULT (getdate()) FOR [InvoiceDue]
GO
ALTER TABLE [dbo].[invoices] ADD  DEFAULT ((0)) FOR [InvoiceDisc]
GO
ALTER TABLE [dbo].[invoices] ADD  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[invoices] ADD  DEFAULT (getdate()) FOR [EditDate]
GO
ALTER TABLE [dbo].[purchaseOrders] ADD  DEFAULT ((0)) FOR [Amount]
GO
ALTER TABLE [dbo].[invoiceDetails]  WITH CHECK ADD FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[invoices] ([Id])
GO
ALTER TABLE [dbo].[invoiceDetails]  WITH CHECK ADD FOREIGN KEY([UomId])
REFERENCES [dbo].[staticDatas] ([Id])
GO
ALTER TABLE [dbo].[invoices]  WITH CHECK ADD FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[staticDatas] ([Id])
GO
ALTER TABLE [dbo].[invoices]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[customers] ([Id])
GO
ALTER TABLE [dbo].[invoices]  WITH CHECK ADD FOREIGN KEY([LanguageId])
REFERENCES [dbo].[staticDatas] ([Id])
GO
ALTER TABLE [dbo].[invoices]  WITH CHECK ADD FOREIGN KEY([PurchaseOrderId])
REFERENCES [dbo].[purchaseOrders] ([Id])
GO