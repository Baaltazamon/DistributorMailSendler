﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DistributorMailSendler.OldDistributorsMaterialArticul {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.khortitsa.com/wcf/Money/2008", ConfigurationName="OldDistributorsMaterialArticul.IDistributorsMaterialArticul")]
    public interface IDistributorsMaterialArticul {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.khortitsa.com/wcf/Money/2008/IDistributorsMaterialArticul_nonDataSets/" +
            "CollateMaterArticul", ReplyAction="http://www.khortitsa.com/wcf/Money/2008/IDistributorsMaterialArticul_nonDataSets/" +
            "CollateMaterArticulResponse")]
        int CollateMaterArticul(System.Nullable<double> paramCodeEan, string paramArticul, string paramMaterName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.khortitsa.com/wcf/Money/2008/IDistributorsMaterialArticul_nonDataSets/" +
            "CollateMaterArticul", ReplyAction="http://www.khortitsa.com/wcf/Money/2008/IDistributorsMaterialArticul_nonDataSets/" +
            "CollateMaterArticulResponse")]
        System.Threading.Tasks.Task<int> CollateMaterArticulAsync(System.Nullable<double> paramCodeEan, string paramArticul, string paramMaterName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDistributorsMaterialArticulChannel : DistributorMailSendler.OldDistributorsMaterialArticul.IDistributorsMaterialArticul, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DistributorsMaterialArticulClient : System.ServiceModel.ClientBase<DistributorMailSendler.OldDistributorsMaterialArticul.IDistributorsMaterialArticul>, DistributorMailSendler.OldDistributorsMaterialArticul.IDistributorsMaterialArticul {
        
        public DistributorsMaterialArticulClient() {
        }
        
        public DistributorsMaterialArticulClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DistributorsMaterialArticulClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DistributorsMaterialArticulClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DistributorsMaterialArticulClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int CollateMaterArticul(System.Nullable<double> paramCodeEan, string paramArticul, string paramMaterName) {
            return base.Channel.CollateMaterArticul(paramCodeEan, paramArticul, paramMaterName);
        }
        
        public System.Threading.Tasks.Task<int> CollateMaterArticulAsync(System.Nullable<double> paramCodeEan, string paramArticul, string paramMaterName) {
            return base.Channel.CollateMaterArticulAsync(paramCodeEan, paramArticul, paramMaterName);
        }
    }
}
