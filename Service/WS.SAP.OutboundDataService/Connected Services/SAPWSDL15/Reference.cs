﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS.SAP.OutboundDataService.SAPWSDL15 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", ConfigurationName="SAPWSDL15.ZLES006")]
    public interface ZLES006 {
        
        // CODEGEN: 参数“Itab”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlArrayAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        WS.SAP.OutboundDataService.SAPWSDL15.Zles006Response Zles006(WS.SAP.OutboundDataService.SAPWSDL15.Zles006Request request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style")]
    public partial class Zsles006 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string aufnrField;
        
        private string nmatnrField;
        
        private string omatnrField;
        
        private decimal mengeField;
        
        private string vlschField;
        
        private string rdateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string Aufnr {
            get {
                return this.aufnrField;
            }
            set {
                this.aufnrField = value;
                this.RaisePropertyChanged("Aufnr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string Nmatnr {
            get {
                return this.nmatnrField;
            }
            set {
                this.nmatnrField = value;
                this.RaisePropertyChanged("Nmatnr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string Omatnr {
            get {
                return this.omatnrField;
            }
            set {
                this.omatnrField = value;
                this.RaisePropertyChanged("Omatnr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public decimal Menge {
            get {
                return this.mengeField;
            }
            set {
                this.mengeField = value;
                this.RaisePropertyChanged("Menge");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string Vlsch {
            get {
                return this.vlschField;
            }
            set {
                this.vlschField = value;
                this.RaisePropertyChanged("Vlsch");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string Rdate {
            get {
                return this.rdateField;
            }
            set {
                this.rdateField = value;
                this.RaisePropertyChanged("Rdate");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style")]
    public partial class Zfles006 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string aufnrField;
        
        private string nmatnrField;
        
        private string omaterField;
        
        private string vlschField;
        
        private string logoField;
        
        private string messField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string Aufnr {
            get {
                return this.aufnrField;
            }
            set {
                this.aufnrField = value;
                this.RaisePropertyChanged("Aufnr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string Nmatnr {
            get {
                return this.nmatnrField;
            }
            set {
                this.nmatnrField = value;
                this.RaisePropertyChanged("Nmatnr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string Omater {
            get {
                return this.omaterField;
            }
            set {
                this.omaterField = value;
                this.RaisePropertyChanged("Omater");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string Vlsch {
            get {
                return this.vlschField;
            }
            set {
                this.vlschField = value;
                this.RaisePropertyChanged("Vlsch");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string Logo {
            get {
                return this.logoField;
            }
            set {
                this.logoField = value;
                this.RaisePropertyChanged("Logo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string Mess {
            get {
                return this.messField;
            }
            set {
                this.messField = value;
                this.RaisePropertyChanged("Mess");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Zles006", WrapperNamespace="urn:sap-com:document:sap:soap:functions:mc-style", IsWrapped=true)]
    public partial class Zles006Request {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=0)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public WS.SAP.OutboundDataService.SAPWSDL15.Zsles006[] Itab;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=1)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public WS.SAP.OutboundDataService.SAPWSDL15.Zfles006[] Return;
        
        public Zles006Request() {
        }
        
        public Zles006Request(WS.SAP.OutboundDataService.SAPWSDL15.Zsles006[] Itab, WS.SAP.OutboundDataService.SAPWSDL15.Zfles006[] Return) {
            this.Itab = Itab;
            this.Return = Return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Zles006Response", WrapperNamespace="urn:sap-com:document:sap:soap:functions:mc-style", IsWrapped=true)]
    public partial class Zles006Response {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=0)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public WS.SAP.OutboundDataService.SAPWSDL15.Zsles006[] Itab;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int LNum;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Logo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Msg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=4)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public WS.SAP.OutboundDataService.SAPWSDL15.Zfles006[] Return;
        
        public Zles006Response() {
        }
        
        public Zles006Response(WS.SAP.OutboundDataService.SAPWSDL15.Zsles006[] Itab, int LNum, string Logo, string Msg, WS.SAP.OutboundDataService.SAPWSDL15.Zfles006[] Return) {
            this.Itab = Itab;
            this.LNum = LNum;
            this.Logo = Logo;
            this.Msg = Msg;
            this.Return = Return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ZLES006Channel : WS.SAP.OutboundDataService.SAPWSDL15.ZLES006, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ZLES006Client : System.ServiceModel.ClientBase<WS.SAP.OutboundDataService.SAPWSDL15.ZLES006>, WS.SAP.OutboundDataService.SAPWSDL15.ZLES006 {
        
        public ZLES006Client() {
        }
        
        public ZLES006Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ZLES006Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ZLES006Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ZLES006Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WS.SAP.OutboundDataService.SAPWSDL15.Zles006Response WS.SAP.OutboundDataService.SAPWSDL15.ZLES006.Zles006(WS.SAP.OutboundDataService.SAPWSDL15.Zles006Request request) {
            return base.Channel.Zles006(request);
        }
        
        public int Zles006(ref WS.SAP.OutboundDataService.SAPWSDL15.Zsles006[] Itab, ref WS.SAP.OutboundDataService.SAPWSDL15.Zfles006[] Return, out string Logo, out string Msg) {
            WS.SAP.OutboundDataService.SAPWSDL15.Zles006Request inValue = new WS.SAP.OutboundDataService.SAPWSDL15.Zles006Request();
            inValue.Itab = Itab;
            inValue.Return = Return;
            WS.SAP.OutboundDataService.SAPWSDL15.Zles006Response retVal = ((WS.SAP.OutboundDataService.SAPWSDL15.ZLES006)(this)).Zles006(inValue);
            Itab = retVal.Itab;
            Logo = retVal.Logo;
            Msg = retVal.Msg;
            Return = retVal.Return;
            return retVal.LNum;
        }
    }
}
