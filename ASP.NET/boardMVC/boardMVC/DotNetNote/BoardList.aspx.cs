using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using boardMVC.DotNetNote.Models;

namespace boardMVC.DotNetNote
{
    public partial class BoardList : System.Web.UI.Page
    {
        //공통 속성: 검색모드: 검색모드이면 true, 그렇지 않으면 false
        public bool SearchMode { get; set; } = false;
        public string SearchField { get; set; }
        public string SearchQuery { get; set; }

        public int PageIndex = 0; //현재 보여줄 페이지 번호
        public int RecordCount = 0; //총 레코드 개수 (글 번호 순서 정렬용)


        private NoteRespository _repository;
        public BoardList()
        {
            _repository = new NoteRespository();
        }
        protected void Page_Load(object sender, EventArgs e)
        { // 검색 모드 결정
            
            SearchMode = (!string.IsNullOrEmpty(Request.QueryString["SearchField"]) && 
                !string.IsNullOrEmpty(Request.QueryString["SearchQuery"]));
            if(SearchMode)
            {
                SearchField = Request.QueryString["SearchField"];
                SearchQuery = Request.QueryString["SearchQuery"];
            }

        //쿼리스트링에 따른 페이지 보여주기
        if (Request["Page"] !=null)
            {
                //page는 보여지는 쪽은 1,2 ~ code => 0, 1
                PageIndex = Convert.ToInt32(Request["Page"]) - 1;
            } else
            {
                PageIndex = 0; // 1페이지
            }

        //쿠키를 사용한 리스트 펭지ㅣ 번호 유지적용:
        // 100번째 페이지의 글 보고, 다시 리스트 왔을 때 100번쨰 페이지 표시
        if(Request.Cookies["DoNetNote"] != null)
            {
                if(!String.IsNullOrEmpty(Request.Cookies["DotNetNote"]["PageNum"]))
                {
                    PageIndex = Convert.ToInt32(Request.Cookies["DotNetNote"]["PageNum"]);
                } else
                {
                    PageIndex = 0;
                }
            }
        // 레코드 카운트 출력
        if (SearchMode == false)
            {
                //Notes 테이블의 전체 레코드
                RecordCount = _repository.GetCountAll();
            } else
            {
                //Notes 테이블 중 SearchField + SearchQuery에 해당하는 레코드 수
                RecordCount = _repository.GetCountBySearch(SearchField, SearchQuery);
            }
            lblTotalRecord.Text = RecordCount.ToString();

            //페이징 처리
            AdvancedPagingSingleWithBootstrap.PageIndex = PageIndex;
            AdvancedPagingSingleWithBootstrap.RecordCount = RecordCount;
            if (!Page.IsPostBack)
            {
                //if(!String.IsNullOrEmpty(Session["userID"].ToString())) // 이 값을 넣을 시 else가 작동이 안되고 여기서 멈추어진다.
                if(Session["userID"] != null)
                {
                    DisplayData();

                } 
                else
                {
                    
                    Response.Redirect("~/DotNetNote/UserLogin.aspx");
                }
                
            }

        }
        public void DisplayData()
        {
            if (SearchMode == false) //기본 리스트
            {
                ctlBoardList.DataSource = _repository.GetAll(PageIndex);

            } else //검색 결과 리스트
            {
                ctlBoardList.DataSource = _repository.GetSearchAll(PageIndex, SearchField, SearchQuery);
            }
            ctlBoardList.DataBind();
        }
    }
}