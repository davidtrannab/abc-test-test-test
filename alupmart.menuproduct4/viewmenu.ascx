<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewMenu.ascx.cs" Inherits="ALupMartV2.Manager.ViewMenu" %>

<div class="cl"></div>


<div class="row" style="margin-top: 10px">
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-8 v_align_m d_table_cell">
            <div id="menu-icon" style="padding-left: 0; color: #1d5284; font-weight: bold; font-size: 15px; float: left;"><%= ALup.Language.ConverLanguageDataProvider.ConvertLanguageResources("SẢN PHẨM ",Session) %></div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-4 t_align_r d_table_cell">
            <!--carousel navigation-->
            <div class="clearfix d_inline_b" style="margin-top: 10px;">
                <button type="button" class="rp_prev black_hover button_type_4 grey state_2 tr_all d_block f_left vc_child m_right_5">
                    <i class="fa fa-angle-left d_inline_m"></i>
                </button>
                <button type="button" class="rp_next black_hover button_type_4 grey state_2 tr_all d_block f_left vc_child">
                    <i class="fa fa-angle-right d_inline_m"></i>
                </button>
            </div>
        </div>
        <div class="cl"></div>
        <div class="owl-carousel" data-nav="rp_" data-owl-carousel-options='{
									"stagePadding" : 0,
									"margin" : 30,
									"responsive" : {
											"0" : {
												"items" : 1
											},
											"470" : {
												"items" : 3
											},
											"992" : {
												"items" : 5
											}
										}
									}'>
            <asp:Repeater runat="server" ID="rptItemMenu" OnItemDataBound="Rpt_ItemRootDataBound">
                <ItemTemplate>
                    <div class="col-lg-12" style="background: #1d5284;border: 1px solid #1d5284;">
                        <div class="row pviet">
                            <div class="img">
                                <asp:HyperLink ID="ItemMenuHyperLink" runat="server">
                    <img src="/Portals/2/Small_<%#Eval("ImageSource")%>" title="<%#Eval("CatalogName")%>" />
                          
                            <h2 style="font-family: Tahoma; font-size: 12px; text-align: center; font-weight: bold; color: #fff;"><%#Eval("CatalogName")%></h2>

                                </asp:HyperLink>
                            </div>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
<script>
    $("#nav .fa-angle-down").click(function () {
        $(this).parent().children("ul").toggle("slow");
    });
</script>

<script>
    function owlCarousel() {



        $('.owl-carousel').each(function () {



            var _this = $(this),

                options = _this.data('owl-carousel-options') ? _this.data('owl-carousel-options') : {},

                buttons = _this.data('nav'),

                config = $.extend(options, {

                    dragEndSpeed: 500,
                    autoplay: true,
                    loop: true,

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



    owlCarousel();





</script>
