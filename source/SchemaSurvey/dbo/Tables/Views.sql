CREATE TABLE [dbo].[Views]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[name] VARCHAR(255) NOT NULL,
	[object_id] INT NOT NULL,
	[principal_id] INT NULL,
	[schema_id] INT NOT NULL,
	[parent_object_id] INT NOT NULL,
	[type] CHAR(2) NOT NULL,
	[type_desc] NVARCHAR(60) NULL,
	[create_date] DATETIME NOT NULL,
	[modify_date] DATETIME NOT NULL,
	[is_ms_shipped] BIT NOT NULL,
	[is_published] BIT NOT NULL,
	[is_schema_published] BIT NOT NULL,
	[is_replicated] BIT NULL,
	[has_replication_filter] BIT NULL,
	[has_opaque_metadata] BIT NOT NULL,
	[has_unchecked_assembly_data] BIT NOT NULL,
	[with_check_option] BIT NOT NULL,
	[is_date_correlation_view] BIT NOT NULL,
	-- TODO: this columns is added in MSSQL2008
	--[is_tracked_by_cdc] BIT NULL,
	CONSTRAINT [PK__Views] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Views__database_survey] ON [dbo].[Views] ([database_survey] ASC)
GO
