<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="EditArticles.aspx.cs" Inherits="Admin_EditArticles" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/tiny_mce/tiny_mce.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <!-- TinyMCE -->

    <script type="text/javascript">
        tinyMCE.init({
            // General options
            mode: "exact",
            elements: '<%= txtBody.ClientID %>',
            theme: "advanced",
            plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,autosave",

            // Theme options
            theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
            theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak,restoredraft",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,

            // Example content CSS (should be your site CSS)
            content_css: "css/content.css",

            // Drop lists for link/image/media/template dialogs
            template_external_list_url: "lists/template_list.js",
            external_link_list_url: "lists/link_list.js",
            external_image_list_url: "lists/image_list.js",
            media_external_list_url: "lists/media_list.js",

            // Style formats
            style_formats: [
			{ title: 'Bold text', inline: 'b' },
			{ title: 'Red text', inline: 'span', styles: { color: '#ff0000'} },
			{ title: 'Red header', block: 'h1', styles: { color: '#ff0000'} },
			{ title: 'Example 1', inline: 'span', classes: 'example1' },
			{ title: 'Example 2', inline: 'span', classes: 'example2' },
			{ title: 'Table styles' },
			{ title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
		],

            // Replace values for the template plugin
            template_replace_values: {
                username: "Some User",
                staffid: "991234"
            }
        });
    </script>

    <!-- /TinyMCE -->
    <div id="content_wrapper" style="padding-top: 74px;">
        <div style="padding: 0px 100px 40px 100px;">
            <asp:Label ID="lblTitle" runat="server" Text="עריכת מאמרים" Font-Bold="true" Font-Size="20px" ForeColor="#EF1E3D" />
            <div style="margin-top: 20px; font-weight: bold; font-size: 16px; padding-right: 20px;">
                בחר מאמר:
                &nbsp;
                <asp:DropDownList ID="ddlArticles" runat="server" OnSelectedIndexChanged="ddlArticles_SelectedIndexChanged"
                    AutoPostBack="true" Width="300px" />
            </div>
            <div style="margin-top: 20px;">
                <asp:HiddenField ID="hfArticleId" runat="server" />
                <asp:Label runat="server" Text="כותרת המאמר:" Font-Bold="true" /><br />
                <asp:TextBox ID="txtArticleTitle" runat="server" Width="730px" />
            </div>
            <div style="margin-top: 10px;">
                <asp:Label ID="Label1" runat="server" Text="מחבר המאמר:" Font-Bold="true" /><br />
                <asp:TextBox ID="txtPublisher" runat="server" Width="730px" />
            </div>
            <div style="margin-top: 10px;">
                <asp:Label ID="Label2" runat="server" Text="תקציר:" Font-Bold="true" /><br />
                <asp:TextBox ID="txtAbstract" runat="server" TextMode="MultiLine" Rows="3" Width="730px" />
            </div>
            <div style="margin-top: 20px;">
                <asp:Label ID="Label3" runat="server" Text="תוכן המאמר:" Font-Bold="true" /><br />
                <asp:TextBox ID="txtBody" TextMode="MultiLine" Rows="10" runat="server" />
            </div>
            <div style="margin-top: 20px;">
                <asp:Label ID="Label5" runat="server" Text="תאריך עריכה:" Font-Bold="true" />&nbsp;
                <asp:Label ID="lblModifiedDate" runat="server" ForeColor="DarkGray" />
            </div>
            <div style="margin-top: 30px; text-align: center;">
                <asp:Button ID="btnSave" runat="server" Text="שמור שינויים" Style="margin-right: 50px; width: 120px; height: 30px; "
                    OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
