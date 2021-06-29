CREATE OR REPLACE
PROCEDURE User_check_insert(
p_user_key in user_check.user_key%type,
p_Target_date in user_check.target_date%type
)

IS


BEGIN

	INSERT INTO USER_CHECK VALUES (user_check_seq.nextval, p_user_key, p_Target_Date, sysdate, null);

 commit;
END;

/