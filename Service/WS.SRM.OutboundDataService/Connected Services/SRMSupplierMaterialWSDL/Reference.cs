﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.foton.com.cn/SupplierMaterial", ConfigurationName="SRMSupplierMaterialWSDL.SupplierMaterialService_ptt")]
    public interface SupplierMaterialService_ptt {
        
        // CODEGEN: 消息 SupplierMaterialServiceResponse 的包装名称(getSupplierMaterialResponse)以后生成的消息协定与默认值(SupplierMaterialService)不匹配
        [System.ServiceModel.OperationContractAttribute(Action="SupplierMaterialService", ReplyAction="*")]
        WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialServiceResponse SupplierMaterialService(WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialServiceRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SupplierMaterialService", WrapperNamespace="http://www.foton.com.cn/SupplierMaterial", IsWrapped=true)]
    public partial class SupplierMaterialServiceRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/SupplierMaterial", Order=0)]
        public string list;
        
        public SupplierMaterialServiceRequest() {
        }
        
        public SupplierMaterialServiceRequest(string list) {
            this.list = list;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getSupplierMaterialResponse", WrapperNamespace="http://www.foton.com.cn/SupplierMaterial", IsWrapped=true)]
    public partial class SupplierMaterialServiceResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/SupplierMaterial", Order=0)]
        public string ExecuteResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/SupplierMaterial", Order=1)]
        public string ErrorCode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/SupplierMaterial", Order=2)]
        public string MessageContent;
        
        public SupplierMaterialServiceResponse() {
        }
        
        public SupplierMaterialServiceResponse(string ExecuteResult, string ErrorCode, string MessageContent) {
            this.ExecuteResult = ExecuteResult;
            this.ErrorCode = ErrorCode;
            this.MessageContent = MessageContent;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SupplierMaterialService_pttChannel : WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialService_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SupplierMaterialService_pttClient : System.ServiceModel.ClientBase<WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialService_ptt>, WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialService_ptt {
        
        public SupplierMaterialService_pttClient() {
        }
        
        public SupplierMaterialService_pttClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SupplierMaterialService_pttClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupplierMaterialService_pttClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupplierMaterialService_pttClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialServiceResponse WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialService_ptt.SupplierMaterialService(WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialServiceRequest request) {
            return base.Channel.SupplierMaterialService(request);
        }
        
        public string SupplierMaterialService(string list, out string ErrorCode, out string MessageContent) {
            WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialServiceRequest inValue = new WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialServiceRequest();
            inValue.list = list;
            WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialServiceResponse retVal = ((WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL.SupplierMaterialService_ptt)(this)).SupplierMaterialService(inValue);
            ErrorCode = retVal.ErrorCode;
            MessageContent = retVal.MessageContent;
            return retVal.ExecuteResult;
        }
    }
}