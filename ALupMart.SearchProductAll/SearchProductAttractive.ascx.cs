
using System;
using System.Web.UI;
using System.Linq;
using System.Collections.Generic;
using DotNetNuke.Entities.Modules;
using System.Web.UI.WebControls;
using ALup.Language;
using ALupMartV2.Manager;
using System.Collections.Specialized;
using System.Xml.Linq;
public partial class View : PortalModuleBase
{
    int tabId = 0;
    protected string urlHost = "/Portals/2/";
    long catID = 0;
    long? mnuID = null;
    long? mau = null;
    double? bPrice = null;
    double? ePrice = null;
    long? dis = null;
    string key = "";
    string price = "";
    string urlParam = string.Empty;
    int desc = 0;
    XElement paramE = null;
    ProductDataProvider pd = new ProductDataProvider();
    MartLinQDataContext dp = new MartLinQDataContext();

    #region VARIABLE PAGING

    //Các biến phân trang
    private int page = 0;
    private string urlPostBack;
    private int recordNumberALl;

    int numberPage;
    #endregion

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            //try
            {
                urlHost = Functions.GetVisualDirectoryFileUpload(PortalId);
                BindData();

            }
           // catch { }
        }
    }
    #endregion

    #region FIND DATA

    protected void BindData()
    {
       
        if (string.IsNullOrEmpty(Request.QueryString["cat_id"]))
        {
            Repeater1.DataSource = new CatalogDataProvider().SearchAllChildCategoryByCatalogID(622, 1);
            Repeater1.DataBind();
        }
        else
        {
           
            CatalogDataProvider catalogData = new CatalogDataProvider();
            divCatalog.Visible = true;
            long cat_id = long.Parse(Request.QueryString["cat_id"]);
            lrlTitle.Text = catalogData.SearchCatalogByID2(2, cat_id, 1).CatalogName;
            rptCatalog.DataSource = catalogData.SearchAllChildCategoryByCatalogID((int)cat_id, 1);
            rptCatalog.DataBind();
          
            
        }
       
    }


    #endregion
    protected void Rpt_ItemCatalogDataBound(object sender, RepeaterItemEventArgs e)
    {
        long catID = long.Parse(DataBinder.Eval(e.Item.DataItem, "CatalogID").ToString());

        Repeater rptProduct = (Repeater)e.Item.FindControl("rptProduct");

        rptProduct.DataSource = pd.ALupMart_SearchProductByParam(PortalId, catID, mnuID, 1, bPrice, ePrice, key, paramE, 1, 4, ref recordNumberALl, desc);


        rptProduct.DataBind();

        System.Web.UI.HtmlControls.HtmlGenericControl divCatalog = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divCatalog");
        if (rptProduct.Items.Count <= 0)    
        {
            divCatalog.Visible = false;
        }

        //  Response.Write(rptProduct.Items.Count.ToString());

    }
    protected void Rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (double.Parse(DataBinder.Eval(e.Item.DataItem, "PriceMarket").ToString()) <= 0)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl PriceOldDiv = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("PriceOldDiv");
            PriceOldDiv.Visible = false;
        }
        if (DataBinder.Eval(e.Item.DataItem, "Price").ToString() == "0")
        {
            System.Web.UI.HtmlControls.HtmlGenericControl PriceDiv = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("PriceDiv");
            PriceDiv.InnerHtml = "Vui lòng liên hệ";
        }


    }
    protected string GetUrl(object ProductId, object ProductName)
    {

        return ProductsFunctions.GetUrlProductDetail(ProductId, Server.HtmlDecode(ProductName.ToString()));
    }

    protected string GetUrl1(object ProductId, object ProductName)
    {

        return ProductsFunctions.GetUrlListProduct(long.Parse(ProductId.ToString()), StringUtil.ConvertUrlString(Server.HtmlDecode(ProductName.ToString())), null);
    }


}
