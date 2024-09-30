CREATE TABLE [dbo].[CotizacionSF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdQuotation] [nvarchar](15) NOT NULL,
	[QuoClient] [nvarchar](64) NOT NULL,
	[QuoVendor] [nvarchar](64) NOT NULL,
	[QuoCreated] [datetime] NOT NULL,
	[QuoExpiration] [datetime] NOT NULL,
	[QuoTotal] [decimal](16, 4) NOT NULL,
	[QuoCurrency] [nvarchar](64) NOT NULL,
	[Send] [bit] DEFAULT 0
 CONSTRAINT [PK_CotizacionSF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[CotizacionSFDetalle](
	[IdQuotationDetails] [int] IDENTITY(1,1) NOT NULL,
	[IdMaster] [int] NOT NULL,
	[Model] [nvarchar](256) NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPriceIVA] [decimal](16, 4) NOT NULL,
	[Subtotal] [decimal](16, 4) NOT NULL,
 CONSTRAINT [PK_CotizacionSFDetalle] PRIMARY KEY CLUSTERED 
(
	[IdQuotationDetails] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


<<<<<<< HEAD
select * from [CotizacionSF]
select * from [CotizacionSFDetalle]
=======
>>>>>>> 50a97b7 (no message)
