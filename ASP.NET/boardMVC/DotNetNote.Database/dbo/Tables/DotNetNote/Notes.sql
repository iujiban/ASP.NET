Create Table dbo.Notes
(
Id Int Identity(1,1) Not Null Primary Key, -- 번호
Name NVarChar(25) Not Null, -- 이름
Email NVarchar(100) Null, -- 이메일
Title NVarChar(150) Not Null, -- 제목
PostDate DateTime Default GetDate() Not Null, --작성일
PostIp NVarChar(15) Null, --작성 ip
Content NText Not NULL, -- 내용
Password NVarchar(20) Null, --게시판 비밀번호
ReadCount Int default 0, -- 조회수
Encoding NVarchar(10) Not Null, --인코딩
HomePage NVarchar(100) Null, -- 홈페이지
ModifyDate DateTime Null, -- 수정일
ModifyIp NVarchar(15) Null, -- 수정 Ip
FileName NVarchar(255) Null, --파일명
FileSize Int default 0, --파일 크기
DownCount Int Default 0, -- 다운수
Ref Int Not Null, --참조 (부모글)
Step Int Default 0, -- 답변깊이(레벨)
RefOrder Int Default 0, -- 답변순서
AnswerNum Int Default 0, -- 답변 수
ParentNum Int Default 0, -- 댓글 수
Category NVarChar(10) Null -- 카테고리
)
Go