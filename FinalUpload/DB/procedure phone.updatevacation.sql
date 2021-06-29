CREATE OR REPLACE
procedure updateVacation(
p_startVacation phone_data.receivedate%type,
p_endVacation phone_data.enddate%type,
p_type phone_data.durationcall%type,
p_email phone_data.email%type
)
IS

BEGIN
	
	IF p_type = 2 THEN
		UPDATE PHONE_DATA SET offTime = 'Y' Where TO_CHAR(RECEIVEDATE, 'yyyy-MM-dd') = p_startVacation and TO_CHAR(RECEIVEDATE, 'hh24:mm:ss') >= '08:00:00' 
		and TO_CHAR(RECEIVEDATE, 'hh24:mm:ss') <= '14:00:00' and email = p_email; 
	ELSIF p_type = 3  THEN
		UPDATE PHONE_DATA SET offTime = 'Y' WHERE TO_CHAR(RECEIVEDATE, 'yyyy-MM-dd') = p_startVacation and TO_CHAR(RECEIVEDATE, 'hh24:mm:ss') >= '14:00:00' 
		and TO_CHAR(RECEIVEDATE, 'hh24:mm:ss') <= '19:00:00' and email = p_email;
	ELSIF p_type = 1  THEN
		UPDATE PHONE_DATA SET offTime = 'Y' WHERE TO_CHAR(RECEIVEDATE, 'yyyy-MM-dd') = p_startVacation and email = p_email;
	ELSE UPDATE PHONE_DATA SET offTime = 'Y' WHERE TO_CHAR (RECEIVEDATE, 'yyyy-MM-dd') >= p_startVacation and TO_CHAR (RECEIVEDATE, 'yyyy-MM-dd') <= p_endVacation
	and email = p_email;
	END IF;
	COMMIT;
END;

/