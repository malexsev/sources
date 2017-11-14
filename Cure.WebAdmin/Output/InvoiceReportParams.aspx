<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Empty.master" CodeBehind="InvoiceReportParams.aspx.cs" Inherits="Cure.WebAdmin.Admin.InvoiceReportParams" %>

<%@ Register Src="~/Controls/ResultBox.ascx" TagName="ResultBox" TagPrefix="uc" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        var statics = {
            daysCount: <%=this.DaysCount%>,
            procCount: <%=this.ProcCount%>,
        };

        var calcLine = function($rate, $days, $proc, $cost, $total) {
            var proc = 1.0 * $rate.GetValue() * $days.GetValue();
            $proc.SetValue(Math.ceil(proc));
            var total = Math.ceil(1.0 * proc * $cost.GetValue());
            $total.SetValue(total);
            return total;
        };

        var calculateZbor = function() {
            switch (true) {
            case (statics.daysCount >= 1 && statics.daysCount <= 37):
                return 2000;
            case (statics.daysCount >= 38 && statics.daysCount <= 67):
                return 3000;
            case (statics.daysCount >= 68 && statics.daysCount <= 97):
                return 3500;
            case (statics.daysCount >= 98 && statics.daysCount <= 127):
                return 4000;
            case (statics.daysCount >= 128 && statics.daysCount <= 157):
                return 4500;
            case (statics.daysCount >= 158 && statics.daysCount <= 187):
                return 5000;
            case (statics.daysCount >= 188 && statics.daysCount <= 217):
                return 5500;
            case (statics.daysCount >= 218 && statics.daysCount <= 247):
                return 6000;
            case (statics.daysCount >= 248 && statics.daysCount <= 277):
                return 6500;
            case (statics.daysCount >= 278 && statics.daysCount <= 307):
                return 7000;
            case (statics.daysCount >= 308 && statics.daysCount <= 337):
                return 7500;
            case (statics.daysCount >= 338 && statics.daysCount <= 367):
                return 8000;
            case (statics.daysCount >= 368 && statics.daysCount <= 397):
                return 8500;
            case (statics.daysCount >= 398 && statics.daysCount <= 427):
                return 9000;
            case (statics.daysCount >= 428 && statics.daysCount <= 457):
                return 9500;
            default:
                return 3500;
            }
        };

        var defaults = function() {
            //Проживание
            uxLine1CheckBox.SetChecked(true);
            uxLine1Rate.SetValue(1);
            uxLine1Days.SetValue(statics.daysCount);
            uxLine1Cost.SetValue(80);
            //Услуги медсестры
            uxLine2CheckBox.SetChecked(true);
            uxLine2Rate.SetValue(1);
            uxLine2Days.SetValue(statics.daysCount);
            uxLine2Cost.SetValue(10);
            //Анализы
            uxLine3CheckBox.SetChecked(true);
            uxLine3Rate.SetValue("-");
            uxLine3Days.SetValue("-");
            uxLine3Proc.SetValue("-");
            uxLine3Cost.SetValue(1189);
            //Лекарства
            uxLine4CheckBox.SetChecked(true);
            uxLine4Rate.SetValue(1);
            uxLine4Days.SetValue(statics.procCount);
            uxLine4Cost.SetValue(200);
            //Массаж
            uxLine5CheckBox.SetChecked(true);
            uxLine5Rate.SetValue(2);
            uxLine5Days.SetValue(statics.procCount);
            uxLine5Cost.SetValue(65);
            //ЛФК
            uxLine6CheckBox.SetChecked(true);
            uxLine6Rate.SetValue(4);
            uxLine6Days.SetValue(statics.procCount);
            uxLine6Cost.SetValue(65);
            //Физио
            uxLine7CheckBox.SetChecked(true);
            uxLine7Rate.SetValue(0.5);
            uxLine7Days.SetValue(statics.procCount);
            uxLine7Cost.SetValue(40);
            //Физио
            uxLine7CheckBox.SetChecked(true);
            uxLine7Rate.SetValue(0.5);
            uxLine7Days.SetValue(statics.procCount);
            uxLine7Cost.SetValue(40);
            //Моторика
            uxLine8CheckBox.SetChecked(true);
            uxLine8Rate.SetValue(1);
            uxLine8Days.SetValue(statics.procCount);
            uxLine8Cost.SetValue(40);
            //Иголки
            uxLine9CheckBox.SetChecked(true);
            uxLine9Rate.SetValue(0.5);
            uxLine9Days.SetValue(statics.procCount);
            uxLine9Cost.SetValue(70);
            //Капельницы
            uxLine10CheckBox.SetChecked(true);
            uxLine10Rate.SetValue(0.5);
            uxLine10Days.SetValue(statics.procCount);
            uxLine10Cost.SetValue(20);
            //Расходные материалы
            uxLine11CheckBox.SetChecked(true);
            uxLine11Rate.SetValue(1);
            uxLine11Days.SetValue(statics.procCount);
            uxLine11Cost.SetValue(13);
            //Калоген
            uxLine12CheckBox.SetChecked(true);
            uxLine12Rate.SetValue(5);
            uxLine12Days.SetValue(1);
            uxLine12Cost.SetValue(300);
            //Иглонож
            uxLine13CheckBox.SetChecked(true);
            uxLine13Rate.SetValue(3);
            uxLine13Days.SetValue(1);
            uxLine13Cost.SetValue(900);
            //Гипсование
            uxLine14CheckBox.SetChecked(true);
            uxLine14Rate.SetValue(4);
            uxLine14Days.SetValue(1);
            uxLine14Cost.SetValue(300);
            //Оргзбор
            uxLine15CheckBox.SetChecked(true);
            uxLine15Rate.SetValue("-");
            uxLine15Days.SetValue("-");
            uxLine15Proc.SetValue("-");
            uxLine15Cost.SetValue(3500);
            //Резерв - 1
            uxLine16CheckBox.SetChecked(false);
            uxLine16Rate.SetValue("-");
            uxLine16Days.SetValue("-");
            uxLine16Proc.SetValue("-");
            uxLine16Cost.SetValue(0);
            //Резерв - 2
            uxLine17CheckBox.SetChecked(false);
            uxLine17Rate.SetValue("-");
            uxLine17Days.SetValue("-");
            uxLine17Proc.SetValue("-");
            uxLine17Cost.SetValue(0);

            uxBottomUsdRate.SetValue(<%=this.RatePendo%>);
            uxBottomRubRate.SetValue(<%=this.RateRubl%>);
        };

        var calculate = function(isInit) {
            var total = 0;
            //Проживание
            var total1 = calcLine(uxLine1Rate, uxLine1Days, uxLine1Proc, uxLine1Cost, uxLine1Total);
            if (isInit == true || uxLine1CheckBox.GetChecked()) {
                total += parseInt(total1);
            };
            //Услуги медсестры
            var total2 = calcLine(uxLine2Rate, uxLine2Days, uxLine2Proc, uxLine2Cost, uxLine2Total);
            if (isInit == true || uxLine2CheckBox.GetChecked()) {
                total += parseInt(total2);
            };
            //Анализы
            var total3 = uxLine3Cost.GetValue();
            uxLine3Total.SetValue(total3);
            if (isInit == true || uxLine3CheckBox.GetChecked()) {
                total += parseInt(total3);
            };
            //Лекарства
            var total4 = calcLine(uxLine4Rate, uxLine4Days, uxLine4Proc, uxLine4Cost, uxLine4Total);
            if (isInit == true || uxLine4CheckBox.GetChecked()) {
                total += parseInt(total4);
            };
            //Массаж
            var total5 = calcLine(uxLine5Rate, uxLine5Days, uxLine5Proc, uxLine5Cost, uxLine5Total);
            if (isInit == true || uxLine5CheckBox.GetChecked()) {
                total += parseInt(total5);
            };
            //ЛФК
            var total6 = calcLine(uxLine6Rate, uxLine6Days, uxLine6Proc, uxLine6Cost, uxLine6Total);
            if (isInit == true || uxLine6CheckBox.GetChecked()) {
                total += parseInt(total6);
            };
            //Физио
            var total7 = calcLine(uxLine7Rate, uxLine7Days, uxLine7Proc, uxLine7Cost, uxLine7Total);
            if (isInit == true || uxLine7CheckBox.GetChecked()) {
                total += parseInt(total7);
            };
            //Моторика
            var total8 = calcLine(uxLine8Rate, uxLine8Days, uxLine8Proc, uxLine8Cost, uxLine8Total);
            if (isInit == true || uxLine8CheckBox.GetChecked()) {
                total += parseInt(total8);
            };
            //Иголки
            var total9 = calcLine(uxLine9Rate, uxLine9Days, uxLine9Proc, uxLine9Cost, uxLine9Total);
            if (isInit == true || uxLine9CheckBox.GetChecked()) {
                total += parseInt(total9);
            };
            //Капельницы
            var total10 = calcLine(uxLine10Rate, uxLine10Days, uxLine10Proc, uxLine10Cost, uxLine10Total);
            if (isInit == true || uxLine10CheckBox.GetChecked()) {
                total += parseInt(total10);
            };
            //Расходные материалы
            var total11 = calcLine(uxLine11Rate, uxLine11Days, uxLine11Proc, uxLine11Cost, uxLine11Total);
            if (isInit == true || uxLine11CheckBox.GetChecked()) {
                total += parseInt(total11);
            };
            //Калоген
            var total12 = calcLine(uxLine12Rate, uxLine12Days, uxLine12Proc, uxLine12Cost, uxLine12Total);
            if (isInit == true || uxLine12CheckBox.GetChecked()) {
                total += parseInt(total12);
            };
            //Иглонож
            var total13 = calcLine(uxLine13Rate, uxLine13Days, uxLine13Proc, uxLine13Cost, uxLine13Total);
            if (isInit == true || uxLine13CheckBox.GetChecked()) {
                total += parseInt(total13);
            };
            //Гипсование
            var total14 = calcLine(uxLine14Rate, uxLine14Days, uxLine14Proc, uxLine14Cost, uxLine14Total);
            if (isInit == true || uxLine14CheckBox.GetChecked()) {
                total += parseInt(total14);
            };
            //Оргзбор
            var total15 = calculateZbor();
            uxLine15Total.SetValue(total15);
            if (isInit == true || uxLine15CheckBox.GetChecked()) {
                total += parseInt(total15);
            };
            //Резерв - 1
            var total16 = uxLine16Cost.GetValue();
            uxLine16Total.SetValue(total16);
            if (isInit == true || uxLine16CheckBox.GetChecked()) {
                total += parseInt(total16);
            };
            //Резерв - 2
            var total17 = uxLine17Cost.GetValue();
            uxLine17Total.SetValue(total17);
            if (isInit == true || uxLine17CheckBox.GetChecked()) {
                total += parseInt(total17);
            };
            //Общее
            uxBottomUaniTotal.SetValue((total * 1.0).toFixed(2));
            uxBottomUsdTotal.SetValue((total * uxBottomRubRate.GetValue() / uxBottomUsdRate.GetValue()).toFixed(2));
            uxBottomRubTotal.SetValue((total * uxBottomRubRate.GetValue()).toFixed(2));
            console.log("Выбрано: " + total);
        };
        
        $(document).ready(function () {
            console.log("Перед вычислением.");
            defaults();
            calculate(true);
        });
    </script>

    <div class="content">
        <uc:ResultBox ID="uxResult" runat="server" />
        <div class="panelsdelimiter"></div>

        <asp:Panel ID="FormPanel" runat="server">
            <table class="zui-table" id="uxTable" runat="server">
                <thead>
                    <tr>
                        <th>Активный</th>
                        <th>Наименование</th>
                        <th>Коэфициент</th>
                        <th>Кол. дней</th>
                        <th>Кол. процедур</th>
                        <th>Стоимость</th>
                        <th>Итого</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine1CheckBox" ClientInstanceName="uxLine1CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine1Name" runat="server" Text="Проживание" />
                            <dx:ASPxLabel ID="uxLine1Description" runat="server" Text="Проживание в больничной палате" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine1Rate" ClientInstanceName="uxLine1Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine1Days" ClientInstanceName="uxLine1Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine1Proc" ClientInstanceName="uxLine1Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine1Cost" ClientInstanceName="uxLine1Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine1Total" ClientInstanceName="uxLine1Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine2CheckBox" ClientInstanceName="uxLine2CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine2Name" runat="server" Text="Услуги медсестры" />
                            <dx:ASPxLabel ID="uxLine2Description" runat="server" Text="Обслуживание медперсоналом пациентов в сутки" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine2Rate" ClientInstanceName="uxLine2Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine2Days" ClientInstanceName="uxLine2Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine2Proc" ClientInstanceName="uxLine2Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine2Cost" ClientInstanceName="uxLine2Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine2Total" ClientInstanceName="uxLine2Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine3CheckBox" ClientInstanceName="uxLine3CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine3Name" runat="server" Text="Анализы" />
                            <dx:ASPxLabel ID="uxLine3Description" runat="server" Text="Анализы и исследования (общий анализ крови, ЭЭГ, ЭКГ, УЗИ, МРТ, рентген, и др)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine3Rate" ClientInstanceName="uxLine3Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine3Days" ClientInstanceName="uxLine3Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine3Proc" ClientInstanceName="uxLine3Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine3Cost" ClientInstanceName="uxLine3Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine3Total" ClientInstanceName="uxLine3Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine4CheckBox" ClientInstanceName="uxLine4CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine4Name" runat="server" Text="Лекарства" />
                            <dx:ASPxLabel ID="uxLine4Description" runat="server" Text="Лекарственные препараты КТМ и западной медицины" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine4Rate" ClientInstanceName="uxLine4Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine4Days" ClientInstanceName="uxLine4Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine4Proc" ClientInstanceName="uxLine4Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine4Cost" ClientInstanceName="uxLine4Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine4Total" ClientInstanceName="uxLine4Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine5CheckBox" ClientInstanceName="uxLine5CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine5Name" runat="server" Text="Массаж" />
                            <dx:ASPxLabel ID="uxLine5Description" runat="server" Text="Общий массаж (стимулирующий и расслабляющий массаж всего тела)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine5Rate" ClientInstanceName="uxLine5Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine5Days" ClientInstanceName="uxLine5Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine5Proc" ClientInstanceName="uxLine5Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine5Cost" ClientInstanceName="uxLine5Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine5Total" ClientInstanceName="uxLine5Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine6CheckBox" ClientInstanceName="uxLine6CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine6Name" runat="server" Text="ЛФК" />
                            <dx:ASPxLabel ID="uxLine6Description" runat="server" Text="ЛФК (методика Бобата, Войта, растяжки, и др методы КТМ)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine6Rate" ClientInstanceName="uxLine6Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine6Days" ClientInstanceName="uxLine6Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine6Proc" ClientInstanceName="uxLine6Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine6Cost" ClientInstanceName="uxLine6Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine6Total" ClientInstanceName="uxLine6Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine7CheckBox" ClientInstanceName="uxLine7CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine7Name" runat="server" Text="Физио" />
                            <dx:ASPxLabel ID="uxLine7Description" runat="server" Text="Физиопроцедуры (речевой аппарат, для снижения тонуса, мото-мед, и др.)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine7Rate" ClientInstanceName="uxLine7Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine7Days" ClientInstanceName="uxLine7Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine7Proc" ClientInstanceName="uxLine7Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine7Cost" ClientInstanceName="uxLine7Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine7Total" ClientInstanceName="uxLine7Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine8CheckBox" ClientInstanceName="uxLine8CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine8Name" runat="server" Text="Моторика" />
                            <dx:ASPxLabel ID="uxLine8Description" runat="server" Text="Трудотерапия, мелкая моторика (сенсорно-интегральные, разработка суставов и др)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine8Rate" ClientInstanceName="uxLine8Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine8Days" ClientInstanceName="uxLine8Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine8Proc" ClientInstanceName="uxLine8Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine8Cost" ClientInstanceName="uxLine8Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine8Total" ClientInstanceName="uxLine8Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine9CheckBox" ClientInstanceName="uxLine9CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine9Name" runat="server" Text="Иголки" />
                            <dx:ASPxLabel ID="uxLine9Description" runat="server" Text="Иглотерапия (голова, лицо, глаза, руки, ноги, спина, живот)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine9Rate" ClientInstanceName="uxLine9Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine9Days" ClientInstanceName="uxLine9Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine9Proc" ClientInstanceName="uxLine9Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine9Cost" ClientInstanceName="uxLine9Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine9Total" ClientInstanceName="uxLine9Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine10CheckBox" ClientInstanceName="uxLine10CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine10Name" runat="server" Text="Капельницы" />
                            <dx:ASPxLabel ID="uxLine10Description" runat="server" Text="Капельницы (внутривенное введение мед. препаратов КТМ для стимуляции)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine10Rate" ClientInstanceName="uxLine10Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine10Days" ClientInstanceName="uxLine10Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine10Proc" ClientInstanceName="uxLine10Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine10Cost" ClientInstanceName="uxLine10Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine10Total" ClientInstanceName="uxLine10Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine11CheckBox" ClientInstanceName="uxLine11CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine11Name" runat="server" Text="Расходные материалы" />
                            <dx:ASPxLabel ID="uxLine11Description" runat="server" Text="Медицинские услуги и расходные материалы" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine11Rate" ClientInstanceName="uxLine11Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine11Days" ClientInstanceName="uxLine11Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine11Proc" ClientInstanceName="uxLine11Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine11Cost" ClientInstanceName="uxLine11Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine11Total" ClientInstanceName="uxLine11Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine12CheckBox" ClientInstanceName="uxLine12CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine12Name" runat="server" Text="Калоген" />
                            <dx:ASPxLabel ID="uxLine12Description" runat="server" Text="Имплантация калогеновых нитей (хирургическая операция)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine12Rate" ClientInstanceName="uxLine12Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine12Days" ClientInstanceName="uxLine12Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine12Proc" ClientInstanceName="uxLine12Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine12Cost" ClientInstanceName="uxLine12Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine12Total" ClientInstanceName="uxLine12Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine13CheckBox" ClientInstanceName="uxLine13CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine13Name" runat="server" Text="Иглонож" />
                            <dx:ASPxLabel ID="uxLine13Description" runat="server" Text="Игло-нож (хирургическая операция)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine13Rate" ClientInstanceName="uxLine13Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine13Days" ClientInstanceName="uxLine13Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine13Proc" ClientInstanceName="uxLine13Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine13Cost" ClientInstanceName="uxLine13Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine13Total" ClientInstanceName="uxLine13Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine14CheckBox" ClientInstanceName="uxLine14CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine14Name" runat="server" Text="Гипсование" />
                            <dx:ASPxLabel ID="uxLine14Description" runat="server" Text="Гипсование методом Панцети" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine14Rate" ClientInstanceName="uxLine14Rate" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine14Days" ClientInstanceName="uxLine14Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine14Proc" ClientInstanceName="uxLine14Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine14Cost" ClientInstanceName="uxLine14Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine14Total" ClientInstanceName="uxLine14Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine15CheckBox" ClientInstanceName="uxLine15CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="uxLine15Name" runat="server" Text="Оргзбор" />
                            <dx:ASPxLabel ID="uxLine15Description" runat="server" Text="Организационнные услуги (переводчик, коммуникации, и тд)" Visible="false" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxLabel ID="uxLine15Rate" ClientInstanceName="uxLine15Rate" runat="server" Text="-">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine15Days" ClientInstanceName="uxLine15Days" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine15Proc" ClientInstanceName="uxLine15Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine15Cost" ClientInstanceName="uxLine15Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine15Total" ClientInstanceName="uxLine15Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine16CheckBox" ClientInstanceName="uxLine16CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="uxLine16Name" ClientInstanceName="uxLine16Name" runat="server" Text="Резерв - 1"
                                Increment="1" HorizontalAlign="Left" Width="160px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                            <dx:ASPxLabel ID="uxLine16Description" runat="server" Text="" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxLabel ID="uxLine16Rate" ClientInstanceName="uxLine16Rate" runat="server" Text="-">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine16Days" ClientInstanceName="uxLine16Days" runat="server" Text="-">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine16Proc" ClientInstanceName="uxLine16Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine16Cost" ClientInstanceName="uxLine16Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine16Total" ClientInstanceName="uxLine16Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                            <dx:ASPxCheckBox ID="uxLine17CheckBox" ClientInstanceName="uxLine17CheckBox" runat="server">
                                <ClientSideEvents CheckedChanged="calculate"></ClientSideEvents>
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="uxLine17Name" ClientInstanceName="uxLine17Name" runat="server" Text="Резерв - 2"
                                Increment="1" HorizontalAlign="Left" Width="160px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                            <dx:ASPxLabel ID="uxLine17Description" runat="server" Text="" />
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxLabel ID="uxLine17Rate" ClientInstanceName="uxLine17Rate" runat="server" Text="-">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine17Days" ClientInstanceName="uxLine17Days" runat="server" Text="-">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="uxLine17Proc" ClientInstanceName="uxLine17Proc" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: center;">
                            <dx:ASPxTextBox ID="uxLine17Cost" ClientInstanceName="uxLine17Cost" runat="server" Number="0" NumberType="Integer"
                                Increment="1" HorizontalAlign="Right" Width="100px">
                                <Paddings PaddingRight="5px" />
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxLine17Total" ClientInstanceName="uxLine17Total" runat="server" ReadOnly="true" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                        </td>
                        <td>Итого юани
                        </td>
                        <td style="text-align: left;">курс: 
                        </td>
                        <td colspan="3">
                            1
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxBottomUaniTotal" ClientInstanceName="uxBottomUaniTotal" runat="server" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                        </td>
                        <td>Итого доллары
                        </td>
                        <td style="text-align: left;">
                            курс: 
                        </td>
                        <td colspan="3">
                            <dx:ASPxTextBox ID="uxBottomUsdRate" ClientInstanceName="uxBottomUsdRate" runat="server" HorizontalAlign="Right" Width="60px">
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxBottomUsdTotal" ClientInstanceName="uxBottomUsdTotal" runat="server" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: center;">
                        </td>
                        <td>Итого рубли
                        </td>
                        <td style="text-align: left;">курс: 
                        </td>
                        <td colspan="3">
                            <dx:ASPxTextBox ID="uxBottomRubRate" ClientInstanceName="uxBottomRubRate" runat="server" HorizontalAlign="Right" Width="60px">
                                <ClientSideEvents TextChanged="calculate"></ClientSideEvents>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxTextBox ID="uxBottomRubTotal" ClientInstanceName="uxBottomRubTotal" runat="server" HorizontalAlign="Right" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="panelsdelimiter"></div>
            <div class="panelsdelimiter"></div>
            
            <table>
                <tr>
                    <td><asp:Button ID="SaveButon" runat="server" Text="Сформировать счёт на лечение" OnClick="SaveButon_Click" OnClientClick="$('.zui-table').hide();" /></td>
                    <td><asp:Button ID="PreviewButton" runat="server" Text="Предпросмотр" OnClick="PreviewButon_Click" /></td>
                </tr>
            </table>
            
            <div class="panelsdelimiter"></div>
            <br />
            <uc:ResultBox ID="uxResultSave" runat="server" Visible="False" />

        </asp:Panel>
    </div>
</asp:Content>

