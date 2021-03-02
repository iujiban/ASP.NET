Create Procedure dbo.DeleteNote
@Id Int,
@Password NVarChar(30) --암호 매개 변수 추가
As
Declare @cnt Int
Select @cnt = Count(*) From Notes
Where Id = @Id And Password = @Password

If @cnt = 0
Begin 
Return 0 -- 번호와 암호가 맞는 게 없으면 0을 반환
End

Declare @AnswerNum Int
Declare @RefOrder Int
Declare @Ref Int
Declare @ParentNum Int

Select 
@AnswerNum = AnswerNum, @RefOrder = RefOrder,
@Ref= Ref, @ParentNum = ParentNum
From Notes
WHere Id = @Id

If @AnswerNum = 0
Begin 
If @RefOrder >0
Begin 
Update Notes SET RefOrder = RefOrder -1
Where Ref  = @Ref AND RefOrder > @RefOrder
UPDATE Notes SET AnswerNum = AnswerNum -1 WHERE Id = @ParentNum
End

Delete Notes where Id = @Id
Delete Notes
Where Id = @ParentNum AND ModifyIp = N'((DELETED))' AND AnswerNum = 0
END
Else
Begin
Update Notes
Set
Name = N'(Unknown)', Email = '', Password = '',
Title = N'(삭제된 글입니다)', Content = N'(삭제된 글입니다. ' + N'현재 답변이 포함되어 있기 때문에 내용만 삭제되었습니다.)',
ModifyIp = N'((DELETED))', FileName = '',
FileSize = 0
Where Id = @Id
End
Go