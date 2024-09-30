

CREATE OR ALTER PROCEDURE [dbo].[STP_Quotation]
    @diferencia INT
AS
BEGIN
    -- Primera consulta: Obtener las cotizaciones creadas dentro del intervalo de tiempo especificado
    SELECT 
		c.Id,
        c.IdQuotation,
        c.QuoClient,
        c.QuoVendor,
        c.QuoCreated,
        c.QuoExpiration,
        c.QuoTotal,
        c.QuoCurrency,
	    c.Send
    FROM 
        [dbo].[CotizacionSF] c
    

    -- Segunda consulta: Obtener los detalles de las cotizaciones obtenidas en la primera consulta
    SELECT 
        d.IdQuotationDetails,
        d.IdMaster,
        d.Model,
        d.Quantity,
        d.UnitPriceIVA,
        d.Subtotal
    FROM 
        [dbo].[CotizacionSFDetalle] d
    WHERE 
        EXISTS (
            SELECT 1 
            FROM [dbo].[CotizacionSF] c
            WHERE 
                c.Id = d.IdMaster
        );
END;
GO


