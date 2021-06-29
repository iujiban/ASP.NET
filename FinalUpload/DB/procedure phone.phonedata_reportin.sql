CREATE OR REPLACE
PROCEDURE PhoneData_ReportIn(
p_user_key phone_data.user_key%type,
p_Target_Date in user_check.Target_Date%type
)

IS

	
   vr_key user_check.uc_key%type;
   vn_key phone_data_report.phone_key%type;
   
   
   CURSOR c1 IS
   SELECT PHONE_KEY
   FROM PHONE_DATA_REPORT
   WHERE uc_key = vr_key and OFFTIME = 'Y';

BEGIN
    
	SELECT DISTINCT UC_KEY
	INTO vr_key
	FROM USER_CHECK
	WHERE USER_KEY = p_user_key 
	AND TO_CHAR(TARGET_DATE, 'YYYY-MM-dd') = P_Target_Date;

    INSERT INTO PHONE_DATA_REPORT SELECT * FROM PHONE_DATA where user_key = p_user_key and OFFTIME = 'Y'
    and uc_key = vr_key;

    OPEN c1;
    LOOP
    FETCH c1 into vn_key;
    EXIT WHEN c1%NOTFOUND;

    UPDATE PHONE_DATA_REPORT
    SET COSTCALL = durationCall * 3
    Where phone_key = vn_key;

    END LOOP;
    
    COMMIT;
END;

/