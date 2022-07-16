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

        MartLinQDataContext dp = new MartLinQDataContext();
        CatalogDataProvider cdp = new CatalogDataProvider();
        ProductDataProvider pdp = new ProductDataProvider();

        #region PAGELOAD
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (!IsPostBack)
            {

                BindData();

            }
        }
        #endregion

        protected void BindData()
        {
            rptItemMenu.DataSource = cdp.searchCategoryChildByIDArray(-1, ALup.Language.ConverLanguageDataProvider.GetSessionLanguage(Session));
            rptItemMenu.DataBind();

        }

        protected void Rpt_ItemDataBound2(object sender, RepeaterItemEventArgs e)
        {
            //HyperLink ItemMenu = (HyperLink)e.Item.FindControl("ItemMenuHyperLink");
            //ItemMenu.Text = DataBinder.Eval(e.Item.DataItem, "CatalogName").ToString();
            //ItemMenu.NavigateUrl = ProductsFunctions.GetUrlListProduct(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString()), StringUtil.ConvertUrlString(ItemMenu.Text), null);// long.Parse(DataBinder.Eval(e.Item.DataItem, "value").ToString().Split('*')[1]), string.Empty, string.Format("{0}={1}", NAME_URL, DataBinder.Eval(e.Item.DataItem, "key")));

        }


        protected void Rpt_ItemRootDataBound(object sender, RepeaterItemEventArgs e)
        {
            HyperLink ItemMenu = (HyperLink)e.Item.FindControl("ItemMenuHyperLink");
          
            string CatName = DataBinder.Eval(e.Item.DataItem, "CatalogName").ToString();
            //ItemMenu.Text = CatName;

            
            ItemMenu.NavigateUrl = ProductsFunctions.GetUrlListProduct(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString()), StringUtil.ConvertUrlString(CatName), null);

           

        }
        protected void Rpt_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            HyperLink ItemMenu = (HyperLink)e.Item.FindControl("ItemMenuHyperLink");
            Repeater itemMenu = (Repeater)e.Item.FindControl("itemMenu");

            string CatName = DataBinder.Eval(e.Item.DataItem, "CatalogName").ToString();
            ItemMenu.Text = CatName;

            itemMenu.DataSource = cdp.searchCategoryChildByID(int.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString()), 1);
            itemMenu.DataBind();

            ItemMenu.NavigateUrl = ProductsFunctions.GetUrlListProduct(long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString()), StringUtil.ConvertUrlString(CatName), null);

            if (itemMenu.Items.Count > 0)
            {
                Literal lrlOpen = (Literal)e.Item.FindControl("lrlOpen");

                lrlOpen.Text = " <ul>";

                Literal lrlClose = (Literal)e.Item.FindControl("lrlClose");
                lrlClose.Text = "</ul><i class=\"fa fa-angle-down\"></i>";

                ItemMenu.CssClass = "parent";
            }
            else
            {
                ItemMenu.CssClass = "";
            }

        }

    }
}
