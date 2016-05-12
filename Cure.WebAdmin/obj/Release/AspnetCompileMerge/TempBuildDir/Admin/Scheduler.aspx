<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Scheduler.aspx.cs" Inherits="Cure.WebAdmin.Admin.Scheduler" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v14.1.Core, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" SelectMethod="GetScheduler" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

        <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" ActiveViewType="Month" AppointmentDataSourceID="uxMainDataSource" ClientIDMode="AutoID" ResourceDataSourceID="uxMainDataSource" Start="2016-01-04">
            <Storage EnableReminders="False">
                <Appointments AutoRetrieveId="True">
                    <Mappings AppointmentId="Id" End="oDateTo" Start="oDateFrom" Subject="Total" />
                    <CustomFieldMappings>
                        <dxwschs:ASPxAppointmentCustomFieldMapping Member="dName" Name="DName" />
                    </CustomFieldMappings>
                </Appointments>
                <Resources>
                    <Mappings Caption="dShortName" Color="oDney" ResourceId="Id" />
                </Resources>
            </Storage>
            <Views>
                <DayView Enabled="False">
                    <TimeRulers>
                        <cc1:TimeRuler></cc1:TimeRuler>
                    </TimeRulers>
                </DayView>

                <WorkWeekView Enabled="False">
                    <TimeRulers>
                        <cc1:TimeRuler></cc1:TimeRuler>
                    </TimeRulers>
                </WorkWeekView>
                <WeekView Enabled="False">
                </WeekView>
                <MonthView WeekCount="60">
                    <AppointmentDisplayOptions AppointmentAutoHeight="True" ContinueArrowDisplayType="ArrowWithText" EndTimeVisibility="Always" ShowBordersForSameDayAppointments="True" StartTimeVisibility="Always" />
                    <CellAutoHeightOptions Mode="FitToContent" />
                </MonthView>
                <TimelineView Enabled="False">
                </TimelineView>
            </Views>
            <optionscustomization allowappointmentcopy="None" allowappointmentcreate="None" allowappointmentdelete="None" allowappointmentdrag="None" allowappointmentdragbetweenresources="None" allowappointmentedit="None" allowappointmentmultiselect="False" allowappointmentresize="None" allowdisplayappointmentdependencyform="Never" allowdisplayappointmentform="Never" allowinplaceeditor="None" />
            <OptionsBehavior ShowRemindersForm="False" ShowTimeMarker="False" ShowViewSelector="False" RecurrentAppointmentDeleteAction="Cancel" RecurrentAppointmentEditAction="Cancel" />
            <OptionsView FirstDayOfWeek="Monday" />
        </dxwschs:ASPxScheduler>

        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

    </div>

</asp:Content>
