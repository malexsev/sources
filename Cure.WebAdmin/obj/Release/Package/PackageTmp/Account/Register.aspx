<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Register.aspx.cs" Inherits="Cure.WebAdmin.Register" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
    <div class="accountHeader">
    <h2>
        Создание учётной записи</h2>
    <p>Воспользуйтесь формой ниже, для создания учётной записи.</p>
    <p>Пароль должен содержать минимум <%= Membership.MinRequiredPasswordLength %> символов.</p>
</div>
    <dx:ASPxLabel ID="lblUserName" runat="server" AssociatedControlID="tbUserName" Text="Имя пользователя:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbUserName" runat="server" Width="200px">
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField ErrorText="Обязательное поле." IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblEmail" runat="server" AssociatedControlID="tbEmail" Text="E-mail:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbEmail" runat="server" Width="200px">
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField ErrorText="Обязательное поле." IsRequired="true" />
                <RegularExpression ErrorText="Неверный e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Пароль:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server"
            Width="200px">
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField ErrorText="Обязательное поле." IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblConfirmPassword" runat="server" AssociatedControlID="tbConfirmPassword"
        Text="Подтверждение пароля:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbConfirmPassword" Password="true" runat="server" Width="200px">
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField ErrorText="Обязательное поле." IsRequired="true" />
            </ValidationSettings>
            <ClientSideEvents Validation="function(s, e) {
                var originalPasswd = Password.GetText();
                var currentPasswd = s.GetText();
                e.isValid = (originalPasswd  == currentPasswd );
                e.errorText = 'Пароль и его подтверждение должны совпадать.';
            }" />
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxButton ID="btnCreateUser" runat="server" Text="Создать нового пользователя" ValidationGroup="RegisterUserValidationGroup"
        OnClick="btnCreateUser_Click">
    </dx:ASPxButton>
</asp:content>