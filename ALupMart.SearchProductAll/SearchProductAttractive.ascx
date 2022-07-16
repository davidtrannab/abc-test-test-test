<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchProductAttractive.ascx.cs"
    Inherits="View" %>
<asp:Label runat="server" ID="titleHyper" />
<asp:Repeater runat="server" ID="Repeater1" OnItemDataBound="Rpt_ItemCatalogDataBound">
    <ItemTemplate>
        <div class="cat-height  col-sm-12" runat="server" id="divCatalog">

            <div class="name subcatname" style="background-color: #1d5284; height: 30px; border-radius: 5px; text-align: center; padding-top: 5px; background-image: linear-gradient(#1d5284,#eee, #1d5284, #1d5284, #1d5284 , #1d5284 , #1d5284 , #1d5284);">

                <a style="color: White; font-size: 16px; font-weight: bold; text-transform: uppercase; text-decoration: none;"
                    href="/product?cat_id=<%#Eval("CatalogID")%>">
                    <%#Eval("CatalogName")%></a>

            </div>


            <div class="content_th">
                <div class="row">
                    <div class="box-product">
                        <ul style="float:left;">
                <asp:Repeater runat="server" ID="rptProduct" OnItemDataBound="Rpt_ItemDataBound">
                    <ItemTemplate>
                        <div class="product col-sm-3" style="padding-bottom:10px;"
                            <li class="first-in-line"> 
                                <div class="padding pviet" style="border:1px solid #cecece; padding:10px; padding-bottom:0px;">
                            <div class="bg_imgpr" style="border: 1px solid #ebebeb;">
                                <a href="#"><a href='<%#GetUrl(Eval("ProductID"),Eval("ProductName"))%>' title='<%#Eval("ProductName")%>'>
                                    <asp:Image ID="Image1" style="max-height: 174px;" runat="server" ImageUrl='<%#Functions.GetUrlImage(Eval("ImageSource"),urlHost)%>'
                                        AlternateText='<%#Server.HtmlDecode(Eval("ProductName").ToString())%>' />
                                </a>
                            </div>
                            <div class="cl">
                            </div>
                            <div class="name_pr" style="text-align: center; padding-top: 15px;">
                                <a style="text-transform: uppercase; font-size: 12px; font-weight: bold; color: #1d5284; text-decoration: none;" href='<%#GetUrl(Eval("ProductID"),Eval("ProductName"))%>' title='<%#Eval("ProductName")%>'>
                                    <%#Eval("ProductName")%>
                                </a>
                            </div>
                            <div class="cl">
                            </div>

                            <div class="price_goc" id="PriceOldDiv" runat="server" style="display: none;">
                                <%#String.Format("{0:#,###}", double.Parse(Eval("PriceMarket").ToString())).Replace(",", ".")%><span
                                    style="margin-left: 5px;">VNĐ</span>
                            </div>
                            <div class="cl">
                            </div>
                            <div class="price_giam" runat="server" id="PriceDiv" style="display: none;">
                                <%#String.Format("{0:#,###}", double.Parse(Eval("NewPrice").ToString())).Replace(",", ".")%><span
                                    style="margin-left: 5px;">VNĐ</span>
                            </div>
                                    </div>
                                </li>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                            </ul>
                </div>
                    </div>
            </div>
            <div style="clear: both">
            </div>

        </div>
    </ItemTemplate>
</asp:Repeater>

<div class="cat-height  col-sm-12" runat="server" id="divCatalog" visible="false">

    <div class="name subcatname" style="background-color: #1d5284; height: 30px; border-radius: 5px; text-align: center; padding-top: 5px; background-image: linear-gradient(#1d5284,#eee, #1d5284, #1d5284, #1d5284 , #1d5284 , #1d5284 , #1d5284);">

       
        <a style="color: White; font-size: 16px; font-weight: bold; text-transform: uppercase; text-decoration: none;">
            
         <asp:Literal runat="server" ID="lrlTitle" />
        </a>



    </div>


    <div class="content_th">
                <div class="row ">
                    <div class="box-product">
                        <ul style="float:left;">

        <asp:Repeater runat="server" ID="rptCatalog">
            <ItemTemplate>
                <div class="product col-sm-3" style="padding-bottom:10px;"
                            <li class="first-in-line"> 
                                <div class="padding pviet" style="border:1px solid #cecece; padding:10px; padding-bottom:0px;">
                
                    <div class="bg_imgpr" style="border: 1px solid #ebebeb;">
                        <a href="#"><a href='<%#GetUrl1(Eval("CatalogID"),Eval("CatalogName"))%>' title='<%#Eval("CatalogName")%>'>
                            <asp:Image ID="Image1" style="max-height:174px;" runat="server" ImageUrl='<%#Functions.GetUrlImage(Eval("ImageSource"),urlHost)%>'
                                AlternateText='<%#Server.HtmlDecode(Eval("CatalogName").ToString())%>' />
                        </a>
                    </div>
                    <div class="cl">
                    </div>
                    <div class="name_pr" style="text-align: center; padding-top: 15px;">
                        <a style="text-transform: uppercase; font-size: 12px; font-weight: bold; color: #1d5284; text-decoration: none;" href='<%#GetUrl1(Eval("CatalogID"),Eval("CatalogName"))%>' title='<%#Eval("CatalogName")%>'>
                            <%#Eval("CatalogName")%>
                        </a>
                    </div>
                    <div class="cl">
                    </div>
</div>
                   </li>
                    </div>
                
            </ItemTemplate>
        </asp:Repeater>
                            </ul>
                        </div>
</div>
    </div>
    <div style="clear: both">
    </div>

</div>
