﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _711_A1_Cache.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IServerService")]
    public interface IServerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerService/GetFile", ReplyAction="http://tempuri.org/IServerService/GetFileResponse")]
        byte[] GetFile(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerService/GetFile", ReplyAction="http://tempuri.org/IServerService/GetFileResponse")]
        System.Threading.Tasks.Task<byte[]> GetFileAsync(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerService/GetFileList", ReplyAction="http://tempuri.org/IServerService/GetFileListResponse")]
        string[] GetFileList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServerService/GetFileList", ReplyAction="http://tempuri.org/IServerService/GetFileListResponse")]
        System.Threading.Tasks.Task<string[]> GetFileListAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServerServiceChannel : _711_A1_Cache.ServiceReference1.IServerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServerServiceClient : System.ServiceModel.ClientBase<_711_A1_Cache.ServiceReference1.IServerService>, _711_A1_Cache.ServiceReference1.IServerService {
        
        public ServerServiceClient() {
        }
        
        public ServerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public byte[] GetFile(string fileName) {
            return base.Channel.GetFile(fileName);
        }
        
        public System.Threading.Tasks.Task<byte[]> GetFileAsync(string fileName) {
            return base.Channel.GetFileAsync(fileName);
        }
        
        public string[] GetFileList() {
            return base.Channel.GetFileList();
        }
        
        public System.Threading.Tasks.Task<string[]> GetFileListAsync() {
            return base.Channel.GetFileListAsync();
        }
    }
}
