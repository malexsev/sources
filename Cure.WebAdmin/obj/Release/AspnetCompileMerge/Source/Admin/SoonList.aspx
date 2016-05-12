<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="SoonList.aspx.cs" Inherits="Cure.WebAdmin.Admin.SoonList" %>

<%@ Register Src="~/Admin/Controls/SoonVisitsList.ascx" TagPrefix="uc" TagName="SoonVisitsList" %>
<%@ Register Src="~/Admin/Controls/JustClosedVisitsList.ascx" TagPrefix="uc" TagName="JustClosedVisitsList" %>
<%@ Register Src="~/Admin/Controls/CurrentVisitsList.ascx" TagPrefix="uc" TagName="CurrentVisitsList" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function OpenReps(visitId) {
            var url = '/Output/PacientVisitDetailsReport.aspx?visitId=' + visitId;
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>

    <div class="content">
        <uc:SoonVisitsList runat="server" ID="uxSoonVisitsList" />
        <uc:CurrentVisitsList runat="server" ID="uxCurrentVisitsList" />
        <uc:JustClosedVisitsList runat="server" ID="uxJustClosedVisitsList" />
    </div>
</asp:Content>
