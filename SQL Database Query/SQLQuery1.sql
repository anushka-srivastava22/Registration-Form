USE [userreg]
GO

-- Insert data into the [city] table for Uttar Pradesh (stateid = 1)
INSERT INTO [dbo].[city]
           ([cityid]
           ,[cityname]
           ,[stateid])
VALUES
           ('C11', 'Faridabad', '2'),
           ('C12', 'Gurgaon', '2'),
           ('C13', 'Rohtak', '2'),
           ('C14', 'Kurukshetra', '2'),
           ('C15', 'Karnal', '2');
          
GO
