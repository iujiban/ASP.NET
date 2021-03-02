Create Proc dbo.ReplyNote
@Name NVarChar(25),
@Email NVarChar(100),
@Title NVarChar(150),
@PostIp NVarChar(15),
@Content NText,
@Password NVarChar(20),
@Encoding NVarChar(10),
@Homepage NVarChar(100),
@ParentNum Int, --부모글 고유번호
@FileName NVarChar(255),
@FileSize Int

As
--변수 선언
Declare @MaxRefOrder Int
Declare @MaxRefAnswerNum Int
Declare @ParentRef Int
Declare @ParentStep Int
Declare @ParentRefOrder Int

-- 부모글의 답변수 (AnswerNum)를 1 증가
Update Notes Set AnswerNum = AnswerNum + 1 where Id = @ParentNum

-- 같은 글에 대해서 답변을 두 번 이상하면 먼저 답변한 게 위에 나타나게 한다.
Select @MaxRefOrder = RefOrder, @MaxRefAnswerNum = AnswerNum From Notes
Where ParentNum = @ParentNum And RefOrder = (Select Max(RefOrder) From Notes Where ParentNum = @ParentNum)

If @MaxRefOrder Is Null
Begin 
Select @MaxRefOrder = RefOrder From Notes Where Id = @ParentNum
Set @MaxRefAnswerNum = 0
End

Update Notes
Set 
RefOrder = RefOrder +1
Where Ref = @ParentRef And RefOrder > (@MaxRefOrder + @MaxRefAnswerNum)

Insert Notes (
Name, Email, Title, PostIp, Content, Password, Encoding, Homepage, Ref, Step, RefOrder, ParentNum, FileName, FileSize
) Values (
@Name, @Email, @Title, @PostIp, @Content, @Password, @Encoding, @Homepage, @ParentRef, @ParentStep + 1, @MaxRefOrder + @MaxRefOrder + @MaxRefAnswerNum +1, @ParentNum, @FileName, @FileSize
)
Go
