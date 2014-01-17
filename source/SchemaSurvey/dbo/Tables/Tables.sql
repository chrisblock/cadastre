CREATE TABLE [dbo].[Tables]
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
	[lob_data_space_id] INT NULL,
	[filestream_data_space_id] INT NULL,
	[max_column_id_used] INT NOT NULL,
	[lock_on_bulk_load] BIT NOT NULL,
	[uses_ansi_nulls] BIT NULL,
	[is_replicated] BIT NULL,
	[has_replication_filter] BIT NULL,
	[is_merge_published] BIT NULL,
	[is_sync_tran_subscribed] BIT NULL,
	[has_unchecked_assembly_data] BIT NOT NULL,
	[text_in_row_limit] INT NULL,
	[large_value_types_out_of_row] BIT NULL,
	-- TODO: these appear in MSSQL2008
	--[is_tracked_by_cdc] BIT NULL,
	--[lock_escalation] TINYINT NULL,
	--[lock_escalation_desc] NVARCHAR(60) NULL,
	CONSTRAINT [PK__Tables] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Tables__database_survey] ON [dbo].[Tables] ([database_survey] ASC)
GO
