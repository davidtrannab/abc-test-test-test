
using System;
using System.Web.UI;
using System.Linq;
using System.Collections.Generic;
using DotNetNuke.Entities.Modules;
using System.Web.UI.WebControls;
using ALup.Language;
using ALupMartV2.Manager;
using System.Xml.Linq;
using AlupHtmlV2.Manager;
using DotNetNuke.Common;
public partial class View : PortalModuleBase
{
    long? mnuID = null;
    long? mau = null;
    double? bPrice = null;
    double? ePrice = null;
    long? dis = null;
    string key = "";
    
    string urlParam = string.Empty;
    int desc = 0;
    XElement paramE = null;
    protected string strHT = "Họ tên";
    protected string strCM = "Cảm nhận của bạn";
    CommentDataProvider cdp = new CommentDataProvider();

    protected string urlHost = "/Portals/2/";
    protected long id = 0;
    MartLinQDataContext dp = new MartLinQDataContext();
    string seo = "<div style=\"padding-top:0px;\" <!-- AddThis Button BEGIN --><div class=\"addthis_toolbox addthis_default_style \" style=\"width: 150px;\"><a class=\"addthis_button_preferred_1\"></a><a class=\"addthis_button_preferred_2\"></a><a class=\"addthis_button_preferred_3\"></a><a class=\"addthis_button_preferred_4\"></a><a class=\"addthis_button_compact\"></a><a class=\"addthis_counter addthis_bubble_style\"></a></div><script type=\"text/javascript\">var addthis_config = {\"data_track_clickback\":true};</script><script type=\"text/javascript\" src=\"http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-4daff1497c2f0a37\"></script><!-- AddThis Button END --></div>";
    protected string FbID = "";
    protected string ImageSource = "";
    protected double price;
    protected string codesp;
    int countNhom = 0;
    double tongtien = 0;
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {


            //try
            //{

            id = long.Parse(Request.QueryString["id"].ToString());
            new ProductDataProvider().UpdateTotalView((int)id);

            InitImage();

            urlHost = Functions.GetVisualDirectory(PortalId);
            InitHtml();
            InitSPMuaCungNhau(id);

            BindData();


            //  InitComment();s

            //}
            //catch { }
        }
    }
    #endregion
    public void InitHtml()
    {

        AlupHtmlV2.Manager.TextDataProvider textDataProvider = new AlupHtmlV2.Manager.TextDataProvider();
        try
        {
            lrlQc1.Text = Server.HtmlDecode(textDataProvider.searchTextByID(67, 1).Description);
        }
        catch { }
        try
        {
            lrlQc2.Text = Server.HtmlDecode(textDataProvider.searchTextByID(66, 1).Description);
        }
        catch { }
    }
    protected string InitData(string title, string content)
    {
        string html = "";
        html += "<li class=\"m_bottom_3\"><span class=\"project_list_title second_font d_inline_b\">" + title + ":</span> <span class=\" fw_light\">" + content + "</span></li>";


        return html;
    }


    protected string InitData2(string title, string content)
    {
        string html = "";

        string abcsize = " <div class=\"rowDescrip\">" + title + "</div>";
        string size = "<div class=\"colName\"><span>" + title + "</span></div>";
        size += "<div class=\"col_dtP\">" + content + "</div>";
        html += "<div class=\"rowtt\"><div class=\"clleft\">" + title + "</div><div class=\"clright\">" + content + "</div></div><div class=\"cl\"></div>";

        return "<div class=\"rowDescrip\">" + size + "</div>";
    }
    #region FIND DATA

    protected void BindData()
    {

        DotNetNuke.Framework.CDefault tp = (DotNetNuke.Framework.CDefault)this.Page;
        ProductDataProvider pd = new ProductDataProvider();
        ALupMart_Product temp = pd.SearchProductByID(id, 1);
        id = temp.ProductID;
        ProductName.Text = temp.ProductName;
        Functions.InitSeoDetail(ref tp.Title, ref tp.KeyWords, ref tp.Description, Page, PortalId, temp.SeoTitle, temp.SeoKeyword, temp.SeoDescription);
        int recordNumberALl = 0;
        rptProduct.DataSource = pd.ALupMart_SearchProductByParam(PortalId, temp.ALupMart_ProductsInfo.CatalogID, mnuID, 1, bPrice, ePrice, key, paramE, 1, 10, ref recordNumberALl, desc);

       // rptProduct.DataSource = dp.ALupMart_SearchProductByCatalogId(PortalId, temp.ALupMart_ProductsInfo.CatalogID, 1, 0, 9);
        rptProduct.DataBind();
        if (!string.IsNullOrEmpty(temp.ALupMart_ProductsInfo.ImageSource))
        {
            ImageSource = temp.ALupMart_ProductsInfo.ImageSource;
        }
        string html = "";
        try
        {
            if (temp.ALupMart_ProductsInfo.Price > 0)
            {
                lrlPrice.Text = "<dl class=\"tm-promo-panel tm-promo-cur\" data-label=\"Giá bán\"><dt class=\"tb-metatit\">Giá bán:</dt><dd><div class=\"tm-promo-price\"> <span class=\"customer_price_item tm-price \">" + String.Format("{0:#,###}", (double)temp.ALupMart_ProductsInfo.Price).Replace(",", ".") + "</span>   <em class=\"tm-yen\" style=\"color: #c40000;\">vnđ</em>&nbsp;&nbsp;</div> <p>   </p></dd></dl>";
                //";//String.Format("{0:#,###}", (double)temp.ALupMart_ProductsInfo.Price).Replace(",", ".") + "đ";
            }
            else
            {
                lrlPrice.Text = "Liên hệ: <em class=\"tm-yen\" style=\"color: #ec0b0b;font-size: 20px;font-weight: bold;padding-left: 80px;\">  096.958.8998 </em> ";
            }
            price = temp.ALupMart_ProductsInfo.Price;
            if (temp.ALupMart_ProductsInfo.PriceMarket > 0)
            {
                lrlPriceMarket.Text = "<dl class=\"tm-price-panel\">" +
                                      "<dt class=\"tb-metatit\">Giá thị trường:</dt>" +
                                      "<dd><span class=\"tm-price line-through\">" + String.Format("{0:#,###}", (double)temp.ALupMart_ProductsInfo.PriceMarket).Replace(",", ".") + "</span><em class=\"tm-yen\"> vnđ</em> </dd></dl>";//  + "đ";
                sales.Visible = true;
            }
        }
        catch { }
        pd.UpdateTotalView((int)temp.ProductID);
        try
        {
            if (temp.ALupMart_ProductsInfo.ManufacturerID != null)
            {
                ALupMart_Manufacturer manufa = new CatalogDataProvider().searchManufacturerByID(PortalId, (long)temp.ALupMart_ProductsInfo.ManufacturerID);
                HL_Thuonghieu.Text = manufa.ManufacturerName;
                HL_Thuonghieu.NavigateUrl = "/" + manufa.ManufacturerName + "-cp0-" + manufa.ManufacturerID;
            }
        }
        catch { }


        if (!string.IsNullOrEmpty(temp.ALupMart_ProductsInfo.ProductCode))
        {
            Li_code.Text = "Mã sản phẩm: " + temp.ALupMart_ProductsInfo.ProductCode;
            codesp = temp.ALupMart_ProductsInfo.ProductCode;
        }
        try
        {
            if (!string.IsNullOrEmpty(temp.ALupMart_ProductsInfo.MadeIn))
                html += InitData("Xuất xứ ", temp.ALupMart_ProductsInfo.MadeIn);
        }
        catch { }
        try
        {
            if (!string.IsNullOrEmpty(temp.Warranty))
                html += InitData("Bảo hành ", temp.Warranty);
        }
        catch { }
        try
        {
            if (temp.ALupMart_ProductsInfo.PriceVAT)
                html += InitData("Vat", "<vat>Đã có VAT</vat>");
            else
                html += InitData("Vat", "<vat>Chưa tính VAT</vat>");
        }
        catch { }

        try
        {
            html += InitData("Lượt xem ", temp.ALupMart_ProductsInfo.TotalView.ToString());
        }
        catch { }
        try
        {
        setAtrributeOtherOrder(temp.OtherAttributesOrder);
      
            string htmlConten = "";
            foreach (XElement item_ in temp.AttributeTemp.Elements())
            {

                htmlConten += "<li style=\"  width: 50%;    margin-left: 0px;  border-style: solid solid solid;  border-color: #E6E2E1;\"><span class=\"title-attr\" style=\"font-weight:bold;margin-left: 25px;\">" + item_.Attribute("name").Value + ": </span>";
                foreach (XElement item_1 in item_.Elements())
                {

                    htmlConten += item_1.Attribute("name").Value + ", ";


                }

                if (item_.Elements().Count() > 0)
                    htmlConten = htmlConten.Substring(0, htmlConten.Length - 2);
                htmlConten += "</li>";

            }

            lrlAttribute.Text = "<ul>" + htmlConten + "</ul>";
        }
        catch { }



        if (!string.IsNullOrEmpty(temp.Description_1))
            Lb_Mieuta.Text = temp.Description_1;


        Literal1.Text = html;
        descriptionDiv.InnerHtml = Server.HtmlDecode(temp.Description_2);
        ThanhPhan.InnerHtml = Server.HtmlDecode(temp.ReviewArticle);
        divKhuyenMai.InnerHtml = Server.HtmlDecode(temp.Promotion);
        Cachsudung.InnerHtml = Server.HtmlDecode(temp.Video);
        int d = cdp.TinhDiem(id);

        for (int i = 0; i < d; i++){
				initStar.Text += "<li class=\"star color_lbrown\" data-value=\""+ (i+1) +"\"><i class=\"fa fa-star fa-fw\"></i></li>";	
		}            
        for (int i = d; i < 5; i++){
				initStar.Text += "	<li class=\"star\" data-value=\""+ (i+1) +"\"><i class=\"fa fa-star fa-fw\"></i></li>";
			}


        totalCom.Text = cdp.CountCommentByProductId(id).ToString();

        if (temp.ALupMart_ProductsInfo.AllowOrder == '1')
        {
            OrderProductDiv.Visible = true;
        }
        else
        {
            OrderProductDiv.Visible = false;
        }
        OrderProductDiv.Visible = true;

    }


    protected void InitImage()
    {
        try { 
        id = long.Parse(Request.QueryString["id"].ToString());
        imageRepeater.DataSource = dp.ALupMart_SearchImageByProductId(id);
        imageRepeater.DataBind();
        if (dp.ALupMart_SearchImageByProductId(id).Count() <= 0)
            imgCon.Visible = false;

      //  Response.Write(id.ToString());
        }
        catch { }
    }

    #endregion

    protected void setAtrributeOtherOrder(XElement temp)
    {
        //   try
        {
            string strItem;
            if (temp != null)
            {


                var at = temp.Elements();
                string htmlConten = string.Format("<input type='hidden' id='lengthAtrr' value='{0}' />", at.Count());
                int i = 0;
                int j = 0;
                foreach (XElement re in at)
                {
                    var Items = re.Elements();
                    strItem = string.Format("<input type='hidden' id='attr{0}' />", i);
                    j = 0;
                    if (re.Attribute("type").Value == "text")
                        foreach (XElement item in Items)
                        {
                            j++;

                            string priceItem = price.ToString(), priceItemDis = String.Format("{0:#,###}", price).Replace(",", ".") + "đ"; ;
                            if (!string.IsNullOrEmpty(item.Attribute("price").Value))
                            {
                                priceItem = item.Attribute("price").Value;
                                try
                                {
                                    priceItemDis = String.Format("{0:#,###}", (double.Parse(item.Attribute("price").Value))).Replace(",", ".") + "đ"; ;
                                }
                                catch { }
                            }

                            strItem += "<a id='item" + i + j + "' class='sizeProducts item" + i + "' href=\"javascript:SelectAttr2('" + re.Attribute("name").Value + "','" + item.Attribute("name").Value + "','" + i + "','" + j + "'," + priceItem + ",'" + priceItemDis + "')\" >" + item.Attribute("name").Value + "</a> ";

                        }
                    else
                        foreach (XElement item in Items)
                        {

                            string imgdg = "/Portals/2/" + item.Attribute("name").Value;
                            string priceItem = price.ToString(), priceItemDis = String.Format("{0:#,###}", price).Replace(",", ".");// +"đ"; ;
                            string CodeItem = codesp.ToString(), codeItemDis = codesp;// +"đ"; ;
                            if (!string.IsNullOrEmpty(item.Attribute("price").Value))
                            {
                                priceItem = item.Attribute("price").Value;
                                try
                                {
                                    priceItemDis = String.Format("{0:#,###}", (double.Parse(item.Attribute("price").Value))).Replace(",", ".");// +"đ"; ;
                                }
                                catch { }
                            }
                            if (!string.IsNullOrEmpty(item.Attribute("code").Value))
                            {
                                CodeItem = item.Attribute("code").Value;
                                try
                                {
                                    codeItemDis = item.Attribute("code").Value;// +"đ"; ;
                                }
                                catch { }
                            }

                            string azoom = string.Empty;
                            if (item.Attribute("des") != null)
                            {
                                string imgdg2 = "<img onclick=\"SelectAttr('" + re.Attribute("name").Value + "','" + item.Attribute("des").Value + "','" + i + "'," + priceItem + ",'" + priceItemDis + "','" + item.Attribute("code").Value + "')\" class=\"zoom-tiny-image y-sub-imgColor-details\" src=\"/Portals/2/Small_" + item.Attribute("name").Value + "\" /><br><div>" + item.Attribute("des").Value + "</div>";

                                azoom = "<a  data-image=\"/Portals/2/Small_" + item.Attribute("name").Value + "\" data-zoom-image=\"" + imgdg + "\"  href='javascript:return;'>" + imgdg2 + "</a>";

                            }
                            else
                            {
                                string imgdg2 = "<img  class=\"zoom-tiny-image y-sub-imgColor-details\" src=\"/Portals/2/Small_" + item.Attribute("name").Value + "\" />";

                                azoom = "<a  data-image=\"/Portals/2/Small_" + item.Attribute("name").Value + "\" data-zoom-image=\"" + imgdg + "\"  href='#'>" + imgdg2 + "</a>";
                            }
                            strItem += "<div class=\"zoom-desc\" id=\"thumbnails\" data-nav=\"thumbnails_product_\">" + azoom + "</div>";
                        }
                    htmlConten += InitData2(re.Attribute("name").Value, strItem);
                    i++;
                }

                otherAttributeOrderLiteral.Text = htmlConten;
            }
        }
        // catch { }
    }

    protected void Rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void Rpt_ItemImageDataBound(object sender, RepeaterItemEventArgs e)
    {


    }
    protected string GetUrl(object ProductId, object ProductName)
    {
        return ProductsFunctions.GetUrlProductDetail(ProductId, ProductName);
    }
    protected string GetUrlImage(object instr)
    {
        if (instr != null)
        {
            return urlHost + instr.ToString();
        }
        return "";
    }

    protected string GetPriceMarket(object p)
    {

        if (p.ToString() != "0") return String.Format("{0:#,###}", double.Parse(p.ToString())) + " đ";
        return "";

    }
    protected string GetPrice(object p)
    {

        if (p.ToString() != "0") return String.Format("{0:#,###}", double.Parse(p.ToString())) + " đ";
        return "Liên hệ";
    }
    protected string GetPriceSale(object price, object priceMarket)
    {

        if (priceMarket.ToString() == "0") return "";

        return "<div class=\"product_label fs_ex_small circle color_white bg_lbrown t_align_c vc_child tt_uppercase\"><i class=\"d_inline_m\">Sale!</i></div>";


    }


    protected void InitItem_DataBound(Object sender, ListViewItemEventArgs e)
    {
        //try{

        Image img = (Image)e.Item.FindControl("img");
        int s = int.Parse(DataBinder.Eval(e.Item.DataItem, "Vote").ToString());
        if (s == 1) img.ImageUrl = "/images/1 sao.gif";
        else if (s == 2) img.ImageUrl = "/images/2 sao.gif";
        else if (s == 3) img.ImageUrl = "/images/3 sao.gif";
        else if (s == 4) img.ImageUrl = "/images/4 sao.gif";
        else if (s == 5) img.ImageUrl = "/images/5 sao.gif";

        Repeater dataGridView = (Repeater)e.Item.FindControl("dataGridView");

        dataGridView.DataSource = cdp.searchByParentID(long.Parse(DataBinder.Eval(e.Item.DataItem, "ID").ToString()));
        dataGridView.DataBind();
    }

    protected void InitComment()
    {

        dataGridView.DataSource = cdp.searchByProductID(int.Parse(Request.QueryString["id"].ToString()), -1);
        dataGridView.DataBind();
    }
    protected void send_Click(object sender, EventArgs e)
    {
        if (txtCaptcha.Text.Equals(Session["captcha"].ToString(), StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                ALupMart_Comment comment = new ALupMart_Comment();
                comment.PortalId = PortalId;
                comment.ProductID = id;
                comment.Status = true;
                comment.Vote = short.Parse(startValue.Value);
                comment.Name = Server.HtmlEncode(nameTextBox.Text);
                comment.Email = Server.HtmlEncode(emailText.Text);
                comment.Comment = Server.HtmlEncode(contentTextBox.Text);
                comment.CreateDate = DateTime.Now;
                comment.ParentID = -1;
                new ProductDataProvider().InsertComment(comment);
                string stringArt = "alert('Bình luận của bạn đã được gửi.'); ";
                InitComment();

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertScript", stringArt, true);
            }
            catch
            {
                string stringArt = "alert('Không gửi được bình luận, mời bạn thử lại.');";

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertScript", stringArt, true);

            }
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertScript", "alert('Mã bảo vệ không chính xác!');", true);
        }
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //dataGridView.PageIndex = e.NewPageIndex;
        //  id = long.Parse(Request.QueryString["id"]);
        InitComment();
    }


    protected void DataPagerProducts_PreRender(object sender, EventArgs e)
    {

        InitComment();
    }

    protected void InitSPMuaCungNhau(long GroupID)
    {


        string[] strarray = hdfPID.Value.Split(',');
        long[] arrayID = new long[strarray.Length];
        int i = 0;
        foreach (string s in strarray)
        {
            if (!string.IsNullOrEmpty(s))
            {
                arrayID[i] = long.Parse(s);
                i++;
            }
        }
        // Response.Write(GroupID.ToString());

        Array list = new ProductDataProvider().SearchProductByGroupID(GroupID, 1, arrayID);

        countNhom = list.Length - 1;
        if (countNhom >= 0)
        {
            //  if (m1.Items.Count < 1) { divMuacungnhau.Visible = false; }
            lblQ.InnerText = (countNhom + 1).ToString();
            divMuacungnhau.Visible = true;

            m1.DataSource = list;
            m1.DataBind();

            lblTongtien.InnerHtml = ConverLanguageDataProvider.ConvertToCurrency(ConverLanguageDataProvider.ConvertCurrentcy(tongtien.ToString(), Page.Session), Page.Session) + "";
        }
        else
            divMuacungnhau.Visible = false;

    }


    protected void m1DataBound(object sender, RepeaterItemEventArgs e)
    {

        Literal lrlPrice = (Literal)e.Item.FindControl("lrlPrice");
        HiddenField hdfPrice = (HiddenField)e.Item.FindControl("hdfPrice");
        TextBox txtPrice = (TextBox)e.Item.FindControl("txtPrice");
        Literal lrlCong = (Literal)e.Item.FindControl("lrlCong");

        double price = 0, priceNew = 0, discount = 0;
        if (DataBinder.Eval(e.Item.DataItem, "Price") != null)
            price = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Price").ToString());

        //if (DataBinder.Eval(e.Item.DataItem, "Discounts") != null)
        //    priceNew = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Discounts").ToString());

        string vnd = " Vnđ";

        if (discount > 0)
        {
            lrlPrice.Text = "<span class='relate-price'>" + ConverLanguageDataProvider.ConvertToCurrency(ConverLanguageDataProvider.ConvertCurrentcy(priceNew.ToString(), Page.Session), Page.Session) + vnd + "</span> <span class='relate-old-price'>" + ConverLanguageDataProvider.ConvertToCurrency(ConverLanguageDataProvider.ConvertCurrentcy(price.ToString(), Page.Session), Page.Session) + vnd + "</span>";
            hdfPrice.Value = priceNew.ToString();
            tongtien += priceNew;
        }
        else
        {
            lrlPrice.Text = "<span class='relate-price'>" + ConverLanguageDataProvider.ConvertToCurrency(ConverLanguageDataProvider.ConvertCurrentcy(price.ToString(), Page.Session), Page.Session) + "</span>";
            hdfPrice.Value = price.ToString();
            tongtien += price;
        }
        if (e.Item.ItemIndex < countNhom)
            lrlCong.Text = " <div class='relate-seperate-item'><img src='/images/bg-relate-seperate.png'></div>";
    }
    protected void Loai_Command(object sender, CommandEventArgs e)
    {
        hdfPID.Value = hdfPID.Value + "," + e.CommandArgument;
        //ProductClass Product = pd.searchProductByID(int.Parse(Request.QueryString["id"].ToString()), langId);
        InitSPMuaCungNhau(int.Parse(Request.QueryString["id"].ToString()));

    }
    protected void ChangeNumber(object sender, EventArgs e)
    {
        int len = m1.Items.Count;
        for (int count = 0; count < len; count++)
        {
            HiddenField hdfPrice = (HiddenField)m1.Items[count].FindControl("hdfPrice");
            //HiddenField hdfProductID = (HiddenField)m1.Items[count].FindControl("hdfProductID");
            TextBox txtPrice = (TextBox)m1.Items[count].FindControl("txtPrice");
            try
            {
                tongtien += double.Parse(hdfPrice.Value) * int.Parse(txtPrice.Text);
                countNhom += int.Parse(txtPrice.Text);
            }
            catch
            {
            }
        }
        lblQ.InnerText = countNhom.ToString();
        //m1.DataSource = list;
        // m1.DataBind();
        lblTongtien.InnerHtml = ConverLanguageDataProvider.ConvertToCurrency(ConverLanguageDataProvider.ConvertCurrentcy(tongtien.ToString(), Page.Session), Page.Session) + "";

    }
    protected void BuyAll_Click(object sender, EventArgs e)
    {
        ShoppingCart shoppingcart = ProductsFunctions.GetShoppingCart(Page.Session);


        if (shoppingcart == null) shoppingcart = new ShoppingCart();

        if (m1.Items.Count > 0)
        {
            int len = m1.Items.Count;
            for (int count = 0; count < len; count++)
            {
                HiddenField hdfPrice = (HiddenField)m1.Items[count].FindControl("hdfPrice");
                HiddenField hdfProductID = (HiddenField)m1.Items[count].FindControl("hdfProductID");
                HiddenField hdfName = (HiddenField)m1.Items[count].FindControl("hdfName");

                TextBox txtPrice = (TextBox)m1.Items[count].FindControl("txtPrice");
                try
                {
                    OrderRecord record = new OrderRecord();
                    record.productID = int.Parse(hdfProductID.Value);
                    record.count = int.Parse(txtPrice.Text);
                    record.price = double.Parse(hdfPrice.Value);
                    record.productName = hdfName.Value;
                    shoppingcart.InsertShoppingCart(record);
                    ProductsFunctions.SetShoppingCart(Page.Session, shoppingcart);
                }
                catch
                {
                }
            }
        }
        Response.Redirect("http://" + PortalAlias.HTTPAlias + "/order");


    }
    protected string GetDetail(object id, object name)
    {
        return "/" + StringUtil.ConvertUrlString(name.ToString()) + "-p" + id;

    }
}
