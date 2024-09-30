CREATE OR ALTER PROCEDURE [dbo].[STP_InsertQuotation]
(
 @Id INT OUTPUT,
  @IdQuotation [nvarchar](15),
  @QuoClient [nvarchar](64),	
  @QuoVendor [nvarchar](64),	
  @QuoCreated [datetime],
  @QuoExpiration [datetime],
  @QuoTotal [decimal](16,4),
  @QuoCurrency [nvarchar](15)
)
AS
BEGIN 
	INSERT INTO [dbo].[CotizacionSF](IdQuotation,QuoClient,QuoVendor,QuoCreated,QuoExpiration,QuoTotal,QuoCurrency,Send)
	VALUES(@IdQuotation,@QuoClient,@QuoVendor,@QuoCreated,@QuoExpiration,@QuoTotal,@QuoCurrency,0)
	
	SET @Id = SCOPE_IDENTITY()
END

GO



CREATE OR ALTER PROCEDURE [dbo].[STP_InsertQuotationDetail]
(
	@IdMaster [int],	
	@Model [nvarchar](256),
	@Quantity [int],
	@UnitPriceIVA [decimal](16,4),
	@Subtotal [decimal](16,4)
)
AS
BEGIN
	INSERT INTO [dbo].[CotizacionSFDetalle](IdMaster,Model,Quantity,UnitPriceIVA,Subtotal)
	VALUES(@IdMaster,@Model,@Quantity,@UnitPriceIVA,@Subtotal)
END