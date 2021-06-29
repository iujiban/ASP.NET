CREATE OR REPLACE
procedure SaveData (
p_user_key in phone_data.user_key%type,
p_name in phone_Data.recipient%type,
p_PhoneNumber in phone_data.RNumber%type,
p_receiveDate in phone_data.receiveDate%type,
p_enddate in phone_data.enddate%type,
p_offTime in phone_Data.offTime%type,
p_durationcall in phone_data.durationcall%type,
p_CallStatus in phone_data.callstatus%type,
p_resultCall in phone_data.resultCall%type,
p_businessName in phone_data.businessname%type,
p_serviceLevel in phone_data.servicelevel%type,
p_TargetDate in phone_data.receiveDate%type,
p_email in phone_data.email%type
)
IS
  vn_ucKey user_check.uc_key%type;

  CURSOR c1 IS
  SELECT PHONE_KEY
  FROM PHONE_DATA
  WHERE  TO_CHAR(RECEIVEDATE, 'hh24:mm:ss') >= '19:00:00'
  or TO_CHAR(RECEIVEDATE, 'hh24:mm:ss') <= '08:00:00'
  and uc_key = vn_ucKey;

  vn_key phone_data.uc_key%type;
  
  vn_Date vacation.vacationDate%type;
  	
  CURSOR c2 IS
  SELECT VACATIONDATE
  FROM VACATION;
  
BEGIN
     
	SELECT UC_KEY
	INTO vn_ucKey
	FROM USER_CHECK
	WHERE USER_KEY = p_user_key and TO_CHAR(Target_Date, 'yyyy-MM-dd') = p_TargetDate;
	
	IF p_CallStatus = '수신'  THEN
		INSERT INTO PHONE_DATA (Phone_key, User_key, uc_key, recipient, rnumber, caller, cnumber, businessname, servicelevel, receivedate, enddate, offtime, durationcall,  callstatus, resultcall, etc_1, etc_2, etc_3, etc_4, email, note)
					values (phone_seq.nextval, p_user_key, vn_ucKey, p_name, p_PhoneNumber, null, null, p_businessName, p_serviceLevel, p_receivedate, p_enddate, p_offTime, p_durationcall, p_callstatus, p_resultcall, null, null, null, null, p_email, null);
		
	ELSIF p_CallStatus = '발신' THEN
		INSERT INTO PHONE_DATA (Phone_key, User_key, uc_key, recipient, rnumber, caller, cnumber, businessname, servicelevel, receivedate, enddate, offtime, durationcall,  callstatus, resultcall, etc_1, etc_2, etc_3, etc_4, email, note)
					values (phone_seq.nextval, p_user_key, vn_ucKey, null, null, p_name, p_PhoneNumber, p_businessName, p_serviceLevel, p_receivedate, p_enddate, p_offtime, p_durationcall,  p_callstatus, p_resultcall, null, null, null, null, p_email, null);
	ELSE
		INSERT INTO PHONE_DATA (Phone_key, User_key, uc_key, recipient, rnumber, caller, cnumber, businessname, servicelevel, receivedate, enddate, offtime, durationcall,  callstatus, resultcall, etc_1, etc_2, etc_3, etc_4, email, note)
					values (phone_seq.nextval, p_user_key, vn_ucKey, null, null, null, null, null, null, p_receivedate, p_enddate, p_offtime, p_durationcall, p_callstatus, p_resultcall, null, null, null, null, p_email, null);
	END IF;

	OPEN c1;
	LOOP
	FETCH c1 into vn_key;
	EXIT WHEN c1%NOTFOUND;

	DBMS_OUTPUT.PUT_LINE('uc_key'|| vn_key);

	UPDATE phone_data
	SET offTime = 'Y'
	Where phone_key = vn_key;

	END LOOP;
	
	OPEN c2;
	LOOP 
	FETCH c2 into vn_Date;
	EXIT WHEN c2%NOTFOUND;
	
	UPDATE Phone_data
	Set offTime = 'Y'
	Where TO_CHAR(RECEIVEDATE, 'yyyy-MM-dd') = vn_Date;
	END LOOP;	 	
	
	Commit;
END;

/