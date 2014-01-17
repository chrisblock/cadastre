CREATE TABLE [dbo].[Parameters]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[object_id] INT NOT NULL,
	[name] VARCHAR(255) NULL,
	[parameter_id] INT NOT NULL,
	[system_type_id] TINYINT NOT NULL,
	[user_type_id] INT NOT NULL,
	[max_length] SMALLINT NOT NULL,
	[precision] TINYINT NOT NULL,
	[scale] TINYINT NOT NULL,
	[is_output] BIT NOT NULL,
	[is_cursor_ref] BIT NOT NULL,
	[has_default_value] BIT NOT NULL,
	[is_xml_document] BIT NOT NULL,
	[default_value] SQL_VARIANT NULL,
	[xml_collection_id] INT NOT NULL,
	-- TODO: this columns is added in MSSQL2008
	--[is_readonly] BIT NOT NULL,
	CONSTRAINT [PK__Parameters] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Parameters__database_survey] ON [dbo].[Parameters] ([database_survey] ASC)
GO
