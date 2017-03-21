<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Login.aspx.cs" Inherits="Cure.WebAdmin.Login" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Literal runat="server" ID="uxRegisterCompleted" Visible="false" Text="<p>Регистрация прошла успешно. Войдите в личный кабинет, используя Ваш логин и пароль.</p>"></asp:Literal>
    <div class="accountHeader">
        <h2>Вход</h2>
        <p>
            Введите имя и пароль. 
        <a href="Register.aspx">Зарегистрируйтесь</a> если у Вас нет учётной записи.
        </p>
        Или <a href="Recovery.aspx">Восстановите пароль</a>, если Вы помните логин.
        <br />
        <br />
        <p>
            Открыт новый сайт для пользоватлей, Ваш логин перенесён в новый Личный кабинет: <a href='http://dcp-china.ru'>http://dcp-china.ru</a>
        </p>
    </div>
    <dx:ASPxLabel ID="lblUserName" runat="server" AssociatedControlID="tbUserName" Text="Имя пользователя:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbUserName" runat="server" Width="200px">
            <ValidationSettings ValidationGroup="LoginUserValidationGroup">
                <RequiredField ErrorText="Требуется имя пользователя." IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Пароль:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbPassword" runat="server" Password="true" Width="200px">
            <ValidationSettings ValidationGroup="LoginUserValidationGroup">
                <RequiredField ErrorText="Требуется ввести пароль." IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxButton ID="btnLogin" runat="server" Text="Войти" ValidationGroup="LoginUserValidationGroup"
        OnClick="btnLogin_Click">
    </dx:ASPxButton>
</asp:Content>
