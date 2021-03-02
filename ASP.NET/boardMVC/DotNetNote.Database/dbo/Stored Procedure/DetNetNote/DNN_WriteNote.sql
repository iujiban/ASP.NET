Create Procedure dbo.WriteNote
@Name NVarChar(25),
@Email NVarChar(100),
@Title NVarchar(150),
@PostIp NVarchar(15),
@Content NText,
@Password NVarChar(20),
@Encoding NVarChar(10),
@Homepage NVarChar(100),
@FileName NVarChar(255),
@FileSize Int
As 
Declare @MaxRef Int
Select @MaxRef = Max(Ref) From Notes

If @MaxRef is Null
Set @MaxRef = 1 -- 처음 비교
Else
Set @MaxRef = @MaxRef + 1

Insert Notes
(
	Name, Email, Title, PostIp, Content, Password, Encoding, Homepage,Ref, FileName, Filesize
) Values
(
	@Name, @Email, @Title, @PostIp, @Content, @Password, @Encoding, @Homepage, @MaxRef, @FileName, @FileSize
)
Go