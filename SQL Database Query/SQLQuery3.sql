USE [userreg]
GO

-- Insert data into the [city] table for Uttar Pradesh (stateid = 1)
INSERT INTO [dbo].[city]
           ([cityid]
           ,[cityname]
           ,[stateid])
VALUES
           ('C41', 'Mumbai', '5'),
           ('C42', 'Pune', '5'),
           ('C43', 'Lonavla', '5'),
           ('C44', 'Juhu', '5'),
           ('C45', 'Shirdi', '5');
GO
