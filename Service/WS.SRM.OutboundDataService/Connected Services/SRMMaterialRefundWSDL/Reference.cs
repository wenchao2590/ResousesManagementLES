﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS.SRM.OutboundDataService.SRMMaterialRefundWSDL {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.foton.com.cn/MaterialRefund", ConfigurationName="SRMMaterialRefundWSDL.MaterialRefundService_ptt")]
    public interface MaterialRefundService_ptt {
        
        // CODEGEN: 消息 MaterialRefundServiceResponse 的包装名称(getMaterialRefundResponse)以后生成的消息协定与默认值(MaterialRefundService)不匹配
        [System.ServiceModel.OperationContractAttribute(Action="MaterialRefundService", ReplyAction="*")]
        WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundServiceResponse MaterialRefundService(WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundServiceRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="MaterialRefundService", WrapperNamespace="http://www.foton.com.cn/MaterialRefund", IsWrapped=true)]
    public partial class MaterialRefundServiceRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/MaterialRefund", Order=0)]
        public string list;
        
        public MaterialRefundServiceRequest() {
        }
        
        public MaterialRefundServiceRequest(string list) {
            this.list = list;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getMaterialRefundResponse", WrapperNamespace="http://www.foton.com.cn/MaterialRefund", IsWrapped=true)]
    public partial class MaterialRefundServiceResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/MaterialRefund", Order=0)]
        public string ExecuteResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/MaterialRefund", Order=1)]
        public string ErrorCode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.foton.com.cn/MaterialRefund", Order=2)]
        public string MessageContent;
        
        public MaterialRefundServiceResponse() {
        }
        
        public MaterialRefundServiceResponse(string ExecuteResult, string ErrorCode, string MessageContent) {
            this.ExecuteResult = ExecuteResult;
            this.ErrorCode = ErrorCode;
            this.MessageContent = MessageContent;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MaterialRefundService_pttChannel : WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundService_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MaterialRefundService_pttClient : System.ServiceModel.ClientBase<WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundService_ptt>, WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundService_ptt {
        
        public MaterialRefundService_pttClient() {
        }
        
        public MaterialRefundService_pttClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MaterialRefundService_pttClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MaterialRefundService_pttClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MaterialRefundService_pttClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundServiceResponse WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundService_ptt.MaterialRefundService(WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundServiceRequest request) {
            return base.Channel.MaterialRefundService(request);
        }
        
        public string MaterialRefundService(string list, out string ErrorCode, out string MessageContent) {
            WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundServiceRequest inValue = new WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundServiceRequest();
            inValue.list = list;
            WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundServiceResponse retVal = ((WS.SRM.OutboundDataService.SRMMaterialRefundWSDL.MaterialRefundService_ptt)(this)).MaterialRefundService(inValue);
            ErrorCode = retVal.ErrorCode;
            MessageContent = retVal.MessageContent;
            return retVal.ExecuteResult;
        }
    }
}