using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web.Controls;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using DevExpress.ExpressApp.Web.Templates.Controls;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web;
using Solution1.Web.UIControls.UserControls;

namespace Solution1.Web
{
    public partial class DefaultVerticalTemplateContentNew : TemplateContent, IXafPopupWindowControlContainer, IXafSecurityActionContainerHolder
    {
        protected ASPxHiddenField ClientParams;

        protected XafPopupWindowControl PopupWindowControl;

        protected XafUpdatePanel UPPopupWindowControl;

        protected ActionContainerHolder SAC;

        protected XafUpdatePanel UPSAC;

        protected NavigationActionContainer NC;

        protected XafUpdatePanel UPNC;

        protected Panel navigation;

        protected ViewImageControl VIC;

        protected XafUpdatePanel UPVIC;

        protected ViewCaptionControl VCC;

        protected XafUpdatePanel UPVH;

        protected ActionContainerHolder mainMenu;

        protected XafUpdatePanel XafUpdatePanel1;

        protected ActionContainerHolder SearchAC;

        protected XafUpdatePanel XafUpdatePanel2;

        protected ErrorInfoControl ErrorInfo;

        protected XafUpdatePanel UPEI;

        protected ViewSiteControl VSC;

        protected XafUpdatePanel UPVSC;

        protected AboutInfoControl AIC;

        private static bool __initialized;

        public static string AdditionalClass
        {
            get;
            set;
        }

        public override IActionContainer DefaultContainer
        {
            get
            {
                if (this.mainMenu != null)
                {
                    return this.mainMenu.FindActionContainerById("View");
                }
                return null;
            }
        }

        public override object ViewSiteControl
        {
            get
            {
                return this.VSC;
            }
        }

        public XafPopupWindowControl XafPopupWindowControl
        {
            get
            {
                return this.PopupWindowControl;
            }
        }

