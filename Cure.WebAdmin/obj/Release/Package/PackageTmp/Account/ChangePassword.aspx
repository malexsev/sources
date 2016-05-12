<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="ChangePassword.aspx.cs" Inherits="Cure.WebAdmin.ChangePassword" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="accountHeader">
    <h2>
        Изменение пароля</h2>
    <p>Воспользуйтесь формой ниже, чтобы изменить пароль.</p>
    <p>Для нового пароля требуется как минимум <%= Membership.MinRequiredPasswordLength %> символов.</p>
</div>

<br />
<dx:ASPxLabel ID="lblCurrentPassword" runat="server" Text="Старый пароль:" />
<div class="form-field">
    <dx:ASPxTextBox ID="tbCurrentPassword" runat="server" Password="true" Width="200px">
        <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup">
            <RequiredField ErrorText="Требуется старый пароль." IsRequired="true" />
        </ValidationSettings>
    </dx:ASPxTextBox>
</div>
<dx:ASPxLabel ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Пароль:" />
<div class="form-field">
    <dx:ASPxTextBox ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server"
        Width="200px">
        <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup">
            <RequiredField ErrorText="Требуется пароль." IsRequired="true" />
        </ValidationSettings>
    </dx:ASPxTextBox>
</div>
<dx:ASPxLabel ID="lblConfirmPassword" runat="server" AssociatedControlID="tbConfirmPassword"
    Text="Пароль:" />
<div class="form-field">
    <dx:ASPxTextBox ID="tbConfirmPassword" Password="true" runat="server" Width="200px">
        <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup">
            <RequiredField ErrorText="Требуется подтверждение пароля." IsRequired="true" />
        </ValidationSettings>
        <ClientSideEvents Validation="function(s, e) {
            var originalPasswd = Password.GetText();
            var currentPasswd = s.GetText();
            e.isValid = (originalPasswd  == currentPasswd );
            e.errorText = 'Пароль и подтверждение пароля должны совпадать.';
        }" />
    </dx:ASPxTextBox>
</div>
<dx:ASPxButton ID="btnChangePassword" runat="server" Text="Изменить пароль" ValidationGroup="ChangeUserPasswordValidationGroup"
    OnClick="btnChangePassword_Click">
</dx:ASPxButton>
</asp:Content>