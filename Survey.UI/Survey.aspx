<%@ Page Language="C#" MasterPageFile="~/Survey.Master" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="Survey.UI.SurveyPage" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assets/css/Survey.css" rel="stylesheet" />
    <link href="Assets/css/RadioButton.css" rel="stylesheet" />

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

    <script src="https://code.jquery.com/jquery-3.1.0.min.js" integrity="sha256-cCueBR6CsyA4/9szpPfrX3s49M9vUU5BgtiJj06wt/s=" crossorigin="anonymous"></script>

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <script src="Assets/js/survey.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" EnablePageMethods="True" />
    <div class="header">
        <div class="container">
            <div class="headerContainer" >
                <div class="headerContent">
                    <div class="ca-sign-wrapper">
                        <span>proudly powered by </span>
                        <span class="ca-sign-smallest">client aura</span>
                    </div>
                    <div class="ca-logo-smallest"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-wrapper">
        <div class="container">
            <asp:HiddenField ID="orderGuid" runat="server"></asp:HiddenField>
            <asp:Literal ID="literalSurveyContent" runat="server"></asp:Literal>

            <button class="submitBtn" onclick="SubmitSurvey();return false;">Submit Survey</button>

        </div>
    </div>
    <div class="header">header content</div>

    <script type="text/javascript">
        OrderGuidHidden = {
            GetValue: function () {
                return document.getElementById('<%= orderGuid.ClientID%>').value;
            }
        };
    </script>
</asp:Content>
