Create Table Memos
(
Num Int Identity(1,1) Primary key, --번호
Name NVarchar(25) Not Null, --이름
Email NVarchar(100) Not Null, --이메일
Title NVarchar(150) Not Null, --메모
PostDate DateTime Default (GetDATE()), -- 작성일
PostIp NVarchar (15) Null -- IP 주소
)
Go

-- 입력문
Insert Memos
Values
(
N'레드플러스', N'redplus@devlec.com', N'레드플러스입니다',
GETDATE(), '127.0.0.1'
)
Go

-- 출력문
Select Num, Name, Email, Title, PostDate, PostIp
From Memos Order By Num Desc
Go

--상세문
Select Num, Name, Email, Title, PostDate, PostIp
From Memos Where Num = 1
Go

--수정문
Begin Tran
Update Memos
Set 
Name = N'백두산',
Email = N'admin@devlec.com',
Title= N'백두산입니다',
PostIp = N'127.0.0.1'
Where Num = 1

commit Tran
Go

-- 삭제문
Begin Tran
Delete Memos
Where Num = 10
commit Tran
Go

-- 검색문
Select Num, Name, Email, Title, PostDate
From Memos
Where Name = '레드플러스' or
Email Like '%r'
Order By Num Desc
Go

-- SQL 자동 프로시저 6가지
-- 메모 입력용
Create Procedure dbo.WriteMemo
(
@Name NVarchar(25),
@Email NVarchar(100),
@Title NVarchar(150),
@PostIp NVarchar(15)
)
As
Insert Memos(Name, Email, Title, PostIp) values
(@Name, @Email, @Title, @PostIp) 
Go

--메모 출력용
Create Procedure dbo.ListMemo
As
Select Num, Name, Email, Title, PostDate, PostIp
From Memos Order By Num DESC
GO

-- 메모 상세보기용
Create Procedure dbo.ViewMemo
(
@Num Int
)
As Select Num, Name, Email, Title, PostDate, PostIp
From Memos
Where Num = @Num
Go

--메모 데이터 수정용 저장 프로시저
Create Proc dbo.ModifyMemo
(
@Name NVarChar(25),
@Email Varchar(100),
@Title NVarchar(150),
@Num Int
)
As
Begin Transaction
Update Memos
Set Name = @Name, 
Email = @Email,
Title = @Title
Where Num = @Num
Commit Transaction
Go

--메모 데이터 검생용 저장 프로시저 (동적 SQL 사용)
Create Proc dbo.SearchMemo
( @SearchField NVarChar(10),
@SearchQuery NVarChar(50))
-- With Encryption -- 현재 SP문 암호화
As
Declare @strSql NVarchar(150) -- 변수선언
Set @strSql = 
'
Select Num, Name, Email, Title, PostDate, PostIp
From Memos
Where ' + @SearchField + ' Like
N''%' + @SearchQuery + '%''
Order By Num Desc
'
--print @strSql
Exec (@strSql)
Go