﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BITCollegeWindows.BITCollegeService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BITCollegeService.ICollegeRegistration")]
    public interface ICollegeRegistration {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICollegeRegistration/DropCourse", ReplyAction="http://tempuri.org/ICollegeRegistration/DropCourseResponse")]
        bool DropCourse(int registrationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICollegeRegistration/DropCourse", ReplyAction="http://tempuri.org/ICollegeRegistration/DropCourseResponse")]
        System.Threading.Tasks.Task<bool> DropCourseAsync(int registrationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICollegeRegistration/RegisterCourse", ReplyAction="http://tempuri.org/ICollegeRegistration/RegisterCourseResponse")]
        int RegisterCourse(int studentId, int courseId, string notes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICollegeRegistration/RegisterCourse", ReplyAction="http://tempuri.org/ICollegeRegistration/RegisterCourseResponse")]
        System.Threading.Tasks.Task<int> RegisterCourseAsync(int studentId, int courseId, string notes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICollegeRegistration/UpdateGrade", ReplyAction="http://tempuri.org/ICollegeRegistration/UpdateGradeResponse")]
        System.Nullable<double> UpdateGrade(double grade, int registrationId, string notes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICollegeRegistration/UpdateGrade", ReplyAction="http://tempuri.org/ICollegeRegistration/UpdateGradeResponse")]
        System.Threading.Tasks.Task<System.Nullable<double>> UpdateGradeAsync(double grade, int registrationId, string notes);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICollegeRegistrationChannel : BITCollegeWindows.BITCollegeService.ICollegeRegistration, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CollegeRegistrationClient : System.ServiceModel.ClientBase<BITCollegeWindows.BITCollegeService.ICollegeRegistration>, BITCollegeWindows.BITCollegeService.ICollegeRegistration {
        
        public CollegeRegistrationClient() {
        }
        
        public CollegeRegistrationClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CollegeRegistrationClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CollegeRegistrationClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CollegeRegistrationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool DropCourse(int registrationId) {
            return base.Channel.DropCourse(registrationId);
        }
        
        public System.Threading.Tasks.Task<bool> DropCourseAsync(int registrationId) {
            return base.Channel.DropCourseAsync(registrationId);
        }
        
        public int RegisterCourse(int studentId, int courseId, string notes) {
            return base.Channel.RegisterCourse(studentId, courseId, notes);
        }
        
        public System.Threading.Tasks.Task<int> RegisterCourseAsync(int studentId, int courseId, string notes) {
            return base.Channel.RegisterCourseAsync(studentId, courseId, notes);
        }
        
        public System.Nullable<double> UpdateGrade(double grade, int registrationId, string notes) {
            return base.Channel.UpdateGrade(grade, registrationId, notes);
        }
        
        public System.Threading.Tasks.Task<System.Nullable<double>> UpdateGradeAsync(double grade, int registrationId, string notes) {
            return base.Channel.UpdateGradeAsync(grade, registrationId, notes);
        }
    }
}
