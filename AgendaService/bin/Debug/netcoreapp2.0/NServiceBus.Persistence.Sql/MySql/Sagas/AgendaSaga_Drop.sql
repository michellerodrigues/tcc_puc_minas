
/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'AgendaSaga`');
set @tableNameNonQuoted = concat(@tablePrefix, 'AgendaSaga');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableNameQuoted);
prepare script from @dropTable;
execute script;
deallocate prepare script;
