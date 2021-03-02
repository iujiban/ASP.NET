CREATE PROCEDURE dbo.ListNotes
@Page Int
As
With DotNetNoteOrderedLists
As
(
Select [Id], [Name], [Email], [Title], [PostDate],[ReadCount], [Ref],
	[Step],[RefOrder],[AnswerNum],[ParentNum],[CoomentCount],[FileName],[FileSize],[DownCount], 
Row_NUMBER() Over (Order By Ref Desc, RefOrder Asc) As 'RowNumber'
	From Notes
)
Select * From DotNetNoteOrderedLists
	Where RowNumber Between @Page * 10 + 1 And (@Page +1) * 10