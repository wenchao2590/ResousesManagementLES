﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS.SAP.OutboundDataService.LesSap012 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", ConfigurationName="LesSap012.ZLES008")]
    public interface ZLES008 {
        
        // CODEGEN: 参数“Flag”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="Flag")]
        WS.SAP.OutboundDataService.LesSap012.Zles008Response Zles008(WS.SAP.OutboundDataService.LesSap012.Zles008Request request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style")]
    public partial class Zsles008 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string iblnrField;
        
        private string matnrField;
        
        private string werksField;
        
        private string lgortField;
        
        private decimal mengeField;
        
        private decimal aqtyField;
        
        private decimal dqtyField;
        
        private string gidatField;
        
        private string sjdatField;
        
        private string remarksField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string Iblnr {
            get {
                return this.iblnrField;
            }
            set {
                this.iblnrField = value;
                this.RaisePropertyChanged("Iblnr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string Matnr {
            get {
                return this.matnrField;
            }
            set {
                this.matnrField = value;
                this.RaisePropertyChanged("Matnr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string Werks {
            get {
                return this.werksField;
            }
            set {
                this.werksField = value;
                this.RaisePropertyChanged("Werks");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string Lgort {
            get {
                return this.lgortField;
            }
            set {
                this.lgortField = value;
                this.RaisePropertyChanged("Lgort");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public decimal Aqty {
            get {
                return this.aqtyField;
            }
            set {
                this.aqtyField = value;
                this.RaisePropertyChanged("Aqty");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public decimal Dqty {
            get {
                return this.dqtyField;
            }
            set {
                this.dqtyField = value;
                this.RaisePropertyChanged("Dqty");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string Gidat {
            get {
                return this.gidatField;
            }
            set {
                this.gidatField = value;
                this.RaisePropertyChanged("Gidat");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public string Sjdat {
            get {
                return this.sjdatField;
            }
            set {
                this.sjdatField = value;
                this.RaisePropertyChanged("Sjdat");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=9)]
        public string Remarks {
            get {
                return this.remarksField;
            }
            set {
                this.remarksField = value;
                this.RaisePropertyChanged("Remarks");
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
    public partial class Zfles008 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string iblnrField;
        
        private string matnrField;
        
        private string werksField;
        
        private string lgortField;
        
        private string logoField;
        
        private string msgField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string Iblnr {
            get {
                return this.iblnrField;
            }
            set {
                this.iblnrField = value;
                this.RaisePropertyChanged("Iblnr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string Matnr {
            get {
                return this.matnrField;
            }
            set {
                this.matnrField = value;
                this.RaisePropertyChanged("Matnr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string Werks {
            get {
                return this.werksField;
            }
            set {
                this.werksField = value;
                this.RaisePropertyChanged("Werks");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string Lgort {
            get {
                return this.lgortField;
            }
            set {
                this.lgortField = value;
                this.RaisePropertyChanged("Lgort");
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
        public string Msg {
            get {
                return this.msgField;
            }
            set {
                this.msgField = value;
                this.RaisePropertyChanged("Msg");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName="Zles008", WrapperNamespace="urn:sap-com:document:sap:soap:functions:mc-style", IsWrapped=true)]
    public partial class Zles008Request {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=0)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public WS.SAP.OutboundDataService.LesSap012.Zsles008[] Itab;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=1)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public WS.SAP.OutboundDataService.LesSap012.Zfles008[] Return;
        
        public Zles008Request() {
        }
        
        public Zles008Request(WS.SAP.OutboundDataService.LesSap012.Zsles008[] Itab, WS.SAP.OutboundDataService.LesSap012.Zfles008[] Return) {
            this.Itab = Itab;
            this.Return = Return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Zles008Response", WrapperNamespace="urn:sap-com:document:sap:soap:functions:mc-style", IsWrapped=true)]
    public partial class Zles008Response {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Flag;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=1)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public WS.SAP.OutboundDataService.LesSap012.Zsles008[] Itab;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int LNum;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Msg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style", Order=4)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public WS.SAP.OutboundDataService.LesSap012.Zfles008[] Return;
        
        public Zles008Response() {
        }
        
        public Zles008Response(string Flag, WS.SAP.OutboundDataService.LesSap012.Zsles008[] Itab, int LNum, string Msg, WS.SAP.OutboundDataService.LesSap012.Zfles008[] Return) {
            this.Flag = Flag;
            this.Itab = Itab;
            this.LNum = LNum;
            this.Msg = Msg;
            this.Return = Return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ZLES008Channel : WS.SAP.OutboundDataService.LesSap012.ZLES008, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ZLES008Client : System.ServiceModel.ClientBase<WS.SAP.OutboundDataService.LesSap012.ZLES008>, WS.SAP.OutboundDataService.LesSap012.ZLES008 {
        
        public ZLES008Client() {
        }
        
        public ZLES008Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ZLES008Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ZLES008Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ZLES008Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WS.SAP.OutboundDataService.LesSap012.Zles008Response WS.SAP.OutboundDataService.LesSap012.ZLES008.Zles008(WS.SAP.OutboundDataService.LesSap012.Zles008Request request) {
            return base.Channel.Zles008(request);
        }
        
        public string Zles008(ref WS.SAP.OutboundDataService.LesSap012.Zsles008[] Itab, ref WS.SAP.OutboundDataService.LesSap012.Zfles008[] Return, out int LNum, out string Msg) {
            WS.SAP.OutboundDataService.LesSap012.Zles008Request inValue = new WS.SAP.OutboundDataService.LesSap012.Zles008Request();
            inValue.Itab = Itab;
            inValue.Return = Return;
            WS.SAP.OutboundDataService.LesSap012.Zles008Response retVal = ((WS.SAP.OutboundDataService.LesSap012.ZLES008)(this)).Zles008(inValue);
            Itab = retVal.Itab;
            LNum = retVal.LNum;
            Msg = retVal.Msg;
            Return = retVal.Return;
            return retVal.Flag;
        }
    }
}