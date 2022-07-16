<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductDetail.ascx.cs" Inherits="View" %>
<%@ Register TagPrefix="tht" TagName="MENUPRODUCT" Src="~/DesktopModules/ALupMart.MenuProduct1/ViewMenu.ascx" %>
<%@ Register TagPrefix="tht" TagName="MENUTHUONGHIEU" Src="~/DesktopModules/ALupMart.ThuongHieu/SliderLogo.ascx" %>

<script src='/Portals/_default/Skins/Skin/css/rating/jquery.rating.js' type="text/javascript"
    language="javascript"></script>

<link href='/Portals/_default/Skins/Skin/css/rating/jquery.rating.css' type="text/css"
    rel="stylesheet" />
<div id="fb-root"></div>
<script>(function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = 'https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v3.0';
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));</script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.auto-submit-star').rating({
            callback: function (value, link) {
                document.getElementById("dnn_ctr1211_productdetail_startValue").value = value;
            }
        });

    });</script>
<style>
    .zoom-desc {
        float: left;
        text-align: center;
    }

    .customer_price_item {
        font-size: 30px;
        color: #c40000;
    }

    .colName {
        float: left;
        width: 120px;
    }

        .colName span {
            line-height: 35px;
        }

    .col_dtP {
        float: left;
        line-height: 25px;
        width: 280px;
    }

    zoom-desc img {
        border: 1px solid #d9d9d9;
        height: 56px;
        margin: 0 6px 5px 0;
        width: 58px;
    }

    .y-sub-imgColor-details {
        border: 1px solid #ddd;
        float: left;
        margin-right: 5px !important;
        overflow: hidden;
        padding: 2px;
        width: 57px !important;
        height: 50px;
    }

    .sizeProducts {
        border: 1px solid #ddd;
        color: #909090;
        display: block;
        float: left;
        margin: 0px 10px 15px 0;
        padding: 2px 5px;
        text-align: center;
    }

    .project_list_title {
        float: left;
        color: #696969;
    }

    .col_dtP .active img, .active {
        border-color: red;
    }

    .qc {
        margin-bottom: 15px;
    }

    .att-thuoctinh li {
        border-bottom: 1px solid #f0f0f2;
        padding-bottom: 3px;
    }
