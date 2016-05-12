<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StepAditionalInfo.ascx.cs" Inherits="Cure.WebAdmin.Client.Controls.StepAditionalInfo" %>

<%@ Register Src="~/Client/Controls/MyFileManager.ascx" TagPrefix="uc" TagName="MyFileManager" %>

<uc:myfilemanager runat="server" ID="uxMyFileManager" />
<p>
    Дополнительная информация:
</p>
<ul>
    <li>
        Стоимость лечения, <a href="http://www.dcp-china.ru/informatciya/byudget-na-lechenie">Подробнее</a>
    </li>
    <li>
        Медицинское сопровождение, <a href="http://www.dcp-china.ru/informatciya/meditsinskoe-soprovozhdenie">Подробнее</a>
    </li>
    <li>
        Информация о китайской валюте, <a href="http://www.dcp-china.ru/informatciya/vse-pro-dengi">Подробнее</a>
    </li>
    <li>
        Продукты питания, <a href="http://www.dcp-china.ru/informatciya/produkty-pitaniya">Подробнее</a>
    </li>
    <li>
        Климат в городе Юньчэн, <a href="http://www.dcp-china.ru/informatciya/pogoda-v-yunchen">Подробнее</a>
    </li>
    <li>
        Мобильная связь, <a href="http://www.dcp-china.ru/informatciya/mobilnaya-svyaz">Подробнее</a>
    </li>
</ul>