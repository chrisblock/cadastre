CREATE TABLE [dbo].[Indexes]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[object_id] INT NULL,
	[name] VARCHAR(255) NULL,
	[index_id] INT NULL,
	[type] TINYINT NULL,
	[type_desc] NVARCHAR(60) NOT NULL,
	[is_unique] BIT NOT NULL,
	[data_space_id] INT NULL,
	[ignore_dup_key] BIT NOT NULL,
	[is_primary_key] BIT NOT NULL,
	[is_unique_constraint] BIT NOT NULL,
	[fill_factor] TINYINT NULL,
	[is_padded] BIT NOT NULL,
	[is_disabled] BIT NOT NULL,
	[is_hypothetical] BIT NOT NULL,
	[allow_row_locks] BIT NOT NULL,
	[allow_page_locks] BIT NOT NULL,
	-- TODO: these columns are added in MSSQL2008
	--[has_filter] BIT NOT NULL,
	--[filter_definition] NVARCHAR(MAX) NULL,
	CONSTRAINT [PK__Indexes] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Indexes__database_survey] ON [dbo].[Indexes] ([database_survey] ASC)
GO
