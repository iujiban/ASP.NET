CREATE OR REPLACE
procedure User_check_DelAUpdate(
p_user_key in user_check.user_key%type,
p_Target_Date in user_check.Target_date%type
)
Is
 CURSOR c1 IS
 SELECT DISTINCT uc_key
 FROM USER_CHECK
 WHERE USER_KEY = P_USER_KEY AND TARGET_DATE = P_TARGET_DATE;

 vr_key user_check.user_key%type;
BEGIN

 Open c1;
 LOOP
 	FETCH c1 INTO vr_key;

 	EXIT WHEN c1%NOTFOUND;

 Delete from phone_data where uc_key = vr_key;

 UPDATE USER_CHECK SET DEL_DATE = sysdate WHERE UC_KEY = vr_key;

 END LOOP;
 
 INSERT INTO USER_CHECK VALUES (USER_CHECK_SEQ.NEXTVAL, p_user_key, p_Target_date, sysdate, null);
 
 COMMIT;	
END;

/