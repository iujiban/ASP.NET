CREATE OR REPLACE
PROCEDURE phonedata_reportInDu(
p_user_key phone_data.user_key%type,
p_Target_Date in user_check.Target_date%type
)

IS

    vr_key user_check.user_key%type;
    
    CURSOR c1 IS
    SELECT PHONE_KEY
    FROM PHONE_DATA_REPORT
    WHERE OFFTIME = 'Y' and uc_key = vr_key;
    
    vn_key phone_data_report.phone_key%type;
     
BEGIN
  	
  	SELECT DISTINCT MAX(UC_KEY)
  	INTO vr_key
  	FROM USER_CHECK
  	WHERE USER_KEY = p_user_key and TO_CHAR(TARGET_DATE, 'YYYY-MM-DD') = p_Target_Date; 
  	
    DELETE FROM PHONE_DATA_REPORT WHERE uc_key = vr_key -1;
	
	INSERT INTO PHONE_DATA_REPORT SELECT * FROM PHONE_DATA WHERE USER_KEY = p_user_key and OFFTIME = 'Y' and
	uc_key = vr_key;

    OPEN c1;
    LOOP 
    FETCH c1 INTO vn_key;
    EXIT WHEN c1%NOTFOUND;
    
    UPDATE PHONE_DATA_REPORT
    SET COSTCALL = DURATIONCALL * 3
    WHERE PHONE_KEY = VN_KEY;
    
    END LOOP;
    
    COMMIT;
END;

/