Create Procedure dbo.SearchNotes
@Page INT,
@SearchField NVarChar(25),
@SearchQuery NVarChar(25)
As
With DotNetNoteOrderedLists
As
(
Select [Id],[Name],[Email],[Title], [PostDate], [ReadCount],[Ref],[Step],[RefOrder],[AnswerNum]
,[ParentNum], [FileName], [FileSize],[DownCount], ROW_NUMBER() Over (Order By Ref Desc, RefOrder Asc)
As 'RowNumber'
From Notes
Where ( Case @SearchField
When 'Name' Then [Name]
When 'Title' Then Title
When 'Conten' Then Content
Else
@SearchQuery
End

) Like '%' + @SearchQuery + '%'
)
Select [ID], [Name], [Email], [Title], [PostDate], [ReadCount], [Ref], [Step], [RefOrder], [AnswerNum]
,[ParentNum], [FileName], [FileSize], [DownCount], [RowNumber] From DotNetNoteOrderedLists
Where RowNumber Between @Page * 10 + 1 And (@Page + 1) * 10
Order By Id Desc
Go