</style>
<div class="detail">
    <main class="clearfix m_bottom_10 m_xs_bottom_10 bg_white2">
        <div class="product_preview f_left f_xs_none wrapper m_xs_bottom_15" style="display: none;">
            <div class="d_block relative r_image_container" style="margin-bottom: 10px">

                <img id="zoom" src='/Portals/<%=PortalId %>/Small_<%=ImageSource %>' alt="" data-zoom-image='/Portals/<%=PortalId %>/<%=ImageSource%>'>
                <div runat="server" id="sales" visible="false" class="product_label fs_ex_small circle color_white bg_lbrown t_align_c vc_child tt_uppercase"><i class="d_inline_m">Sale!</i></div>
            </div>
            <!--thumbnails-->
            <div class="clear">
            </div>
            <div class="product_thumbnails_wrap relative m_bottom_3" runat="server" id="imgCon">
                <div class="owl-carousel" id="thumbnails" data-nav="thumbnails_product_" data-owl-carousel-options='{
											"responsive" : {
												"0" : {
													"items" : 3
												},
												"321" : {
													"items" : 4
												},
												"769" : {
													"items" : 2
												},
												"992" : {
													"items" : 3
												}
											},
											"stagePadding" : 40,
											"margin" : 10,
											"URLhashListener" : false
										}'>
                    <asp:Repeater runat="server" ID="imageRepeater" OnItemDataBound="Rpt_ItemImageDataBound">
                        <ItemTemplate>

                            <a href="#" data-image="/Portals/<%=PortalId %>/Small_<%#(Eval("ImageSource"))%>" data-zoom-image="/Portals/<%=PortalId %>/<%#(Eval("ImageSource"))%>" class="d_block">
                                <img src="/Portals/<%=PortalId %>/Small_<%#Eval("ImageSource")%>" alt="">
                            </a>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <button class="thumbnails_product_prev black_hover button_type_4 grey state_2 tr_all d_block vc_child"><i class="fa fa-angle-left d_inline_m"></i></button>
                <button class="thumbnails_product_next black_hover button_type_4 grey state_2 tr_all d_block vc_child"><i class="fa fa-angle-right d_inline_m"></i></button>
            </div>
            <style>
                .product_thumbnails_wrap .owl-carousel .owl-item img {
                    display: block;
                    max-width: 100%;
                    width: auto;
                    -webkit-transform-style: preserve-3d;
                    max-height: 100px;
                }
            </style>
            <div class="clear">
            </div>

            <style>
                .fl {
                    float: left;
                }

                .cart-product-relate {
                }
            </style>
            <%--<asp:Literal runat="server" ID="divKhuyenMai" />--%>
            <asp:Literal runat="server" ID="Camket" />

        </div>
        <div class="f_left f_xs_none" style="width:100%;">
            <div class="wrapper">
                <h1 class="second_font m_bottom_3 f_left product_title customer_title_item" style="margin-bottom: 10px; margin-top: 0; font-size: 20px; text-transform: capitalize;">
                    <asp:Literal runat="server" ID="ProductName" />
                </h1>
                <div class="clear">
                </div>
            </div>
            <div class="relative m_bottom_18" style="display: none;">
                <ul class="rating_list hr_list d_inline_m tr_all m_right_5">
                    <asp:Literal runat="server" ID="initStar" />
                </ul>
                <span class="color_light d_inline_m m_top_2">
                    <a href="?v=com#tab2" class="sc_hover fs_medium fw_light">
                        <asp:Literal runat="server" ID="totalCom" />
                        Đánh giá</a> | <a href="?v=com#tab2" class="color_dark sc_hover fs_medium fw_light">Đánh giá sản phẩm này</a>
                </span>
            </div>
            <div class="clear">
            </div>

            <div style="padding: 0px 0 15px 0;">
                <div class="fl">
                    <div class="fb-like" data-href="http://<%=PortalAlias.HTTPAlias+Request.RawUrl%>" data-layout="button_count" data-action="like" data-show-faces="false" data-share="true"></div>
                </div>
                <div class="fl" style="margin: 0 0 0 5px; width: 90px">
                    <a href="https://twitter.com/share" class="twitter-share-button" data-url="http://<%=PortalAlias.HTTPAlias+Request.RawUrl%>">Tweet</a>
                    <script>                                            !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");
                    </script>
                </div>
                <div class="fl" style="width: 65px;">
                    <script src="https://apis.google.com/js/plusone.js"></script>
                    <g:plusone size="medium"></g:plusone>
                </div>
                <div class="cl">
                </div>
            </div>

            <div class="clear">
            </div>
            <div class="tm-fcs-panel" style="display: none;">
                <asp:Literal runat="server" ID="lrlPriceMarket" />
                <asp:Literal runat="server" ID="lrlPrice" />




            </div>
            <ul class="m_bottom_14 att-thuoctinh" style="display: none;">
                <li class="m_bottom_3"><span class="project_list_title second_font d_inline_b" style="font-size: 16px; font-weight: bold;">Thương hiệu:</span>
                    <span class=" fw_light" style="font-size: 18px; margin-left: 1px;">
                        <asp:HyperLink runat="server" ID="HL_Thuonghieu" /></span>
                </li>


            </ul>
            <div id="masp" style="margin-bottom: 10px; color: #565656; display: none;">
                <asp:Literal runat="server" ID="Li_code" />
            </div>
            <ul class="m_bottom_14 att-thuoctinh" style="display: none;">

                <asp:Literal runat="server" ID="Literal1" />

            </ul>

            <ul class="m_bottom_14">
            </ul>

            <%--  <hr class="divider_light m_bottom_15">--%>
            <p class="fw_light m_bottom_14 color_grey">
                <asp:Literal runat="server" ID="Lb_Mieuta" />
            </p>
            <div class="product_options" style="display: none;">
                <b class="second_font d_block m_bottom_10">Lựa chọn sản phẩm</b>
                <asp:Literal runat="server" ID="otherAttributeOrderLiteral" />

                <div class="ProductDetailsGrid ProductAddToCart" runat="server" id="OrderProductDiv">
                    <div style="clear: both"></div>
                    <hr class="divider_light" style="margin-top: 20px;" />

                    <footer class="bg_grey_light_2" style="padding-top: 10px">

                        <div class="fs_big second_font m_bottom_17"></div>
                        <input type="hidden" id="price" value="<%=price %>" />

                        <div class="clearfix">
                            <div class="quantity clearfix t_align_c f_left f_md_none m_right_10 m_md_bottom_3">
                                <button class="f_left d_block minus black_hover tr_all bg_white">-</button>
                                <input name="qty" type="text" id="qty" style="width: 50px;" class="f_left color_light" maxlength="2" value="1">
                                <button class="f_left d_block black_hover tr_all bg_white">+</button>
                            </div>
                            <br class="d_md_block d_none">
                            <div runat="server" id="buyProductDiv">
                                <a href="javascript:return;" onclick="addProduct(<%=id%>,$('#qty').val())" data-popup="#add_to_cart_popup" data-popup-transition-in="bounceInUp" data-popup-transition-out="bounceOutUp" class="button_type_2 d_block f_sm_none m_sm_bottom_3 t_align_c lbrown state_2 tr_all second_font fs_medium tt_uppercase f_left m_right_3 product_button">
                                    <i class="fa fa-shopping-cart d_inline_m m_right_9"></i>Đặt hàng
                                </a>
                            </div>
                            <br class="d_sm_block d_none">
                    </footer>
                </div>
            </div>
        </div>
    </main>
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <div runat="server" id="divMuacungnhau" visible="false">
                <div class="cart-product-relate" style="margin-bottom: 30px; border: 1px solid #e5e5e5">
                    <div class="tabs-1">
                        <div class="tab-heading">
                            Sản phẩm thường mua cùng nhau
                        </div>
                        <div class="col-sm-9">
                            <div class="relate-content">
                                <asp:HiddenField runat="server" ID="hdfPID" />
                                <div class="owl-carousel" data-nav="product-relate_" data-owl-carousel-options='{
									"stagePadding" : 15,
									"margin" : 15,
									"responsive" : {
											"0" : {
												"items" : 1
											},
											"470" : {
												"items" : 2
											},
											"992" : {
												"items" : 4
											}
										}
									}'>
                                    <asp:Repeater ID="m1" runat="server" OnItemDataBound="m1DataBound">
                                        <ItemTemplate>
                                            <figure>
                                                <div class="relate-item">
                                                    <div style="width: 130px; height: 130px; overflow: hidden; text-align: center">
                                                        <a href='<%#GetDetail(Eval("ProductID"),Eval("ProductName"))%>'>
                                                            <img src='/Portals/2/Small_<%# Eval("ImageSource").ToString().Replace("~","")%>' /></a>
                                                    </div>
                                                    <p class="relate-item-name">
                                                        <a href='<%#GetDetail(Eval("ProductID"),Eval("ProductName"))%>'>
                                                            <%#Eval("ProductName")%></a>
                                                    </p>
                                                    <div class="relate-item-price">
                                                        <%-- <span class="relate-price">850.000đ</span> <span class="relate-old-price">850.000đ</span>--%>
                                                        <asp:Literal runat="server" ID="lrlPrice" />
                                                    </div>
                                                    <asp:HiddenField runat="server" ID="hdfProductID" Value='<%#Eval("ProductID") %>' />
                                                    <asp:HiddenField runat="server" ID="hdfName" Value='<%#Eval("ProductName") %>' />

                                                    <asp:HiddenField runat="server" ID="hdfPrice" />
                                                    <asp:TextBox runat="server" ID="txtPrice" Text="1" class="relate-qty-input" AutoPostBack="true"
                                                        OnTextChanged="ChangeNumber" Visible="false" />
                                                    <asp:ImageButton CssClass="x-icon" ToolTip="Bỏ sản phẩm này" runat="server" ID="loai"
                                                        ImageUrl="~/images/X_icon.png" OnCommand="Loai_Command" CommandArgument='<%#Eval("ProductID")%>' />

                                                </div>
                                                <asp:Literal runat="server" ID="lrlCong" />
                                            </figure>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <button class="product-relate_prev black_hover button_type_4 grey state_2 tr_all d_block vc_child">
                                    <i class="fa fa-angle-left d_inline_m"></i>
                                </button>
                                <button class="product-relate_next black_hover button_type_4 grey state_2 tr_all d_block vc_child">
                                    <i class="fa fa-angle-right d_inline_m"></i>
                                </button>
                                <div style="clear: both;">
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3" style="padding-left: 0;">
                            <div class="cart-relate">
                                <div class="equal">
                                    =
                                </div>
                                <div class="cart-relate-left" style="padding-top: 13px;">
                                    <span style="color: #464646;">Bạn đã chọn: <span class="relate-qty-sum" style="font-size: 18px; color: #006600; font-weight: bold;"
                                        runat="server" id="lblQ"></span>&nbsp; sản phẩm</span><br>
                                    <span style="line-height: 38px; font-size: 18px; color: #006600;"><b>Tổng tiền:</b>&nbsp;</span><span
                                        class="relate-total-price" style="color: #006600; font-size: 18px; font-weight: bold;"
                                        runat="server" id="lblTongtien"> </span>
                                </div>
                                <div class="cart-relate-right" style="padding-top: 11px;">
                                    <asp:LinkButton CssClass=" button_type_2 d_block f_sm_none m_sm_bottom_3 t_align_c lbrown state_2 tr_all second_font fs_medium tt_uppercase f_left m_right_3 product_button"
                                        runat="server" OnClick="BuyAll_Click" Text="Mua ngay"> <i class="fa fa-shopping-cart d_inline_m m_right_9"></i>Mua ngay</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    function pageLoad() {
                        owlCarousel();
                    }
                </script>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row_part2">

        <asp:Literal runat="server" ID="lrlQc1" Visible="false" />

        <asp:Literal runat="server" ID="lrlQc2" Visible="false" />

        <div class="col-sm-12 bg_white2">
            <div style="font-weight: bold;"><%= ALup.Language.ConverLanguageDataProvider.ConvertLanguageResources("Chi tiết sản phẩm",Session) %>
                
            </div>
            <div runat="server" id="descriptionDiv">
                <!--tabs-->
                <div class="	">
                    <nav class="second_font menu_tabs2">
                        <ul class="hr_list">
                            <li class="m_right_3"><a href="#tab1" class="color_light border_light_3 d_block">
                                <h2>Chi tiết</h2>
                            </a></li>
                            <li class="m_right_3" style="display: none;"><a href="#tab2" class="color_light border_light_3 d_block">
                                <h2>Thành phần</h2>
                            </a></li>
                            <li class="m_right_3" style="display: none;"><a href="#tab3" class="color_light border_light_3 d_block">
                                <h2>Cách sử dụng</h2>
                            </a></li>
                            <li class="m_right_3"><a href="#tab4" class="color_light border_light_3 d_block">
                                <h2>Khuyến mãi</h2>
                            </a></li>
                        </ul>
                    </nav>
                    <hr class="d_xs_none">
                    <div id="tab1" class="fw_light tab_content" style="display: none;">
                        <div class="ttchung">
                            <div class="">
                                <div class="row1 clearfix">
                                    <div class="attrs">
                                        <div class="j_Cate attr" style="border-color: #E6E2E1 #E6E2E1 #D1CCC7;">
                                            <asp:Literal runat="server" ID="lrlAttribute" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <style>
                                .ttchung .title-attr {
                                    display: inline-block;
                                    margin-right: 10px;
                                    color: #000;
                                }

                                .ttchung ul {
                                    margin-bottom: 20px;
                                }

                                .ttchung li {
                                    display: inline-block;
                                    width: 20%;
                                    padding: 5px;
                                }
                            </style>
                        </div>

                    </div>
                    <div id="tabreview" runat="server">
                        <div class="d_table m_bottom_5 w_full">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-8 v_align_m d_table_cell f_none">
                                <h5 class="second_font color_dark tt_uppercase fw_light d_inline_m m_bottom_4">Bình luận</h5>
                            </div>
                        </div>
                        <hr class="divider_bg m_bottom_15">
                        <div id="review">
                            <div class="std">
                                <div class="fb-comments" data-href="http://<%=PortalAlias.HTTPAlias+Request.RawUrl%>" data-numposts="10" data-width="100%" data-colorscheme="light"></div>
                            </div>
                        </div>
                    </div>

                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <div class="tabs">
                                    <div class="tab-heading">
                                        Cảm nhận của bạn về sản phẩm này
                                    </div>
                                    <div class="tab-content">
                                        <div id="pWriteComment" class="BlockContent FormContainer VerticalFormContainer center"
                                            style="font-size: 12px;">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                <ContentTemplate>
                                                    <div class="Comment">
                                                        <asp:ListView ID="dataGridView" runat="server" OnItemDataBound="InitItem_DataBound">
                                                            <ItemTemplate>
                                                                <div style="clear: both">
                                                                </div>
                                                                <div style="position: relative; padding-bottom: 10px;">
                                                                    <div class="Name">
                                                                        <%#Eval("Name")%><span style="font-weight: normal"> (<%#DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy")%>)</span>
                                                                    </div>
                                                                    <div style="margin: 4px 0; color: #888">
                                                                        <asp:Image runat="server" ID="img" ImageUrl="/images/3 sao.gif" class="imgStarHome" />
                                                                        <%-- <%#Eval("NguoiCungYK")%>/
                                                                        <%#int.Parse(Eval("KhongCungYK").ToString())+int.Parse(Eval("NguoiCungYK").ToString())%>
                                                                        người có cùng ý kiến này--%>
                                                                    </div>
                                                                    <div class="Content">
                                                                        <%#Eval("Comment")%>
                                                                    </div>
                                                                    <asp:Repeater ID="dataGridView" runat="server">
                                                                        <ItemTemplate>
                                                                            <div style="position: relative; padding-left: 25px; margin-top: 10px;">
                                                                                <div class="Name" style="margin-bottom: 3px">
                                                                                    <%#Eval("Name")%><span style="font-weight: normal"> (<%#DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy")%>)</span>
                                                                                </div>
                                                                                <div class="Content">
                                                                                    <%#Eval("Comment")%>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>

                                                                    </asp:Repeater>
                                                                    <p style="margin-top: 15px; display: none;">
                                                                        Bạn có cùng ý kiến này không?
                                                                        <label style="color: #004C94; padding-left: 10px; display: none;" id='lb<%#Eval("ID")%>'>
                                                                            Cảm ơn bạn đã đóng góp ý kiến
                                                                        </label>
                                                                        <input type="button" id='ag<%#Eval("ID")%>' class="imgAgree" onclick="javascript: checkvoteAgree(<%#Eval("ID")%>);"
                                                                            alt="Đồng ý" value="Có" style="margin-right: 5px"><input type="button" id='dg<%#Eval("ID")%>'
                                                                                class="imgDegree" value="Không" style="" onclick="javascript: checkvoteDeagree(<%#Eval("ID")%>);"
                                                                                alt="Không đồng ý">
                                                                    </p>
                                                                    <a id="imgreply" onclick="javascript:Recomment(recomment<%#Eval("ID")%>);" class="lnkreplycom-<%#Eval("ID")%>">
                                                                        <span class="hB T-I-J3" role="button" alt=""></span>Trả lời</a>
                                                                </div>
                                                                <div id='recomment<%#Eval("ID")%>' class="box-recomment-<%#Eval("ID")%> reboxcom"
                                                                    style="display: none;">
                                                                    <label class="lblmess681" class="relblmess">
                                                                    </label>
                                                                    <input type="text" maxlength="100" id='name<%#Eval("ID")%>' class="rename" value="<%=strHT%>"
                                                                        onblur="javascript:if(this.value == '')this.value='<%=strHT%>';" onfocus="javascript:if(this.value == '<%=strHT%>') this.value='';">
                                                                    <textarea value="<%=strCM %>" id='txtContent<%#Eval("ID")%>' class="recomment" onblur="javascript:if(this.value == '')this.value='<%=strCM %>';"
                                                                        onfocus="javascript:if(this.value == '<%=strCM %>') this.value='';"><%=strCM %></textarea><br>
                                                                    <input type="button" value="Gửi" style="font-weight: bold; color: black;" class="btnrecomment" id='btnrecomment<%#Eval("ID")%>'
                                                                        onclick="javascript: Reply(<%#Eval("ID")%>,<%#Eval("ProductID")%>, '<%=strHT %>    ','<%=strCM %>    ','Bạn phải nhập họ tên','Bạn phải nhập cảm nhận');">
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div style="text-align: right; padding: 10px 0; display: none;">
                                                            <asp:DataPager runat="server" ID="AfterListDataPager" PagedControlID="dataGridView"
                                                                PageSize="5" OnPreRender="DataPagerProducts_PreRender">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="false" ShowNextPageButton="true"
                                                                        ShowPreviousPageButton="true" NextPageText="Tiếp" PreviousPageText="Trước" />
                                                                    <asp:NumericPagerField ButtonCount="5" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <dl runat="server" id="ContentCommentDiv" style="line-height: 22px; margin-top: 10px;">
                                                <dt class="translated">Đánh giá của bạn:</dt>
                                                <dd>
                                                    <input name="star2" type="radio" class="auto-submit-star" value="1" />
                                                    <input name="star2" type="radio" class="auto-submit-star" value="2" />
                                                    <input name="star2" type="radio" class="auto-submit-star" value="3" checked="checked" />
                                                    <input name="star2" type="radio" class="auto-submit-star" value="4" />
                                                    <input name="star2" type="radio" class="auto-submit-star" value="5" />
                                                    <asp:HiddenField runat="server" ID="startValue" Value="3" />

                                                    <script type="text/javascript" language="javascript">
                                                        $(function () {

                                                        });
                                                    </script>

                                                </dd>

                                                <div style="clear: both">
                                                </div>
                                                <dt class="translated">Tên của bạn:</dt>
                                                <dd>
                                                    <asp:TextBox runat="server" ID="nameTextBox" Style="width: 80%;" class="NormalTextBox"
                                                        MaxLength="100" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Bold="true"
                                                        ForeColor="Red" ValidationGroup="binhluan" ControlToValidate="nameTextBox" Text="*"
                                                        Display="Dynamic" />
                                                </dd>
                                                <dt class="translated">Email:</dt>
                                                <dd>
                                                    <asp:TextBox runat="server" ID="emailText" Style="width: 80%;" class="NormalTextBox"
                                                        MaxLength="200" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Font-Bold="true"
                                                        ValidationGroup="binhluan" ControlToValidate="emailText" Text="*" ForeColor="Red"
                                                        Display="Dynamic" />
                                                    <asp:RegularExpressionValidator ID="emailRegularExpressionValidator" runat="server"
                                                        ForeColor="Red" ValidationExpression="([\w\d\-\.]+)@{1}(([\w\d\-]{1,67})|([\w\d\-]+\.[\w\d\-]{1,67}))\.(([a-zA-Z\d]{2,4})(\.[a-zA-Z\d]{2})?)"
                                                        ControlToValidate="emailText" Display="Dynamic" ErrorMessage="*"></asp:RegularExpressionValidator>
                                                </dd>
                                                <dt class="translated">Nội dung:</dt>
                                                <dd>
                                                    <asp:TextBox runat="server" ID="contentTextBox" Style="width: 80%; float: left;"
                                                        class="NormalTextBox" Rows="6" Columns="20" TextMode="MultiLine" MaxLength="4000" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Font-Bold="true"
                                                        ForeColor="Red" ControlToValidate="contentTextBox" Text="*" Display="Dynamic"
                                                        ValidationGroup="binhluan" />
                                                </dd>
                                                <div style="clear: both">
                                                </div>
                                                <div>
                                                    <dt class="translated">Mã xác nhận:</dt>
                                                    <dd>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                            <ContentTemplate>
                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                                    <ProgressTemplate>
                                                                        <center style="position: fixed; top: 43%; left: 49%;">
                                                                            <img alt="Loading..." src="/images/loading.gif" />
                                                                            <br />
                                                                            Đang tải dữ liệu...
                                                                        </center>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                                <asp:TextBox runat="Server" ID="txtCaptcha" Width="100px" Style="vertical-align: top;"
                                                                    tht="mabaove" />
                                                                <asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/Captcha.aspx" Style="vertical-align: top;" />
                                                                <asp:ImageButton ID="imbReLoad" runat="server" ImageUrl="~/images/refesh7.png" />
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </dd>
                                                </div>
                                                <div style="clear: both">
                                                </div>
                                                <dd class="translated" style="margin: 10px 170px;">
                                                    <asp:Button CssClass="button" runat="server" ID="send" OnClick="send_Click" ValidationGroup="binhluan"
                                                        Text="Gửi bình luận" />
                                                </dd>
                                            </dl>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tab2" class="tab_content">
                    <div runat="server" id="ThanhPhan">
                    </div>
                </div>
                <div id="tab3" class="tab_content">
                    <div runat="server" id="Cachsudung">
                    </div>
                </div>
                <div id="tab4" class="tab_content">
                    <div runat="server" id="divKhuyenMai">
                    </div>
                </div>
            </div>
        </div>
        <div class="d_table m_bottom_5 w_full">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-8 v_align_m d_table_cell f_none">
                <h5 class="second_font color_dark tt_uppercase fw_light d_inline_m m_bottom_4"><%= ALup.Language.ConverLanguageDataProvider.ConvertLanguageResources("Sản phẩm khác",Session) %></h5>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-4 t_align_r d_table_cell f_none">
                <!--carousel navigation-->
                <div class="clearfix d_inline_b">
                    <button class="rp_prev black_hover button_type_4 grey state_2 tr_all d_block f_left vc_child m_right_5">
                        <i class="fa fa-angle-left d_inline_m"></i>
                    </button>
                    <button class="rp_next black_hover button_type_4 grey state_2 tr_all d_block f_left vc_child">
                        <i class="fa fa-angle-right d_inline_m"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <hr class="divider_bg m_bottom_15">
    <div class="row">
        <!--carousel-->
        <div class="owl-carousel" data-nav="rp_" data-owl-carousel-options='{
									"stagePadding" : 15,
									"margin" : 30,
									"responsive" : {
											"0" : {
												"items" : 1
											},
											"470" : {
												"items" : 2
											},
											"992" : {
												"items" : 4
											}
										}
									}'>
            <asp:Repeater runat="server" ID="rptProduct" OnItemDataBound="Rpt_ItemDataBound">
                <ItemTemplate>
                    <figure class="relative r_image_container c_image_container qv_container" style="width: auto;">
                        <div class="relative m_bottom_15">
                            <a href='<%#GetUrl(Eval("ProductID"),Eval("ProductName"))%>' title='<%#Eval("ProductName")%>'>
                                <img src="/Portals/<%=PortalId%>/Small_<%#Eval("ImageSource")%>" title="<%#Eval("ProductName")%>"
                                    alt="<%#Eval("ProductName")%>" />
                            </a>
                        </div>
                        <figcaption class="t_align_c">
                            <ul>
                                <li>
                                    <a class="second_font sc_hover" href='<%#GetUrl(Eval("ProductID"),Eval("ProductName"))%>' title='<%#Eval("ProductName")%>'>
                                        <h3 style="font-weight:bold; font-size: 12px;color: #1d5284; "><%#Eval("ProductName")%></h3>
                                    </a>
                                </li>

                              

                            </ul>
                        </figcaption>
                    </figure>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <script>
        function owlCarousel() {



            $('.owl-carousel').each(function () {



                var _this = $(this),

                    options = _this.data('owl-carousel-options') ? _this.data('owl-carousel-options') : {},

                    buttons = _this.data('nav'),

                    config = $.extend(options, {

                        dragEndSpeed: 500,

                        smartSpeed: 500

                    });



                var owl = _this.owlCarousel(config);



                $('.' + buttons + 'prev').on('click', function () {

                    owl.trigger('prev.owl.carousel');

                });

                $('.' + buttons + 'next').on('click', function () {

                    owl.trigger('next.owl.carousel');

                });

            });

            $(".detail button").on('click', function () {

                return false;

            });



        }


        function tab() {
            if ($('.tabs').length) {

                $('.tabs').easytabs({

                    tabActiveClass: 'color_dark',

                    tabs: '> nav > ul > li',

                    updateHash: false

                }).bind('easytabs:after', function () {

                    $('.tabs').find('.tooltip_container').tooltip('.tooltip').tooltip('.tooltip');

                });

            }
        }

        owlCarousel();
       // tab();





    </script>

    <script type="text/javascript">
        function addProduct(id, qt) {
            var lengthAttr = parseInt(document.getElementById('lengthAtrr').value);

            var attrSelect = '';
            for (var i = 0; i < lengthAttr; i++) {
                attrSelect += document.getElementById('attr' + i).value + ' ';
            }

            var price = $("#price").val();

            select(id, qt, attrSelect, price);

        }
        function addwishlist(id, userid) {

            selectwishlist(id, userid);
        }
        function SelectAttr(name, itemSelect, i, price, priceDisplay, code) {

            document.getElementById('attr' + i).value = name + ": " + itemSelect;

            if (price > 0) {
                $(".customer_price_item").text(priceDisplay);
                $("#price").val(price);
            }

            $("#masp").html('Mã sản phẩm: ' + code);

        }
        function SelectAttr2(name, itemSelect, i, j, price, priceDisplay) {

            document.getElementById('attr' + i).value = name + ": " + itemSelect;

            if (price > 0) {
                $(".customer_price_item").text(priceDisplay);
                $("#price").val(price);
            }

            $(".item" + i).removeClass("active");
            $("#item" + i + j).addClass("active");

        }

    </script>

    <div style="clear: both">
    </div>
</div>