        ActionContainerHolder IXafSecurityActionContainerHolder.SecurityActionContainerHolder
        {
            get
            {
                return this.SAC;
            }
        }

        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)this.Context.Profile;
            }
        }

        protected override bool SupportAutoEvents
        {
            get
            {
                return false;
            }
        }

        protected HttpApplication ApplicationInstance
        {
            get
            {
                return this.Context.ApplicationInstance;
            }
        }

        static DefaultVerticalTemplateContentNew()
        {
            DefaultVerticalTemplateContentNew.AdditionalClass = "sizeLimit";
        }

        public static void ClearSizeLimit()
        {
            DefaultVerticalTemplateContentNew.AdditionalClass = "";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Page.ClientScript.RegisterClientScriptResource(typeof(WebWindow), "DevExpress.ExpressApp.Web.Resources.JScripts.XafNavigation.js");
            this.Page.ClientScript.RegisterClientScriptResource(typeof(WebWindow), "DevExpress.ExpressApp.Web.Resources.JScripts.XafFooter.js");
            this.Page.ClientScript.RegisterClientScriptResource(typeof(WebWindow), "DevExpress.ExpressApp.Web.Resources.JScripts.DefaultVerticalTemplate.js");
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.PagePreRender += new EventHandler(this.CurrentRequestWindow_PagePreRender);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            IModelApplicationNavigationItems modelApplicationNavigationItems = (IModelApplicationNavigationItems)WebApplication.Instance.Model;
            bool showNavigationOnStart = ((IModelRootNavigationItemsWeb)modelApplicationNavigationItems.NavigationItems).ShowNavigationOnStart;
            if (!showNavigationOnStart && !this.navigation.CssClass.Contains("xafHidden"))
            {
                Panel expr_48 = this.navigation;
                expr_48.CssClass += " xafHidden";
            }
            WebApplication.Instance.ClientInfo.SetInfo(this.ClientParams);
            WebApplication.Instance.ClientInfo.SetValue("ShowNavigationPanelOnStart", showNavigationOnStart);
        }

        private void CurrentRequestWindow_PagePreRender(object sender, EventArgs e)
        {
            WebWindow webWindow = (WebWindow)sender;
            webWindow.RegisterStartupScript("Init", "Init();");
        }

        protected override void OnUnload(EventArgs e)
        {
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.PagePreRender -= new EventHandler(this.CurrentRequestWindow_PagePreRender);
            }
            base.OnUnload(e);
        }

        public override void SetStatus(ICollection<string> statusMessages)
        {
        }

        public override void BeginUpdate()
        {
            base.BeginUpdate();
            this.SAC.BeginUpdate();
            this.mainMenu.BeginUpdate();
            this.SearchAC.BeginUpdate();
        }

        public override void EndUpdate()
        {
            this.SAC.EndUpdate();
            this.mainMenu.EndUpdate();
            this.SearchAC.EndUpdate();
            base.EndUpdate();
        }

        [DebuggerNonUserCode]
        public DefaultVerticalTemplateContentNew()
        {
            base.AppRelativeVirtualPath = "~/DefaultVerticalTemplateContentNew.ascx";
            if (!DefaultVerticalTemplateContentNew.__initialized)
            {
                DefaultVerticalTemplateContentNew.__initialized = true;
            }
        }

        [DebuggerNonUserCode]
        private ASPxHiddenField __BuildControlClientParams()
        {
            ASPxHiddenField aSPxHiddenField = new ASPxHiddenField();
            this.ClientParams = aSPxHiddenField;
            aSPxHiddenField.ApplyStyleSheetSkin(this.Page);
            aSPxHiddenField.ApplyStyleSheetThemeInternal();
            aSPxHiddenField.ID = "ClientParams";
            aSPxHiddenField.ClientInstanceName = "ClientParams";
            return aSPxHiddenField;
        }

        [DebuggerNonUserCode]
        private XafPopupWindowControl __BuildControlPopupWindowControl()
        {
            XafPopupWindowControl xafPopupWindowControl = new XafPopupWindowControl();
            this.PopupWindowControl = xafPopupWindowControl;
            xafPopupWindowControl.ID = "PopupWindowControl";
            return xafPopupWindowControl;
        }

        [DebuggerNonUserCode]
        private XafUpdatePanel __BuildControlUPPopupWindowControl()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            this.UPPopupWindowControl = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "UPPopupWindowControl";
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n    "));
            XafPopupWindowControl obj = this.__BuildControlPopupWindowControl();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n"));
            return xafUpdatePanel;
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control3()
        {
            return new WebActionContainer
            {
                IsDropDown = true,
                DropDownMenuItemCssClass = "accountItem",
                ContainerId = "Security",
                DefaultItemCaption = "My Account",
                DefaultItemImageName = "BO_Person"
            };
        }

        [DebuggerNonUserCode]
        private void __BuildControl__control2(WebActionContainersCollection __ctrl)
        {
            WebActionContainer item = this.__BuildControl__control3();
            __ctrl.Add(item);
        }

        [DebuggerNonUserCode]
        private ActionContainerHolder __BuildControlSAC()
        {
            ActionContainerHolder actionContainerHolder = new ActionContainerHolder();
            this.SAC = actionContainerHolder;
            actionContainerHolder.ApplyStyleSheetSkin(this.Page);
            actionContainerHolder.ID = "SAC";
            actionContainerHolder.ContainerStyle = ActionContainerStyle.Links;
            this.__BuildControl__control2(actionContainerHolder.ActionContainers);
            return actionContainerHolder;
        }

        [DebuggerNonUserCode]
        private XafUpdatePanel __BuildControlUPSAC()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            this.UPSAC = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "UPSAC";
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                                "));
            ActionContainerHolder obj = this.__BuildControlSAC();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                            "));
            return xafUpdatePanel;
        }

        [DebuggerNonUserCode]
        private NavigationActionContainer __BuildControlNC()
        {
            NavigationActionContainer navigationActionContainer = new NavigationActionContainer();
            this.NC = navigationActionContainer;
            navigationActionContainer.ApplyStyleSheetSkin(this.Page);
            navigationActionContainer.ApplyStyleSheetThemeInternal();
            navigationActionContainer.ID = "NC";
            navigationActionContainer.ContainerId = "ViewsNavigation";
            navigationActionContainer.Width = new Unit(100.0, UnitType.Percentage);
            navigationActionContainer.BackColor = Color.White;
            return navigationActionContainer;
        }

        [DebuggerNonUserCode]
        private XafUpdatePanel __BuildControlUPNC()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            this.UPNC = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "UPNC";
            xafUpdatePanel.CssClass = "xafContent";
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n            "));
            NavigationActionContainer obj = this.__BuildControlNC();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n        "));
            return xafUpdatePanel;
        }

        [DebuggerNonUserCode]
        private Panel __BuildControlnavigation()
        {
            Panel panel = new Panel();
            this.navigation = panel;
            panel.ApplyStyleSheetSkin(this.Page);
            panel.ID = "navigation";
            panel.CssClass = "xafNav xafNavHidden";
            IParserAccessor parserAccessor = panel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n        "));
            XafUpdatePanel obj = this.__BuildControlUPNC();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n    "));
            return panel;
        }

        [DebuggerNonUserCode]
        private ViewImageControl __BuildControlVIC()
        {
            ViewImageControl viewImageControl = new ViewImageControl();
            this.VIC = viewImageControl;
            viewImageControl.ID = "VIC";
            viewImageControl.CssClass = "ViewImage";
            return viewImageControl;
        }

        [DebuggerNonUserCode]
        private XafUpdatePanel __BuildControlUPVIC()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            this.UPVIC = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "UPVIC";
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                                                "));
            ViewImageControl obj = this.__BuildControlVIC();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                                            "));
            return xafUpdatePanel;
        }

        [DebuggerNonUserCode]
        private ViewCaptionControl __BuildControlVCC()
        {
            ViewCaptionControl viewCaptionControl = new ViewCaptionControl();
            this.VCC = viewCaptionControl;
            viewCaptionControl.ID = "VCC";
            return viewCaptionControl;
        }

        [DebuggerNonUserCode]
        private XafUpdatePanel __BuildControlUPVH()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            this.UPVH = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "UPVH";
            xafUpdatePanel.ForeColor = Color.FromArgb(74, 74, 74);
            xafUpdatePanel.Font.Size = FontUnit.XLarge;
            ((IAttributeAccessor)xafUpdatePanel).SetAttribute("Style", "white-space: normal;");
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                                                "));
            ViewCaptionControl obj = this.__BuildControlVCC();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                                            "));
            return xafUpdatePanel;
        }

        [DebuggerNonUserCode]
        private void __BuildControl__control5(Border __ctrl)
        {
            __ctrl.BorderStyle = BorderStyle.None;
        }

        [DebuggerNonUserCode]
        private void __BuildControl__control6(Border __ctrl)
        {
            __ctrl.BorderStyle = BorderStyle.None;
        }

        [DebuggerNonUserCode]
        private void __BuildControl__control4(ASPxMenu __ctrl)
        {
            __ctrl.Width = new Unit(100.0, UnitType.Percentage);
            __ctrl.ItemAutoWidth = false;
            __ctrl.ClientInstanceName = "mainMenu";
            __ctrl.Font.Size = new FontUnit(new Unit(14.0, UnitType.Pixel));
            __ctrl.EnableAdaptivity = true;
            __ctrl.ItemWrap = false;
            this.__BuildControl__control5(__ctrl.BorderLeft);
            this.__BuildControl__control6(__ctrl.BorderRight);
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control8()
        {
            return new WebActionContainer
            {
                ContainerId = "ObjectsCreation"
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control9()
        {
            return new WebActionContainer
            {
                ContainerId = "Save",
                DefaultActionID = "Save",
                IsDropDown = true,
                AutoChangeDefaultAction = true
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control10()
        {
            return new WebActionContainer
            {
                ContainerId = "Edit"
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control11()
        {
            return new WebActionContainer
            {
                ContainerId = "RecordEdit"
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control12()
        {
            return new WebActionContainer
            {
                ContainerId = "View"
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control13()
        {
            return new WebActionContainer
            {
                ContainerId = "Export"
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control14()
        {
            return new WebActionContainer
            {
                ContainerId = "Reports"
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control15()
        {
            return new WebActionContainer
            {
                ContainerId = "Filters"
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control16()
        {
            return new WebActionContainer
            {
                ContainerId = "RecordsNavigation"
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control17()
        {
            return new WebActionContainer
            {
                ContainerId = "Tools"
            };
        }

        [DebuggerNonUserCode]
        private void __BuildControl__control7(WebActionContainersCollection __ctrl)
        {
            WebActionContainer item = this.__BuildControl__control8();
            __ctrl.Add(item);
            WebActionContainer item2 = this.__BuildControl__control9();
            __ctrl.Add(item2);
            WebActionContainer item3 = this.__BuildControl__control10();
            __ctrl.Add(item3);
            WebActionContainer item4 = this.__BuildControl__control11();
            __ctrl.Add(item4);
            WebActionContainer item5 = this.__BuildControl__control12();
            __ctrl.Add(item5);
            WebActionContainer item6 = this.__BuildControl__control13();
            __ctrl.Add(item6);
            WebActionContainer item7 = this.__BuildControl__control14();
            __ctrl.Add(item7);
            WebActionContainer item8 = this.__BuildControl__control15();
            __ctrl.Add(item8);
            WebActionContainer item9 = this.__BuildControl__control16();
            __ctrl.Add(item9);
            WebActionContainer item10 = this.__BuildControl__control17();
            __ctrl.Add(item10);
        }

        [DebuggerNonUserCode]
        private ActionContainerHolder __BuildControlmainMenu()
        {
            ActionContainerHolder actionContainerHolder = new ActionContainerHolder();
            this.mainMenu = actionContainerHolder;
            actionContainerHolder.ApplyStyleSheetSkin(this.Page);
            actionContainerHolder.ID = "mainMenu";
            actionContainerHolder.ContainerStyle = ActionContainerStyle.Buttons;
            actionContainerHolder.Orientation = ActionContainerOrientation.Horizontal;
            this.__BuildControl__control4(actionContainerHolder.Menu);
            this.__BuildControl__control7(actionContainerHolder.ActionContainers);
            return actionContainerHolder;
        }

        [DebuggerNonUserCode]
        private XafUpdatePanel __BuildControlXafUpdatePanel1()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            this.XafUpdatePanel1 = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "XafUpdatePanel1";
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                                                "));
            ActionContainerHolder obj = this.__BuildControlmainMenu();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                                            "));
            return xafUpdatePanel;
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control19()
        {
            return new WebActionContainer
            {
                ContainerId = "Search"
            };
        }

        [DebuggerNonUserCode]
        private WebActionContainer __BuildControl__control20()
        {
            return new WebActionContainer
            {
                ContainerId = "FullTextSearch"
            };
        }

        [DebuggerNonUserCode]
        private void __BuildControl__control18(WebActionContainersCollection __ctrl)
        {
            WebActionContainer item = this.__BuildControl__control19();
            __ctrl.Add(item);
            WebActionContainer item2 = this.__BuildControl__control20();
            __ctrl.Add(item2);
        }

        [DebuggerNonUserCode]
        private ActionContainerHolder __BuildControlSearchAC()
        {
            ActionContainerHolder actionContainerHolder = new ActionContainerHolder();
            this.SearchAC = actionContainerHolder;
            actionContainerHolder.ApplyStyleSheetSkin(this.Page);
            actionContainerHolder.ID = "SearchAC";
            actionContainerHolder.ContainerStyle = ActionContainerStyle.Buttons;
            actionContainerHolder.Orientation = ActionContainerOrientation.Horizontal;
            this.__BuildControl__control18(actionContainerHolder.ActionContainers);
            return actionContainerHolder;
        }

        [DebuggerNonUserCode]
        private XafUpdatePanel __BuildControlXafUpdatePanel2()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            this.XafUpdatePanel2 = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "XafUpdatePanel2";
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                                                "));
            ActionContainerHolder obj = this.__BuildControlSearchAC();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                                            "));
            return xafUpdatePanel;
        }

        [DebuggerNonUserCode]
        private ErrorInfoControl __BuildControlErrorInfo()
        {
            ErrorInfoControl errorInfoControl = new ErrorInfoControl();
            this.ErrorInfo = errorInfoControl;
            errorInfoControl.ApplyStyleSheetSkin(this.Page);
            errorInfoControl.ID = "ErrorInfo";
            ((IAttributeAccessor)errorInfoControl).SetAttribute("Style", "margin: 10px 0px 10px 0px");
            return errorInfoControl;
        }

        [DebuggerNonUserCode]
        private XafUpdatePanel __BuildControlUPEI()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            this.UPEI = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "UPEI";
            xafUpdatePanel.UpdatePanelForASPxGridListCallback = false;
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                "));
            ErrorInfoControl obj = this.__BuildControlErrorInfo();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n            "));
            return xafUpdatePanel;
        }

        private XafUpdatePanel __BuildControlUPInfoBox()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            //this.UPEI = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "UPInfoBox";
            xafUpdatePanel.UpdatePanelForASPxGridListCallback = false;
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                "));

            //InformationBox obj = new InformationBox();

            var obj = this.LoadControl("~/UIControls/UserControls/InformationBox.ascx");
            
            obj.ApplyStyleSheetSkin(this.Page);
            obj.ID = "InformationBox";
            ((IAttributeAccessor)obj).SetAttribute("Style", "margin: 10px 0px 10px 0px");


            //ErrorInfoControl errorInfoControl = new ErrorInfoControl();
            //this.ErrorInfo = errorInfoControl;
            //errorInfoControl.ApplyStyleSheetSkin(this.Page);
            //errorInfoControl.ID = "ErrorInfo";
            //((IAttributeAccessor)errorInfoControl).SetAttribute("Style", "margin: 10px 0px 10px 0px");
            //return errorInfoControl;

            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n            "));
            return xafUpdatePanel;
        }

        [DebuggerNonUserCode]
        private ViewSiteControl __BuildControlVSC()
        {
            ViewSiteControl viewSiteControl = new ViewSiteControl();
            this.VSC = viewSiteControl;
            viewSiteControl.ID = "VSC";
            return viewSiteControl;
        }

        [DebuggerNonUserCode]
        private XafUpdatePanel __BuildControlUPVSC()
        {
            XafUpdatePanel xafUpdatePanel = new XafUpdatePanel();
            this.UPVSC = xafUpdatePanel;
            xafUpdatePanel.ApplyStyleSheetSkin(this.Page);
            xafUpdatePanel.ID = "UPVSC";
            IParserAccessor parserAccessor = xafUpdatePanel;
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n                "));
            ViewSiteControl obj = this.__BuildControlVSC();
            parserAccessor.AddParsedSubObject(obj);
            parserAccessor.AddParsedSubObject(new LiteralControl("\r\n            "));
            return xafUpdatePanel;
        }

        [DebuggerNonUserCode]
        private AboutInfoControl __BuildControlAIC()
        {
            AboutInfoControl aboutInfoControl = new AboutInfoControl();
            this.AIC = aboutInfoControl;
            aboutInfoControl.ID = "AIC";
            aboutInfoControl.Text = "Copyright text";
            return aboutInfoControl;
        }

        [DebuggerNonUserCode]
        private void __BuildControlTree(DefaultVerticalTemplateContentNew __ctrl)
        {
            ASPxHiddenField obj = this.__BuildControlClientParams();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj);
            XafUpdatePanel obj2 = this.__BuildControlUPPopupWindowControl();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj2);
            XafUpdatePanel obj3 = this.__BuildControlUPSAC();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj3);
            Panel obj4 = this.__BuildControlnavigation();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj4);
            XafUpdatePanel obj5 = this.__BuildControlUPVIC();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj5);
            XafUpdatePanel obj6 = this.__BuildControlUPVH();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj6);
            XafUpdatePanel obj7 = this.__BuildControlXafUpdatePanel1();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj7);
            XafUpdatePanel obj8 = this.__BuildControlXafUpdatePanel2();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj8);
            XafUpdatePanel obj9 = this.__BuildControlUPEI();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj9);
            XafUpdatePanel obj10 = this.__BuildControlUPVSC();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj10);
            AboutInfoControl obj11 = this.__BuildControlAIC();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj11);


            XafUpdatePanel obj12 = this.__BuildControlUPInfoBox();
            ((IParserAccessor)__ctrl).AddParsedSubObject(obj12);


            __ctrl.SetRenderMethodDelegate(new RenderMethod(this.__Render__control1));
        }

        private void __Render__control1(HtmlTextWriter __w, Control parameterContainer)
        {
            __w.Write("\r\n<meta name=\"viewport\" content=\"width=device-width, user-scalable=no, maximum-scale=1.0, minimum-scale=1.0\">\r\n\r\n");
            parameterContainer.Controls[0].RenderControl(__w);
            __w.Write("\r\n");
            parameterContainer.Controls[1].RenderControl(__w);
            __w.Write("\r\n<div id=\"headerDivWithShadow\" style=\"z-index: 2000\">\r\n</div>\r\n<div id=\"TestheaderTableDiv\" style=\"background-color: white; position: absolute; display: none; right: 0px; z-index: 100000\">\r\n</div>\r\n<div class=\"white borderBottom width100\" id=\"headerTableDiv\">\r\n    <div class=\"paddings ");
            __w.Write(DefaultVerticalTemplateContentNew.AdditionalClass);
            __w.Write("\" style=\"margin: auto\">\r\n        <table id=\"headerTable\" class=\"headerTable xafAlignCenter white width100 ");
            __w.Write(DefaultVerticalTemplateContentNew.AdditionalClass);
            __w.Write("\">\r\n            <tbody>\r\n                <tr>\r\n                    <td class=\"xafNavToggleConteiner\">\r\n                        <div id=\"toggleNavigation\" class=\"xafNavToggle\">\r\n                            <div id=\"xafNavTogleActive\" class=\"xafNavHidden ToggleNavigationImage\">\r\n                            </div>\r\n                            <div id=\"xafNavTogle\" class=\"xafNavVisible ToggleNavigationActiveImage\">\r\n                            </div>\r\n                        </div>\r\n                    </td>\r\n                    <td>\r\n                        <div style=\"height: 33px; margin-left: 5px; margin-right: 20px; border-right: 1px solid #c6c6c6\">\r\n                        </div>\r\n                    </td>\r\n                    <td>\r\n                        <img src=\"Images/Logo.png\" />\r\n                    </td>\r\n                    <td class=\"width100\"></td>\r\n                    <td>\r\n                        <div id=\"xafHeaderMenu\" class=\"xafHeaderMenu\" style=\"float: right;\">\r\n                            ");
            parameterContainer.Controls[2].RenderControl(__w);
            __w.Write("\r\n                        </div>\r\n                    </td>\r\n                </tr>\r\n            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n<div id=\"mainDiv\" class=\"xafAlignCenter paddings overflowHidden ");
            __w.Write(DefaultVerticalTemplateContentNew.AdditionalClass);
            __w.Write("\">\r\n    ");
            parameterContainer.Controls[3].RenderControl(__w);
            __w.Write("\r\n    <div id=\"content\" class=\"overflowHidden\">\r\n        <div id=\"menuAreaDiv\" style=\"z-index: 2500\">\r\n            <table id=\"menuInnerTable\" class=\"width100\" style=\"padding-bottom: 13px; padding-top: 13px;\">\r\n                <tbody>\r\n                    <tr>\r\n                        <td class=\"xafNavToggleConteiner\">\r\n                            <div id=\"toggleNavigation_m\" class=\"xafNavToggle xafHidden\">\r\n                                <div id=\"xafNavTogleActive_m\" class=\"xafNavHidden ToggleNavigationImage\">\r\n                                </div>\r\n                                <div id=\"xafNavTogle_m\" class=\"xafNavVisible ToggleNavigationActiveImage\">\r\n                                </div>\r\n                            </div>\r\n                        </td>\r\n                        <td>\r\n                            <div id=\"toggleSeparator_m\" class=\"xafHidden\" style=\"height: 33px; margin-left: 5px; margin-right: 20px; border-right: 1px solid #c6c6c6\">\r\n                            </div>\r\n                        </td>\r\n                        <td style=\"width: 30%\">\r\n                            <table>\r\n                                <tbody>\r\n                                    <tr>\r\n                                        <td>\r\n                                            ");
            parameterContainer.Controls[4].RenderControl(__w);
            __w.Write("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
            parameterContainer.Controls[5].RenderControl(__w);
            __w.Write("\r\n                                        </td>\r\n                                    </tr>\r\n                                </tbody>\r\n                            </table>\r\n                        </td>\r\n                        <td id=\"menuCell\" style=\"width: 70%;\">\r\n                            <table id=\"menuContainer\" style=\"float: right;\">\r\n                                <tbody>\r\n                                    <tr>\r\n                                        <td>\r\n                                            ");
            parameterContainer.Controls[6].RenderControl(__w);
            __w.Write("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
            parameterContainer.Controls[7].RenderControl(__w);
            __w.Write("\r\n                                        </td>\r\n                                    </tr>\r\n                                </tbody>\r\n                            </table>\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n        </div>\r\n        <div id=\"viewSite\" class=\"width100 viewSite\" style=\"float: left\">\r\n            ");
            parameterContainer.Controls[8].RenderControl(__w);
            __w.Write("\r\n            ");
            parameterContainer.Controls[11].RenderControl(__w);
            __w.Write("\r\n            ");
            parameterContainer.Controls[9].RenderControl(__w);
            __w.Write("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div id=\"footer\" class=\"xafFooter width100\">\r\n    <div class=\"xafAlignCenter paddings ");
            __w.Write(DefaultVerticalTemplateContentNew.AdditionalClass);
            __w.Write("\">\r\n        ");
            parameterContainer.Controls[10].RenderControl(__w);
            __w.Write("\r\n    </div>\r\n</div>\r\n");
        }

        [DebuggerNonUserCode]
        protected override void FrameworkInitialize()
        {
            base.FrameworkInitialize();
            this.__BuildControlTree(this);
        }
    }
}