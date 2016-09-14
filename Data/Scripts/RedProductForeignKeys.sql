SELECT st.NAME, sc.NAME, sc.system_type_id
FROM sys.tables st
INNER JOIN sys.columns sc ON st.object_id = sc.object_id
WHERE (sc.name LIKE '%product_id%' or sc.name LIKE '%product%') AND st.NAME NOT LIKE '%_X'