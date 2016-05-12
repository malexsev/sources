<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Recovery.aspx.cs" Inherits="Cure.WebAdmin.Recovery" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="accountHeader">
        <h2>Восстановление пароля</h2>
        <p>
            <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" SuccessText="Новый пароль выслан Вам на указанный при регистрации адрес электронной почты." GeneralFailureText="В базе данных такого логина не зарегистрировано" TextLayout="TextOnTop" UserNameFailureText="Мы не нашли информации по данному логину" Width="479px">
                <MailDefinition From="support@zeiman.ru" Subject="Восстановление пароля"
                    Priority="High" BodyFileName="~/Content/MailTemplates/PassrordRecoveryTemplate.txt" />
                <TextBoxStyle CssClass="content" />
                <UserNameTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                        <tr>
                            <td>
                                <table cellpadding="0" style="width:479px;">
                                    <tr>
                                        <td class="item" style="height: 40px">Введите Ваше имя пользователя (логин), который Вы указывали при регистрации.</td>
                                    </tr>
                                    <tr>
                                        <td class="item">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Логин:</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="item">
                                            <asp:TextBox ID="UserName" runat="server" CssClass="content"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Требуется ввести имя пользователя (логин)." ToolTip="В это поле надо ввести Ваше имя пользователя." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color:Red;" class="item">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="item">
                                            <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Восстановить пароль" ValidationGroup="PasswordRecovery1" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </UserNameTemplate>
            </asp:PasswordRecovery>
        </p>
    </div>

</asp:Content>
