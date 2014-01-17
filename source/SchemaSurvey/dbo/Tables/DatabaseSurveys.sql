CREATE TABLE [dbo].[DatabaseSurveys]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[survey] INT NOT NULL,
	[server] VARCHAR(255),
	[database] VARCHAR(255),
	[is_reference_schema] BIT NOT NULL,
	[had_connection_error] BIT NULL,
	[had_etl_error] BIT NULL,
	[duration] BIGINT NULL,
	CONSTRAINT [PK__DatabaseSurveys] PRIMARY KEY ([id])
)
