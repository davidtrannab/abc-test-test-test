using System.Linq;
using System.Xml.Linq;
using System.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using ALup.Language;
namespace ALupMartV2.Manager
{
    public partial class ViewMenu : PortalModuleBase
    {

        MartLinQDataContext dp1 = new MartLinQDataContext();
        CatalogDataProvider cdp = new CatalogDataProvider();
        ProductDataProvider pdp = new ProductDataProvider();

        #region PAGELOAD
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindData();

                if (Request.QueryString["id"] != null)
                {
                    long catID = long.Parse(Request.QueryString["id"].ToString());

                }


            }
        }
        #endregion
        #region FIND DATA
        protected string InitListCatalog(long id)
        {
            string html = "";//

            try
            {

                ALupMart_Catalog catalogSearch = cdp.SearchCatalogByID2(PortalId, id, 1);
                html = "<a>" + catalogSearch.CatalogName + "</a>";
                if (catalogSearch.ALupMart_CatalogsInfo.ParentID != -1)
                    while (true)
                    {
                        catalogSearch = cdp.SearchCatalogByID2(PortalId, catalogSearch.ALupMart_CatalogsInfo.ParentID, 1);
                        //  Response.Write(catalogSearch.CatalogID.ToString()+catalogSearch.CatalogName);
                        html = "<a href=\"" + ProductsFunctions.GetUrlListProduct(catalogSearch.CatalogID, StringUtil.ConvertUrlString(catalogSearch.CatalogName), null, "") + "\"><i></i>" + catalogSearch.CatalogName + "</a>" + html;
                        if (catalogSearch.ALupMart_CatalogsInfo.ParentID == -1) break;
                    }
            }
            catch { }
            html = "<div class=\"breadcrum\"> <a href=\"http://" + PortalAlias.HTTPAlias + "\">Trang chủ</a>&nbsp;»&nbsp;" + html + "</div>";

            return html;

        }



        #endregion
        protected void BindData()
        {


            rptItemMenu.DataSource = cdp.SearchCategoryCAP1Active_1(1);
            rptItemMenu.DataBind();
           

        }
        protected void Rpt_ItemRootDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            Repeater rptItemMenu1 = (Repeater)e.Item.FindControl("rptItemMenu1");
            Repeater rptItemMenu2 = (Repeater)e.Item.FindControl("rptItemMenu2");
           
            System.Web.UI.HtmlControls.HtmlGenericControl Menusub2 = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("Menusub2");
            System.Web.UI.HtmlControls.HtmlGenericControl MenuManufaceter = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("MenuManufaceter");
            System.Web.UI.HtmlControls.HtmlGenericControl Menucon = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("Menucon");
        
            Repeater RptManufacter = (Repeater)e.Item.FindControl("RptManufacter");
            Literal Thedau = (Literal)e.Item.FindControl("Thedau");
            Literal Thecuoi = (Literal)e.Item.FindControl("Thecuoi");
            Literal icon_img = (Literal)e.Item.FindControl("icon_img");
            Literal AnhnenMenu = (Literal)e.Item.FindControl("AnhnenMenu");
          
            if (cdp.SearchAllChildrenCatalogAMenuTrue(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString())).Count > 0)
            {
                if (bool.Parse(DataBinder.Eval(e.Item.DataItem, "IsAnhNen").ToString()) == true)
                {
                    try
                    {
                        if (DataBinder.Eval(e.Item.DataItem, "AnhNenMenu") != null)
                        {
                            string anhnen = "/Portals/2/" + DataBinder.Eval(e.Item.DataItem, "AnhNenMenu").ToString();
                            Thedau.Text = "  <div id=\"submenu-" + DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString() + "\" class=\"popover subcssmenuhome\" style=\"background:url('" + anhnen + "') no-repeat\" >";
                            Thecuoi.Text = "  </div> ";
                        }
                        else
                        {
                            Thedau.Text = "  <div id=\"submenu-" + DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString() + "\" class=\"popover subcssmenuhome\" >";
                            Thecuoi.Text = "  </div> ";
                        }
                    }
                    catch { }
                }
                else
                {
                    Thedau.Text = "  <div id=\"submenu-" + DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString() + "\" class=\"popover\" >";
                    Thecuoi.Text = "  </div> ";
                }

            }

            if (cdp.SearchAllChildrenCatalogAMenuTrue(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString())).Count()>0)
            {
                rptItemMenu1.DataSource = cdp.SearchAllChildrenCatalogAMenuTrue(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString()));
                rptItemMenu1.DataBind();
                Menucon.Visible = true;
               
            }
            else
            {
                
            }
            if (cdp.SearchAllChildrenCatalogAMenuTrue(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString())).Skip(1 * 5).Take(5).Count() > 0)
            {
                rptItemMenu2.DataSource = cdp.SearchAllChildrenCatalogAMenuTrue(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString())).Skip(1 * 5).Take(5);
                rptItemMenu2.DataBind();
                Menusub2.Visible = true;
            }
            //if (cdp.SearchAllChildrenCatalogAMenuTrue(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString())).Skip(2 * 5).Take(5) != null)
            //{
            //    rptItemMenu3.DataSource = cdp.SearchAllChildrenCatalogAMenuTrue(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString())).Skip(2 * 5).Take(5);
            //    rptItemMenu3.DataBind();
            // //   Panel3.Visible = true;
            //}
            if (cdp.searchAllManufacturer(PortalId, int.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString()), "").Take(10).Count() > 0)
            {
                RptManufacter.DataSource = cdp.searchAllManufacturer(PortalId, int.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString()), "").Take(10);
                RptManufacter.DataBind();
                //MenuManufaceter.Visible = true;
            }

        }
        protected void Rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptItemMenu1 = (Repeater)e.Item.FindControl("rptItemMenu1");
            rptItemMenu1.DataSource = cdp.SearchAllChildrenCatalogAMenuTrue(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString())).Take(10);
            rptItemMenu1.DataBind();
        }
        protected string Getlink(object name, object id)
        {
            return "/" + StringUtil.ConvertUrlString(name.ToString()) + "-cp" + id;
        }
        protected string Getlinkmanu(object name, object id, object m)
        {
            return "/" + StringUtil.ConvertUrlString(name.ToString()) + "-cp" + id + "?m=" + m.ToString();
        }
    }
}
