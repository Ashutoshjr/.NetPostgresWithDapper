# .NetPostgresWithDapper

Sample code to call postgresql stored procedure from c# using dapper.
--------------------------------------------------------------------------------------------------------------

I am refering below Sample Postgres Stored Procedure. 

CREATE OR REPLACE PROCEDURE public.sp_eventsearch(
	p_id integer,
	INOUT p_cursorsearch refcursor)
LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
    OPEN p_cursorSearch FOR
    SELECT emp_id,name,dept,salary,fiforefdate FROM employee
    WHERE emp_id = p_id;
END
$BODY$;
