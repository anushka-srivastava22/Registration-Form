USE [userreg]
GO

-- Update data in the [state] table
UPDATE [dbo].[state]
SET 
    [stateid] = '1',
    [statename] = 'Uttar Pradesh'
WHERE
    [stateid] = '1' AND [statename] = 'UP';
GO


