﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS.SRM.OutboundDataService.SRMMaterialPullWSDL {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.foton.com.cn/MaterialPull", ConfigurationName="SRMMaterialPullWSDL.MaterialPullService_ptt")]
    public interface MaterialPullService_ptt {
        
        // CODEGEN: 消息 MaterialPullServiceResponse 的包装名称(getMaterialPullResponse)以后生成的消息协定与默认值(MaterialPullService)不匹配
        [System.ServiceModel.OperationContractAttribute(Action="MaterialPullService", ReplyAction="*")]
        WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullServiceResponse MaterialPullService(WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullServiceRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="MaterialPullService", WrapperNamespace="http://www.foton.com.cn/MaterialPull", IsWrapped=true)]
    public partial class MaterialPullServiceRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/MaterialPull", Order=0)]
        public string list;
        
        public MaterialPullServiceRequest() {
        }
        
        public MaterialPullServiceRequest(string list) {
            this.list = list;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getMaterialPullResponse", WrapperNamespace="http://www.foton.com.cn/MaterialPull", IsWrapped=true)]
    public partial class MaterialPullServiceResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/MaterialPull", Order=0)]
        public string ExecuteResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/MaterialPull", Order=1)]
        public string ErrorCode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/MaterialPull", Order=2)]
        public string MessageContent;
        
        public MaterialPullServiceResponse() {
        }
        
        public MaterialPullServiceResponse(string ExecuteResult, string ErrorCode, string MessageContent) {
            this.ExecuteResult = ExecuteResult;
            this.ErrorCode = ErrorCode;
            this.MessageContent = MessageContent;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MaterialPullService_pttChannel : WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullService_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MaterialPullService_pttClient : System.ServiceModel.ClientBase<WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullService_ptt>, WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullService_ptt {
        
        public MaterialPullService_pttClient() {
        }
        
        public MaterialPullService_pttClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MaterialPullService_pttClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MaterialPullService_pttClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MaterialPullService_pttClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullServiceResponse WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullService_ptt.MaterialPullService(WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullServiceRequest request) {
            return base.Channel.MaterialPullService(request);
        }
        
        public string MaterialPullService(string list, out string ErrorCode, out string MessageContent) {
            WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullServiceRequest inValue = new WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullServiceRequest();
            inValue.list = list;
            WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullServiceResponse retVal = ((WS.SRM.OutboundDataService.SRMMaterialPullWSDL.MaterialPullService_ptt)(this)).MaterialPullService(inValue);
            ErrorCode = retVal.ErrorCode;
            MessageContent = retVal.MessageContent;
            return retVal.ExecuteResult;
        }
    }
}