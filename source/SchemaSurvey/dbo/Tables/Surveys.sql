CREATE TABLE [dbo].[Surveys]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[name] VARCHAR(255) NOT NULL,
	[user_name] VARCHAR(255) NOT NULL,
	[machine_name] VARCHAR(255) NOT NULL,
	[start_time] DATETIME NOT NULL,
	[end_time] DATETIME NULL,
	CONSTRAINT [PK__Surveys] PRIMARY KEY ([id])
)
