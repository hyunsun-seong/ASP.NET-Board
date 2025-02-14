﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace boardWebPage.Board
{
    public partial class BoardView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
                conn.Open();

                //  조회수 증가
                string updateSql = "update RetblBrd set readcnt=readcnt+1 where num=" + Request["No"];
                SqlCommand cmd = new SqlCommand(updateSql, conn);
                cmd.ExecuteNonQuery();

                // 문자열로 받은 게시물 ID로 데이터 행 선택
                string selectSql = "select * from RetblBrd where num=" + Request["No"];
                SqlCommand Scmd = new SqlCommand(selectSql, conn);
                Scmd.ExecuteNonQuery();
                SqlDataReader dr = Scmd.ExecuteReader();

                if (dr.Read())
                {
                    txtname.Text = dr["name"].ToString();
                    txtwritedate.Text = dr["writedate"].ToString();
                    txtreadcnt.Text = dr["readcnt"].ToString();
                    txttitle.Text = dr["title"].ToString();
                    txtcontents.Text = dr["contents"].ToString();
                    lblnum.Text = dr["num"].ToString();
                    lblrefer.Text = dr["refer"].ToString();
                    lbldepth.Text = dr["depth"].ToString();
                    lblpos.Text = dr["pos"].ToString();
                }
                dr.Close();
                conn.Close();
            }
        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("BoardList.aspx");
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckPassword.aspx?Action=Modify&No=" + Request["No"]);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckPassword.aspx?Action=Delete&No=" + Request["No"]);
        }

        protected void btnReply_Click(object sender, EventArgs e)
        {
            Response.Redirect("BoardReply.aspx?Action=Reply?No=" + Request["No"] + "&num=" + lblnum.Text + "&refer=" + lblrefer.Text + "&depth=" + lbldepth.Text + "&pos=" + lblpos.Text);
        }
    }
}