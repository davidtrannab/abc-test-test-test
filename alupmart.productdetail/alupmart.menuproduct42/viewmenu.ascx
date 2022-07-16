<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewMenu.ascx.cs" Inherits="ALupMartV2.Manager.ViewMenu" %>


<div class="menu-box open">
    <a href="javascript:void(0)" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="true">
        <div class="sidebar-box-heading" style="background-color:#1d5284;height:30px;border-radius: 5px; text-align: center;padding-top:5px;background-image: linear-gradient(#1d5284,#eee, #1d5284, #1d5284, #1d5284 , #1d5284, #1d5284, #1d5284);">
            <h4><%= ALup.Language.ConverLanguageDataProvider.ConvertLanguageResources("Danh mục",Session) %> </h4>
        </div>
    </a>


    <ul class="dropdown-menu" style="margin-bottom:8px; background-color:#fcfefd; border: 1px solid #ebebeb; border-radius: 5px; top: -8px;" role="menu" >
        <asp:Repeater runat="server" ID="rptItemMenu" OnItemDataBound="Rpt_ItemRootDataBound">
            <ItemTemplate>
                <li data-submenu-id="-<%#Eval("CatalogID")%>" class="-<%#Eval("CatalogID")%>">

                    <a href="<%#Getlink(Eval("CatalogName"),Eval("CatalogID"))%>" title="<%#Eval("CatalogName")%>">
					<i class="fa fa-arrow-circle-right" style="color:#1d5284;" aria-hidden="true"></i> 
                        <strong class="submenu-title" style="color: #1d5284;font-size: 12px;text-decoration: none;font-family: Arial,Tahoma,Verdana, MS Sans Serif;">


                            <%#Eval("CatalogName")%></strong>
                       
                    </a>

                    <asp:Literal ID="Thedau" runat="server" />

                    <div class="popover-content" runat="server"  id="Menucon" visible="false">

                        <ul class="MenuSubcl" runat="server">
                           <%-- <asp:Panel ID="Panel1" runat="server" Visible="false">--%>
                                <asp:Repeater runat="server" ID="rptItemMenu1" OnItemDataBound="Rpt_ItemDataBound">
                                    <ItemTemplate>
                                        <li><a href="<%#Getlink(Eval("CatalogName"),Eval("CatalogID"))%>" class="menu2"
                                            title="<%#Eval("CatalogName")%>">
                                            <%#Eval("CatalogName")%>
                                        </a>
                                           <ul style="display:none;">
                                                <asp:Repeater runat="server" ID="rptItemMenu1">
                                                    <ItemTemplate>
                                                        <li class="menu3"><a href="<%#Getlink(Eval("CatalogName"),Eval("CatalogID"))%>"
                                                            title="<%#Eval("CatalogName")%>">
                                                            <%#Eval("CatalogName")%>
                                                        </a>


                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>

                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                           <%-- </asp:Panel>--%>
                        </ul>


                        <ul class="MenuSubcl" runat="server"  id="Menusub2" visible="false">
                           <%-- <asp:Panel ID="Panel2" runat="server" Visible="false">--%>
                                <asp:Repeater runat="server" ID="rptItemMenu2" OnItemDataBound="Rpt_ItemDataBound">
                                    <ItemTemplate>
                                        <li><a href="<%#Getlink(Eval("CatalogName"),Eval("CatalogID"))%>" class="menu2"
                                            title="<%#Eval("CatalogName")%>">
                                            <%#Eval("CatalogName")%>
                                        </a>
                                            <ul style="display:none;">
                                                <asp:Repeater runat="server" ID="rptItemMenu1">
                                                    <ItemTemplate>
                                                        <li class="menu3"><a href="<%#Getlink(Eval("CatalogName"),Eval("CatalogID"))%>"
                                                            title="<%#Eval("CatalogName")%>">
                                                            <%#Eval("CatalogName")%>
                                                        </a>


                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>

                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                           <%-- </asp:Panel>--%>
                        </ul>


                        <%--<ul class="MenuSubcl">
                           <asp:Panel ID="Panel3" runat="server" Visible="false">
                                <asp:Repeater runat="server" ID="rptItemMenu3" OnItemDataBound="Rpt_ItemDataBound">
                                    <ItemTemplate>
                                        <li><a href="<%#Getlink(Eval("CatalogName"),Eval("CatalogID"))%>" class="menu2"
                                            title="<%#Eval("CatalogName")%>">
                                            <%#Eval("CatalogName")%>
                                        </a>
                                            <ul>
                                                <asp:Repeater runat="server" ID="rptItemMenu1">
                                                    <ItemTemplate>
                                                        <li class="menu3"><a href="<%#Getlink(Eval("CatalogName"),Eval("CatalogID"))%>"
                                                            title="<%#Eval("CatalogName")%>">
                                                            <%#Eval("CatalogName")%>
                                                        </a>


                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>

                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                           </asp:Panel>
                        </ul>--%>

                        <ul class="manufaceter" runat="server" visible="false" id="MenuManufaceter">
                            <div class="titlemanu">Thương hiệu nổi bật</div>
                            <asp:Repeater runat="server" ID="RptManufacter">
                                <ItemTemplate>
                                    <li><a href="<%#Getlinkmanu(Eval("ManufacturerName"),Eval("CatalogID"),Eval("manufacturerID"))%>"
                                        title="<%#Eval("ManufacturerName")%>">
                                        <%#Eval("ManufacturerName")%>
                                    </a></li>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ul>
                        <asp:Literal ID="AnhnenMenu" runat="server" />

                        <div class="banner-cBottom">
                        </div>
                    </div>

                    <asp:Literal ID="Thecuoi" runat="server" />
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>



    <script>

        var $menu = $(".dropdown-menu");
        $menu.menuAim({
            activate: activateSubmenu,
            deactivate: deactivateSubmenu
        });

        function activateSubmenu(row) {
            var $row = $(row),
                    submenuId = $row.data("submenuId"),
                    $submenu = $("#" + submenuId),
                    height = $menu.outerHeight(),
                    width = $menu.outerWidth();

            // Show the submenu
            $submenu.css({
                display: "block",
                top: -1,
                left: width - 3,  // main should overlay submenu
                height: height  // padding for main dropdown's arrow
            });

            // Keep the currently activated row's highlighted look
            $row.find("a").addClass("maintainHover");
        }

        function deactivateSubmenu(row) {
            var $row = $(row),
                    submenuId = $row.data("submenuId"),
                    $submenu = $("#" + submenuId);

            // Hide the submenu and remove the row's highlighted look
            $submenu.css("display", "none");
            $row.find("a").removeClass("maintainHover");
        }

        //$(".dropdown-menu li").click(function (e) {
        //    e.stopPropagation();
        //});

        //$(document).click(function () {
        //    $(".popover").css("display", "none");
        //    $("a.maintainHover").removeClass("maintainHover");
        //});

        $(".dropdown-menu li").mouseover(function (e) {
            e.stopPropagation();
        });

        $(document).mouseover(function () {
            $(".popover").css("display", "none");
            $("a.maintainHover").removeClass("maintainHover");
        });

        $(".dropdown-toggle").mouseover(function () {
            if (!$(this).parent().hasClass("open")) {
                $(this).trigger("click");
            }
        });
    </script>




</div>
<div class="clear"></div>
