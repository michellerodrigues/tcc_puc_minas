
/* TableNameVariable */

declare @tableName nvarchar(max) = '[' + @schema + '].[' + @tablePrefix + N'AgendaSaga]';
declare @tableNameWithoutSchema nvarchar(max) = @tablePrefix + N'AgendaSaga';


/* Initialize */

/* CreateTable */

if not exists
(
    select *
    from sys.objects
    where
        object_id = object_id(@tableName) and
        type in ('U')
)
begin
declare @createTable nvarchar(max);
set @createTable = '
    create table ' + @tableName + '(
        Id uniqueidentifier not null primary key,
        Metadata nvarchar(max) not null,
        Data nvarchar(max) not null,
        PersistenceVersion varchar(23) not null,
        SagaTypeVersion varchar(23) not null,
        Concurrency int not null
    )
';
exec(@createTable);
end

/* AddProperty AgendaId */

if not exists
(
  select * from sys.columns
  where
    name = N'Correlation_AgendaId' and
    object_id = object_id(@tableName)
)
begin
  declare @createColumn_AgendaId nvarchar(max);
  set @createColumn_AgendaId = '
  alter table ' + @tableName + N'
    add Correlation_AgendaId uniqueidentifier;';
  exec(@createColumn_AgendaId);
end

/* VerifyColumnType Guid */

declare @dataType_AgendaId nvarchar(max);
set @dataType_AgendaId = (
  select data_type
  from information_schema.columns
  where
    table_name = @tableNameWithoutSchema and
    table_schema = @schema and
    column_name = 'Correlation_AgendaId'
);
if (@dataType_AgendaId <> 'uniqueidentifier')
  begin
    declare @error_AgendaId nvarchar(max) = N'Incorrect data type for Correlation_AgendaId. Expected uniqueidentifier got ' + @dataType_AgendaId + '.';
    throw 50000, @error_AgendaId, 0
  end

/* WriteCreateIndex AgendaId */

if not exists
(
    select *
    from sys.indexes
    where
        name = N'Index_Correlation_AgendaId' and
        object_id = object_id(@tableName)
)
begin
  declare @createIndex_AgendaId nvarchar(max);
  set @createIndex_AgendaId = N'
  create unique index Index_Correlation_AgendaId
  on ' + @tableName + N'(Correlation_AgendaId)
  where Correlation_AgendaId is not null;';
  exec(@createIndex_AgendaId);
end

/* PurgeObsoleteIndex */

declare @dropIndexQuery nvarchar(max);
select @dropIndexQuery =
(
    select 'drop index ' + name + ' on ' + @tableName + ';'
    from sysindexes
    where
        Id = object_id(@tableName) and
        Name is not null and
        Name like 'Index_Correlation_%' and
        Name <> N'Index_Correlation_AgendaId'
);
exec sp_executesql @dropIndexQuery

/* PurgeObsoleteProperties */

declare @dropPropertiesQuery nvarchar(max);
select @dropPropertiesQuery =
(
    select 'alter table ' + @tableName + ' drop column ' + column_name + ';'
    from information_schema.columns
    where
        table_name = @tableNameWithoutSchema and
        table_schema = @schema and
        column_name like 'Correlation_%' and
        column_name <> N'Correlation_AgendaId'
);
exec sp_executesql @dropPropertiesQuery

/* CompleteSagaScript */
