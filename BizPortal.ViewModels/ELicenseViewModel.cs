using BizPortal.Models;
using EGA.EGA_Development.Util.MailV2.Data;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BizPortal.ViewModels
{
    public class ELicenseViewModel
    {
        public Guid? ApplicationRequestID { get; set; }

        public string TemplateID { get; set; }
        
        public string DocumentID { get; set; }

        public string Name { get; set; }

        public Base64Attachment Attachment { get; set; }

        public string Url { get; set; }

        public bool IsPendingSigning { get; set; }

        public string SigningDocumentType { get; set; }

        public string SigningType { get; set; }

        public List<SigningExtendedDataViewData> SigningExtendedDatas { get; set; }

        public List<SigningPositionViewModel> SigningPositions { get; set; }

        public List<SigningPersonViewModel> SigningPersons { get; set; }
        
        public string SigningStatus { get; set; }
    }

    public class SigningExtendedDataViewData 
    {
        public SigningExtendedDataType Type { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class SigningPositionViewModel
    {
        public string Left { get; set; }

        public string Bottom { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }
    }

    public class SigningPersonViewModel
    {
        public int SigningPersonID { get; set; }

        public int Order { get; set; }

        public string Left { get; set; }

        public string Bottom { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public string CitizenID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string SignatureBase64 { get; set; }

        public PersonalSigningStatus Status { get; set; }

        public string Remark { get; set; }
    }

    public class ElicenseRenderViewModel
    {
        public List<ELicenseViewModel> ELicenses { get; set; }

    }

    public class ElicenseRenderResponseModel 
    {
        public string TemplateID { get; set; }

        public string DocumentID { get; set; }
    }
}
