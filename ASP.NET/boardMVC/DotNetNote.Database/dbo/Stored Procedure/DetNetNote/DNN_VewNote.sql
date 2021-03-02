Create Procedure dbo.ViewNote
@Id Int
As
 --조회수 카운트 1 증가
 Update Notes Set ReadCount = ReadCount + 1 Where Id = @Id

 --모든 항목 조회
 Select * From Notes Where Id = @Id

GO