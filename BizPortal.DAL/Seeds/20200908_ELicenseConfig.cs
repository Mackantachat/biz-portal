using BizPortal.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity;

namespace BizPortal.DAL.Seeds
{
    public class _20200908_ELicenseConfig
    {
        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser adminUser)
        {
            #region [ปศุสัตว์]

            // ขอใบอนุญาตดำเนินการสถานพยาบาลสัตว์
            var dldAnimalLicense = context.Applications
                                          .Include(e => e.SigningExtendedDatas)
                                          .Include(e => e.SigningPersons)
                                          .Include(e => e.SigningPositions)
                                          .Where(e => e.ApplicationSysName == "ขอใบอนุญาตดำเนินการสถานพยาบาลสัตว์" && !e.IsDeleted).FirstOrDefault();

            if (dldAnimalLicense != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(dldAnimalLicense.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(dldAnimalLicense.SigningPersons);
                context.SigningPositions.RemoveRange(dldAnimalLicense.SigningPositions);

                // update
                dldAnimalLicense.IsEnableELicense = true;
                dldAnimalLicense.ELicenseConsumerKey = "f17475e4-ab54-4c05-9ddd-497545329c52";
                dldAnimalLicense.ELicenseSecret = "AR6nUJcZk7N";
                dldAnimalLicense.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                dldAnimalLicense.SigningDocumentCitizenTemplateID = "c433e08b-ad07-411f-b336-471bbab25bfb";
                dldAnimalLicense.SigningDocumentJuristicTemplateID = "c433e08b-ad07-411f-b336-471bbab25bfb";
                dldAnimalLicense.SigningDocumentType = EDocumentPermitType.Template.ToString();
                dldAnimalLicense.SigningType = EDocumentType.OrgByPerson.ToString();
                dldAnimalLicense.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ใช้ได้ตั้งแต่ วันที่",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ถึงวันที่",
                        Name ="PermitEndDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ใช้ได้ตั้งแต่ วันที่",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ถึงวันที่",
                        Name ="PermitEndDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    }
                };
                dldAnimalLicense.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                dldAnimalLicense.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "325",
                        Bottom = "265",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, dldAnimalLicense);
                context.SaveChanges(false);
            }



            // ขอใบอนุญาตให้ตั้งสถานพยาบาลสัตว์ (ต้องได้รับหนังสืออนุมัติแผนการจัดตั้งสถานพยาบาลสัตว์ก่อน)
            var dldAnimalMedAppHook = context.Applications
                                        .Include(e => e.SigningExtendedDatas)
                                        .Include(e => e.SigningPersons)
                                        .Include(e => e.SigningPositions)
                                        .Where(e => e.ApplicationSysName == "ขอใบอนุญาตให้ตั้งสถานพยาบาลสัตว์" && !e.IsDeleted).FirstOrDefault();

            if (dldAnimalMedAppHook != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(dldAnimalMedAppHook.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(dldAnimalMedAppHook.SigningPersons);
                context.SigningPositions.RemoveRange(dldAnimalMedAppHook.SigningPositions);

                // update
                dldAnimalMedAppHook.IsEnableELicense = true;
                dldAnimalMedAppHook.ELicenseConsumerKey = "f17475e4-ab54-4c05-9ddd-497545329c52";
                dldAnimalMedAppHook.ELicenseSecret = "AR6nUJcZk7N";
                dldAnimalMedAppHook.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                dldAnimalMedAppHook.SigningDocumentCitizenTemplateID = "16f65426-c3d2-44d1-bde8-7227a82046e3";
                dldAnimalMedAppHook.SigningDocumentJuristicTemplateID = "b82fdfe8-ef14-43e1-9fd8-b025783737b4";
                dldAnimalMedAppHook.SigningDocumentType = EDocumentPermitType.Template.ToString();
                dldAnimalMedAppHook.SigningType = EDocumentType.OrgByPerson.ToString();
                dldAnimalMedAppHook.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้ตั้งแต่วันที่",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ถึงวันที่",
                        Name ="PermitEndDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                       
                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้ตั้งแต่วันที่ ",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ถึงวันที่",
                        Name ="PermitEndDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    }


                };
                dldAnimalMedAppHook.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                dldAnimalMedAppHook.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "325",
                        Bottom = "265",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, dldAnimalMedAppHook);
                context.SaveChanges(false);
            }


            // ขอหนังสืออนุมัติแผนการจัดตั้งสถานพยาบาลสัตว์
            var dldAnimalBuild = context.Applications
                                       .Include(e => e.SigningExtendedDatas)
                                       .Include(e => e.SigningPersons)
                                       .Include(e => e.SigningPositions)
                                       .Where(e => e.ApplicationSysName == "ขอหนังสืออนุมัติแผนการจัดตั้งสถานพยาบาลสัตว์" && !e.IsDeleted).FirstOrDefault();

            if (dldAnimalBuild != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(dldAnimalBuild.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(dldAnimalBuild.SigningPersons);
                context.SigningPositions.RemoveRange(dldAnimalBuild.SigningPositions);

                // update
                dldAnimalBuild.IsEnableELicense = true;
                dldAnimalBuild.ELicenseConsumerKey = "f17475e4-ab54-4c05-9ddd-497545329c52";
                dldAnimalBuild.ELicenseSecret = "AR6nUJcZk7N";
                dldAnimalBuild.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                dldAnimalBuild.SigningDocumentCitizenTemplateID = "43a0428a-2a65-41a7-9bf7-ea9dc3c847e1";
                dldAnimalBuild.SigningDocumentJuristicTemplateID = "d4b6cab1-768c-44d9-ba31-4440967e542f";
                dldAnimalBuild.SigningDocumentType = EDocumentPermitType.Template.ToString();
                dldAnimalBuild.SigningType = EDocumentType.OrgByPerson.ToString();
                dldAnimalBuild.SigningExtendedDatas = new List<SigningExtendedData>
                {

                    // citizen
                    new SigningExtendedData
                    {
                        Label = "เลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ใช้ได้จนถึงวันที่",
                        Name ="PermitEndDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ออกให้ ณ วันที่",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "เลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ออกให้ ณ วันที่",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ใช้ได้จนถึงวันที่",
                        Name ="PermitEndDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    }
                };
                dldAnimalBuild.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                dldAnimalBuild.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "325",
                        Bottom = "265",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, dldAnimalBuild);
                context.SaveChanges(false);
            }

            #endregion

            #region [สบส]

            // ขอใบอนุญาตประกอบกิจการสถานพยาบาล (คลินิก) สพ7
            var hssClinicNew = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();

            if (hssClinicNew != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssClinicNew.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssClinicNew.SigningPersons);
                context.SigningPositions.RemoveRange(hssClinicNew.SigningPositions);

                // update
                hssClinicNew.IsEnableELicense = true;
                hssClinicNew.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssClinicNew.ELicenseSecret = "7Y4JWMjkp5a";
                dldAnimalBuild.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssClinicNew.SigningDocumentCitizenTemplateID = "d509355d-abf7-44fa-a7df-7250d2d1ddb6";
                hssClinicNew.SigningDocumentJuristicTemplateID = "d509355d-abf7-44fa-a7df-7250d2d1ddb6";
                hssClinicNew.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssClinicNew.SigningType = EDocumentType.OrgByPerson.ToString();
                hssClinicNew.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่ 31 ธันวาคม พ.ศ. ",
                        Name ="PermitEndYear",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ไว้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่ 31 ธันวาคม พ.ศ. ",
                        Name ="PermitEndYear",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ไว้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssClinicNew.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssClinicNew.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssClinicNew);
                context.SaveChanges(false);
            }

            // ขออนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)
            var hssClinicBusiness = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC_BUSINESS" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();

            if (hssClinicBusiness != null)
            {
                // clear
                var items = context.SigningExtendedDatas.Where(o => o.ApplicationID == hssClinicBusiness.ApplicationID);
                context.SigningExtendedDatas.RemoveRange(items);

                context.SigningExtendedDatas.RemoveRange(hssClinicBusiness.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssClinicBusiness.SigningPersons);
                context.SigningPositions.RemoveRange(hssClinicBusiness.SigningPositions);

                // update
                hssClinicBusiness.IsEnableELicense = true;
                hssClinicBusiness.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssClinicBusiness.ELicenseSecret = "7Y4JWMjkp5a";
                hssClinicBusiness.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssClinicBusiness.SigningDocumentCitizenTemplateID = "d509355d-abf7-44fa-a7df-7250d2d1ddb6";
                hssClinicBusiness.SigningDocumentJuristicTemplateID = "d509355d-abf7-44fa-a7df-7250d2d1ddb6";
                hssClinicBusiness.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssClinicBusiness.SigningType = EDocumentType.OrgByPerson.ToString();
                hssClinicBusiness.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่ 31 ธันวาคม พ.ศ. ",
                        Name ="PermitEndYear",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ไว้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่ 31 ธันวาคม พ.ศ. ",
                        Name ="PermitEndYear",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ไว้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssClinicBusiness.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssClinicBusiness.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssClinicBusiness);
                context.SaveChanges(false);
            }

            // ขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)
            var hssClinicOperation = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC_OPERATION" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();

            if (hssClinicOperation != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssClinicOperation.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssClinicOperation.SigningPersons);
                context.SigningPositions.RemoveRange(hssClinicOperation.SigningPositions);

                // update
                hssClinicOperation.IsEnableELicense = true;
                hssClinicOperation.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssClinicOperation.ELicenseSecret = "7Y4JWMjkp5a";
                hssClinicOperation.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssClinicOperation.SigningDocumentCitizenTemplateID = "7191ac4d-2efb-4ae1-908c-32b3c9d3ef38";
                hssClinicOperation.SigningDocumentJuristicTemplateID = "7191ac4d-2efb-4ae1-908c-32b3c9d3ef38";
                hssClinicOperation.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssClinicOperation.SigningType = EDocumentType.OrgByPerson.ToString();
                hssClinicOperation.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่ 31 ธันวาคม พ.ศ. ",
                        Name ="PermitEndYear",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ไว้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                     new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่ 31 ธันวาคม พ.ศ. ",
                        Name ="PermitEndYear",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ไว้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssClinicOperation.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssClinicOperation.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssClinicOperation);
                context.SaveChanges(false);
            }

            // การชำระค่าธรรมเนียมประจำปี (คลินิก)
            var hssClinicFee = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC_NOT_ONE_NIGHT_STAND" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssClinicFee != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssClinicFee.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssClinicFee.SigningPersons);
                context.SigningPositions.RemoveRange(hssClinicFee.SigningPositions);

                // update
                hssClinicFee.IsEnableELicense = true;
                hssClinicFee.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssClinicFee.ELicenseSecret = "7Y4JWMjkp5a";
                hssClinicFee.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssClinicFee.SigningDocumentCitizenTemplateID = "f7b5667f-6f6c-4d49-9b5a-b532041debee";
                hssClinicFee.SigningDocumentJuristicTemplateID = "f7b5667f-6f6c-4d49-9b5a-b532041debee";
                hssClinicFee.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssClinicFee.SigningType = EDocumentType.OrgByPerson.ToString();
                hssClinicFee.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "เอกสารเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ชำระค่าธรรมเนียมประจำปี",
                        Name ="YearOfFee",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "โปรดชำระค่าธรรมเนียมประจำปี",
                        Name ="Year",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                   
                    // juristic
                   new SigningExtendedData
                    {
                        Label = "เอกสารเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ชำระค่าธรรมเนียมประจำปี",
                        Name ="YearOfFee",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "โปรดชำระค่าธรรมเนียมประจำปี",
                        Name ="Year",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },

                };
                hssClinicFee.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssClinicFee.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssClinicFee);
                context.SaveChanges(false);
            }

            // ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)
            var hssClinicBussinessRenew = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC_BUSINESS_RENEW" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssClinicBussinessRenew != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssClinicBussinessRenew.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssClinicBussinessRenew.SigningPersons);
                context.SigningPositions.RemoveRange(hssClinicBussinessRenew.SigningPositions);

                // update
                hssClinicBussinessRenew.IsEnableELicense = true;
                hssClinicBussinessRenew.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssClinicBussinessRenew.ELicenseSecret = "7Y4JWMjkp5a";
                hssClinicBussinessRenew.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssClinicBussinessRenew.SigningDocumentCitizenTemplateID = "ee924af8-2d30-40fa-8124-671e205ace52";
                hssClinicBussinessRenew.SigningDocumentJuristicTemplateID = "ee924af8-2d30-40fa-8124-671e205ace52";
                hssClinicBussinessRenew.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssClinicBussinessRenew.SigningType = EDocumentType.OrgByPerson.ToString();
                hssClinicBussinessRenew.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssClinicBussinessRenew.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssClinicBussinessRenew.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssClinicBussinessRenew);
                context.SaveChanges(false);
            }

            // ขอต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)
            var hssClinicOperationRenew = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC_OPERATION_RENEW" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssClinicOperationRenew != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssClinicOperationRenew.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssClinicOperationRenew.SigningPersons);
                context.SigningPositions.RemoveRange(hssClinicOperationRenew.SigningPositions);

                // update
                hssClinicOperationRenew.IsEnableELicense = true;
                hssClinicOperationRenew.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssClinicOperationRenew.ELicenseSecret = "7Y4JWMjkp5a";
                hssClinicOperationRenew.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssClinicOperationRenew.SigningDocumentCitizenTemplateID = "50596c27-0297-4c8e-8c34-ec903f81d285";
                hssClinicOperationRenew.SigningDocumentJuristicTemplateID = "50596c27-0297-4c8e-8c34-ec903f81d285";
                hssClinicOperationRenew.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssClinicOperationRenew.SigningType = EDocumentType.OrgByPerson.ToString();
                hssClinicOperationRenew.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                     new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssClinicOperationRenew.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssClinicOperationRenew.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssClinicOperationRenew);
                context.SaveChanges(false);
            }

            // ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)
            var hssClinicBussinessEdit = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC_BUSINESS_EDIT" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssClinicBussinessEdit != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssClinicBussinessEdit.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssClinicBussinessEdit.SigningPersons);
                context.SigningPositions.RemoveRange(hssClinicBussinessEdit.SigningPositions);

                // update
                hssClinicBussinessEdit.IsEnableELicense = true;
                hssClinicBussinessEdit.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssClinicBussinessEdit.ELicenseSecret = "7Y4JWMjkp5a";
                hssClinicBussinessEdit.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssClinicBussinessEdit.SigningDocumentCitizenTemplateID = "d8082d37-0333-4430-a4ec-864676b7eab6";
                hssClinicBussinessEdit.SigningDocumentJuristicTemplateID = "d8082d37-0333-4430-a4ec-864676b7eab6";
                hssClinicBussinessEdit.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssClinicBussinessEdit.SigningType = EDocumentType.OrgByPerson.ToString();
                hssClinicBussinessEdit.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssClinicBussinessEdit.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssClinicBussinessEdit.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssClinicBussinessEdit);
                context.SaveChanges(false);
            }

            // ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (คลินิก)
            var hssClinicOperationEdit = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC_OPERATION_EDIT" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssClinicOperationEdit != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssClinicOperationEdit.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssClinicOperationEdit.SigningPersons);
                context.SigningPositions.RemoveRange(hssClinicOperationEdit.SigningPositions);

                // update
                hssClinicOperationEdit.IsEnableELicense = true;
                hssClinicOperationEdit.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssClinicOperationEdit.ELicenseSecret = "7Y4JWMjkp5a";
                hssClinicOperationEdit.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssClinicOperationEdit.SigningDocumentCitizenTemplateID = "fcf01981-ca8b-4ed4-9d7b-66dabfbd4cb7";
                hssClinicOperationEdit.SigningDocumentJuristicTemplateID = "fcf01981-ca8b-4ed4-9d7b-66dabfbd4cb7";
                hssClinicOperationEdit.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssClinicOperationEdit.SigningType = EDocumentType.OrgByPerson.ToString();
                hssClinicOperationEdit.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssClinicOperationEdit.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssClinicOperationEdit.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssClinicOperationEdit);
                context.SaveChanges(false);
            }

            //ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)
            var hssClinicOperationEditB = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC_OPERATION_EDIT_B" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssClinicOperationEditB != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssClinicOperationEditB.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssClinicOperationEditB.SigningPersons);
                context.SigningPositions.RemoveRange(hssClinicOperationEditB.SigningPositions);

                // update
                hssClinicOperationEditB.IsEnableELicense = true;
                hssClinicOperationEditB.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssClinicOperationEditB.ELicenseSecret = "7Y4JWMjkp5a";
                hssClinicOperationEditB.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssClinicOperationEditB.SigningDocumentCitizenTemplateID = "";
                hssClinicOperationEditB.SigningDocumentJuristicTemplateID = "";
                hssClinicOperationEditB.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssClinicOperationEditB.SigningType = EDocumentType.OrgByPerson.ToString();
                hssClinicOperationEditB.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssClinicOperationEditB.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssClinicOperationEditB.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssClinicOperationEditB);
                context.SaveChanges(false);
            }

            // ขออนุมัติแผนงานการจัดตั้งสถานพยาบาล(โรงพยาบาล)
            var hssHospitalNew = context.Applications.Where(e => e.ApplicationSysName == "APP_HOSPITAL" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();

            if (hssHospitalNew != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalNew.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalNew.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalNew.SigningPositions);

                // update
                hssHospitalNew.IsEnableELicense = true;
                hssHospitalNew.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalNew.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalNew.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalNew.SigningDocumentCitizenTemplateID = "8cb2fedd-dcfd-4019-ba9d-461ffe63bc4e";
                hssHospitalNew.SigningDocumentJuristicTemplateID = "8cb2fedd-dcfd-4019-ba9d-461ffe63bc4e";
                hssHospitalNew.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalNew.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalNew.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalNew.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalNew);
                context.SaveChanges(false);
            }

            // การชำระค่าธรรมเนียมประจำปี (โรงพยาบาล)
            var hssHospitalFee = context.Applications.Where(e => e.ApplicationSysName == "APP_CLINIC_OVER_NIGHT" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssHospitalFee != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalFee.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalFee.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalFee.SigningPositions);

                // update
                hssHospitalFee.IsEnableELicense = true;
                hssHospitalFee.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalFee.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalFee.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalFee.SigningDocumentCitizenTemplateID = "f7b5667f-6f6c-4d49-9b5a-b532041debee";
                hssHospitalFee.SigningDocumentJuristicTemplateID = "f7b5667f-6f6c-4d49-9b5a-b532041debee";
                hssHospitalFee.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalFee.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalFee.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "เอกสารเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ชำระค่าธรรมเนียมประจำปี",
                        Name ="YearOfFee",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "โปรดชำระค่าธรรมเนียมประจำปี",
                        Name ="Year",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                   
                    // juristic
                   new SigningExtendedData
                    {
                        Label = "เอกสารเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ชำระค่าธรรมเนียมประจำปี",
                        Name ="YearOfFee",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "โปรดชำระค่าธรรมเนียมประจำปี",
                        Name ="Year",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },

                };
                hssHospitalFee.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalFee.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalFee);
                context.SaveChanges(false);
            }

            // ขอใบอนุญาตให้ประกอบกิจการสถานพยาบาล(โรงพยาบาล)
            var hssHospitalPermission = context.Applications.Where(e => e.ApplicationSysName == "APP_HOSPITAL_PERMISSION" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();

            if (hssHospitalPermission != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalPermission.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalPermission.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalPermission.SigningPositions);

                // update
                hssHospitalPermission.IsEnableELicense = true;
                hssHospitalPermission.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalPermission.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalPermission.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalPermission.SigningDocumentCitizenTemplateID = "0a29c913-e0c3-4d52-9e59-2d42065c9060";
                hssHospitalPermission.SigningDocumentJuristicTemplateID = "0a29c913-e0c3-4d52-9e59-2d42065c9060";
                hssHospitalPermission.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalPermission.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalPermission.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่ 31 ธันวาคม พ.ศ. ",
                        Name ="PermitEndYear",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ไว้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่ 31 ธันวาคม พ.ศ. ",
                        Name ="PermitEndYear",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ให้ไว้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssHospitalPermission.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalPermission.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalPermission);
                context.SaveChanges(false);
            }

            // ขออนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)
            var hssHospitalBusiness = context.Applications.Where(e => e.ApplicationSysName == "APP_HOSPITAL_BUSINESS" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();

            if (hssHospitalBusiness != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalBusiness.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalBusiness.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalBusiness.SigningPositions);

                // update
                hssHospitalBusiness.IsEnableELicense = true;
                hssHospitalBusiness.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalBusiness.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalBusiness.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalBusiness.SigningDocumentCitizenTemplateID = "fe2c45c8-9b28-4ea7-8cc3-ad92b57c2ceb";
                hssHospitalBusiness.SigningDocumentJuristicTemplateID = "fe2c45c8-9b28-4ea7-8cc3-ad92b57c2ceb";
                hssHospitalBusiness.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalBusiness.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalBusiness.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssHospitalBusiness.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalBusiness.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalBusiness);
                context.SaveChanges(false);
            }

            // ขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)
            var hssHospitalOperation = context.Applications.Where(e => e.ApplicationSysName == "APP_HOSPITAL_OPERATION" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();

            if (hssHospitalOperation != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalOperation.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalOperation.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalOperation.SigningPositions);

                // update
                hssHospitalOperation.IsEnableELicense = true;
                hssHospitalOperation.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalOperation.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalOperation.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalOperation.SigningDocumentCitizenTemplateID = "04f999c5-7974-458a-a2b3-a82462966723";
                hssHospitalOperation.SigningDocumentJuristicTemplateID = "04f999c5-7974-458a-a2b3-a82462966723";
                hssHospitalOperation.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalOperation.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalOperation.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                     new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssHospitalOperation.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalOperation.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalOperation);
                context.SaveChanges(false);
            }

            // ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)
            var hssHospitalOperationEdit_b = context.Applications.Where(e => e.ApplicationSysName == "APP_HOSPITAL_OPERATION_EDIT_B" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();

            if (hssHospitalOperationEdit_b != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalOperationEdit_b.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalOperationEdit_b.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalOperationEdit_b.SigningPositions);

                // update
                hssHospitalOperationEdit_b.IsEnableELicense = true;
                hssHospitalOperationEdit_b.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalOperationEdit_b.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalOperationEdit_b.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalOperationEdit_b.SigningDocumentCitizenTemplateID = "c383fbe2-e7e9-4aac-969b-b71f9eb42db8";
                hssHospitalOperationEdit_b.SigningDocumentJuristicTemplateID = "c383fbe2-e7e9-4aac-969b-b71f9eb42db8";
                hssHospitalOperationEdit_b.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalOperationEdit_b.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalOperationEdit_b.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssHospitalOperationEdit_b.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalOperationEdit_b.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalOperationEdit_b);
                context.SaveChanges(false);
            }

            // ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)
            var hssHospitalBussinessRenew = context.Applications.Where(e => e.ApplicationSysName == "APP_HOSPITAL_BUSINESS_RENEW" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssHospitalBussinessRenew != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalBussinessRenew.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalBussinessRenew.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalBussinessRenew.SigningPositions);

                // update
                hssHospitalBussinessRenew.IsEnableELicense = true;
                hssHospitalBussinessRenew.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalBussinessRenew.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalBussinessRenew.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalBussinessRenew.SigningDocumentCitizenTemplateID = "";
                hssHospitalBussinessRenew.SigningDocumentJuristicTemplateID = "";
                hssHospitalBussinessRenew.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalBussinessRenew.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalBussinessRenew.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssHospitalBussinessRenew.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalBussinessRenew.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalBussinessRenew);
                context.SaveChanges(false);
            }

            // ขอต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)
            var hssHospitalOperationRenew = context.Applications.Where(e => e.ApplicationSysName == "APP_HOSPITAL_OPERATION_RENEW" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssHospitalOperationRenew != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalOperationRenew.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalOperationRenew.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalOperationRenew.SigningPositions);

                // update
                hssHospitalOperationRenew.IsEnableELicense = true;
                hssHospitalOperationRenew.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalOperationRenew.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalOperationRenew.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalOperationRenew.SigningDocumentCitizenTemplateID = "";
                hssHospitalOperationRenew.SigningDocumentJuristicTemplateID = "";
                hssHospitalOperationRenew.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalOperationRenew.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalOperationRenew.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                     new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssHospitalOperationRenew.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalOperationRenew.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalOperationRenew);
                context.SaveChanges(false);
            }


            // ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)
            var hssHospitalBussinessEdit = context.Applications.Where(e => e.ApplicationSysName == "APP_HOSPITAL_BUSINESS_EDIT" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssHospitalBussinessEdit != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalBussinessEdit.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalBussinessEdit.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalBussinessEdit.SigningPositions);

                // update
                hssHospitalBussinessEdit.IsEnableELicense = true;
                hssHospitalBussinessEdit.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalBussinessEdit.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalBussinessEdit.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalBussinessEdit.SigningDocumentCitizenTemplateID = "";
                hssHospitalBussinessEdit.SigningDocumentJuristicTemplateID = "";
                hssHospitalBussinessEdit.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalBussinessEdit.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalBussinessEdit.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssHospitalBussinessEdit.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalBussinessEdit.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalBussinessEdit);
                context.SaveChanges(false);
            }

            // ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (โรงพยาบาล)
            var hssHospitalOperationEdit = context.Applications.Where(e => e.ApplicationSysName == "APP_HOSPITAL_OPERATION_EDIT" && !e.IsDeleted)
               .Include(e => e.SigningExtendedDatas)
               .Include(e => e.SigningPersons)
               .Include(e => e.SigningPositions)
               .FirstOrDefault();

            if (hssHospitalOperationEdit != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(hssHospitalOperationEdit.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(hssHospitalOperationEdit.SigningPersons);
                context.SigningPositions.RemoveRange(hssHospitalOperationEdit.SigningPositions);

                // update
                hssHospitalOperationEdit.IsEnableELicense = true;
                hssHospitalOperationEdit.ELicenseConsumerKey = "6099b1b5-92df-4c72-9651-114b1656cbe7";
                hssHospitalOperationEdit.ELicenseSecret = "7Y4JWMjkp5a";
                hssHospitalOperationEdit.ELicenseXMLStandard = EdocumentXMLStandard.hl7.ToString();
                hssHospitalOperationEdit.SigningDocumentCitizenTemplateID = "";
                hssHospitalOperationEdit.SigningDocumentJuristicTemplateID = "";
                hssHospitalOperationEdit.SigningDocumentType = EDocumentPermitType.Template.ToString();
                hssHospitalOperationEdit.SigningType = EDocumentType.OrgByPerson.ToString();
                hssHospitalOperationEdit.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                     new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ชื่อผู้ได้รับอนุญาต",
                        Name ="IdentifierName",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตให้ไว้ ณ วันที่",
                        Name ="PeriodStart",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตฉบับนี้ใช้ได้จนถึงวันที่",
                        Name ="PeriodEnd",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                hssHospitalOperationEdit.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                hssHospitalOperationEdit.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, hssHospitalOperationEdit);
                context.SaveChanges(false);
            }
            
            #endregion

            #region[สคบ]
            //ขอจดทะเบียนการประกอบธุรกิจขายตรง
            var ocpbDirectSell = context.Applications.Where(e => e.ApplicationSysName == "ขอจดทะเบียนการประกอบธุรกิจขายตรง" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();
            if (ocpbDirectSell != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(ocpbDirectSell.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(ocpbDirectSell.SigningPersons);
                context.SigningPositions.RemoveRange(ocpbDirectSell.SigningPositions);

                // update
                ocpbDirectSell.IsEnableELicense = true;
                ocpbDirectSell.ELicenseConsumerKey = "1946434c-54c5-4325-828a-f5b3e44ae5c4";
                ocpbDirectSell.ELicenseSecret = "ojarG9Sd7zm";
                ocpbDirectSell.ELicenseXMLStandard = EdocumentXMLStandard.UNCEFACT.ToString();
                ocpbDirectSell.SigningDocumentCitizenTemplateID = "20491b17-097f-4c52-972b-f4b17b9b4523";
                ocpbDirectSell.SigningDocumentJuristicTemplateID = "20491b17-097f-4c52-972b-f4b17b9b4523";
                ocpbDirectSell.SigningDocumentType = EDocumentPermitType.Template.ToString();
                ocpbDirectSell.SigningType = EDocumentType.OrgByPerson.ToString();
                ocpbDirectSell.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ตั้งแต่วันที่",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ออกให้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ตั้งแต่วันที่",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ออกให้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                ocpbDirectSell.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                ocpbDirectSell.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, ocpbDirectSell);
                context.SaveChanges(false);
            }


            //ขอจดทะเบียนการประกอบธุรกิจตลาดแบบตรง
            var ocpbDirectMarketing = context.Applications.Where(e => e.ApplicationSysName == "ขอจดทะเบียนการประกอบธุรกิจตลาดแบบตรง" && !e.IsDeleted)
                .Include(e => e.SigningExtendedDatas)
                .Include(e => e.SigningPersons)
                .Include(e => e.SigningPositions)
                .FirstOrDefault();
            if (ocpbDirectMarketing != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(ocpbDirectMarketing.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(ocpbDirectMarketing.SigningPersons);
                context.SigningPositions.RemoveRange(ocpbDirectMarketing.SigningPositions);

                // update
                ocpbDirectMarketing.IsEnableELicense = true;
                ocpbDirectMarketing.ELicenseConsumerKey = "1946434c-54c5-4325-828a-f5b3e44ae5c4";
                ocpbDirectMarketing.ELicenseSecret = "ojarG9Sd7zm";
                ocpbDirectMarketing.ELicenseXMLStandard = EdocumentXMLStandard.UNCEFACT.ToString();
                ocpbDirectMarketing.SigningDocumentCitizenTemplateID = "c73c89f0-015d-4ec3-82b4-8aaaba2b4178";
                ocpbDirectMarketing.SigningDocumentJuristicTemplateID = "c73c89f0-015d-4ec3-82b4-8aaaba2b4178";
                ocpbDirectMarketing.SigningDocumentType = EDocumentPermitType.Template.ToString();
                ocpbDirectMarketing.SigningType = EDocumentType.OrgByPerson.ToString();
                ocpbDirectMarketing.SigningExtendedDatas = new List<SigningExtendedData>
                {
                    // citizen
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ตั้งแต่วันที่",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },
                    new SigningExtendedData
                    {
                        Label = "ออกให้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Citizen
                    },

                    // juristic
                    new SigningExtendedData
                    {
                        Label = "ใบอนุญาตเลขที่",
                        Name ="Identifier",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ตั้งแต่วันที่",
                        Name ="PermitStartDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                    new SigningExtendedData
                    {
                        Label = "ออกให้ ณ วันที่",
                        Name ="PermitDate",
                        Value = "",
                        Type = SigningExtendedDataType.Text,
                        UserType = UserTypeEnum.Juristic
                    },
                };
                ocpbDirectMarketing.SigningPersons = new List<SigningPerson>
                {
                    new SigningPerson
                    {
                        Order = 1,
                        CitizenID = "3473759856986",
                        FirstName ="biz",
                        LastName = "signer",
                        Position = "นายทะเบียน",
                        SignatureFileName = "untitle.jpg",
                        SignatureBase64 = "data:image/jpeg;base64,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"
                    }
                };
                ocpbDirectMarketing.SigningPositions = new List<SigningPosition>
                {
                    new SigningPosition
                    {
                        Order = 1,
                        Left = "350",
                        Bottom = "320",
                        Width = "200",
                        Height ="50"
                    }
                };

                context.Applications.AddOrUpdate(e => e.ApplicationID, ocpbDirectMarketing);
                context.SaveChanges(false);
            }

            #endregion

            #region [สรรพสามิตร]

            // ขอใบอนุญาจำหหน่ายสุรา
            var ExciseAl_lLicense = context.Applications
                                          .Include(e => e.SigningExtendedDatas)
                                          .Include(e => e.SigningPersons)
                                          .Include(e => e.SigningPositions)
                                          .Where(e => e.ApplicationSysName == "ขอใบอนุญาตขายสุรา" && !e.IsDeleted).FirstOrDefault();

            if (ExciseAl_lLicense != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(ExciseAl_lLicense.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(ExciseAl_lLicense.SigningPersons);
                context.SigningPositions.RemoveRange(ExciseAl_lLicense.SigningPositions);

                // update
                ExciseAl_lLicense.IsEnableELicense = true;
                ExciseAl_lLicense.ELicenseConsumerKey = "e85b17ed-9fb5-4d9a-ae00-30123f3bf151";
                ExciseAl_lLicense.ELicenseSecret = "ATrfb7WgDFI";
                ExciseAl_lLicense.SigningDocumentCitizenTemplateID = "217c6bdd-84d9-4da6-ae32-ca6c76253b04";
                ExciseAl_lLicense.SigningDocumentJuristicTemplateID = "217c6bdd-84d9-4da6-ae32-ca6c76253b04";
                ExciseAl_lLicense.SigningDocumentType = EDocumentPermitType.Organization.ToString();
                ExciseAl_lLicense.SigningType = EDocumentType.NotSign.ToString();
                context.Applications.AddOrUpdate(e => e.ApplicationID, ExciseAl_lLicense);
                context.SaveChanges(false);
            }

            // ขอใบอนุญาจำหหน่ายาสูบ
            var ExciseTob_lLicense = context.Applications
                                          .Include(e => e.SigningExtendedDatas)
                                          .Include(e => e.SigningPersons)
                                          .Include(e => e.SigningPositions)
                                          .Where(e => e.ApplicationSysName == "ขอใบอนุญาตขายยาสูบ" && !e.IsDeleted).FirstOrDefault();

            if (ExciseTob_lLicense != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(ExciseTob_lLicense.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(ExciseTob_lLicense.SigningPersons);
                context.SigningPositions.RemoveRange(ExciseTob_lLicense.SigningPositions);

                // update
                ExciseTob_lLicense.IsEnableELicense = true;
                ExciseTob_lLicense.ELicenseConsumerKey = "e85b17ed-9fb5-4d9a-ae00-30123f3bf151";
                ExciseTob_lLicense.ELicenseSecret = "ATrfb7WgDFI";
                ExciseTob_lLicense.SigningDocumentCitizenTemplateID = "ccf6dc34-5d05-4f75-ad57-8298bd189643";
                ExciseTob_lLicense.SigningDocumentJuristicTemplateID = "ccf6dc34-5d05-4f75-ad57-8298bd189643";
                ExciseTob_lLicense.SigningDocumentType = EDocumentPermitType.Organization.ToString();
                ExciseTob_lLicense.SigningType = EDocumentType.NotSign.ToString();
                context.Applications.AddOrUpdate(e => e.ApplicationID, ExciseTob_lLicense);
                context.SaveChanges(false);
            }

            // ขอใบอนุญาจำหหน่ายไพ่
            var ExciseCard_lLicense = context.Applications
                                          .Include(e => e.SigningExtendedDatas)
                                          .Include(e => e.SigningPersons)
                                          .Include(e => e.SigningPositions)
                                          .Where(e => e.ApplicationSysName == "ขออนุญาตขายไพ่" && !e.IsDeleted).FirstOrDefault();

            if (ExciseCard_lLicense != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(ExciseCard_lLicense.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(ExciseCard_lLicense.SigningPersons);
                context.SigningPositions.RemoveRange(ExciseCard_lLicense.SigningPositions);

                // update
                ExciseCard_lLicense.IsEnableELicense = true;
                ExciseCard_lLicense.ELicenseConsumerKey = "e85b17ed-9fb5-4d9a-ae00-30123f3bf151";
                ExciseCard_lLicense.ELicenseSecret = "ATrfb7WgDFI";
                ExciseCard_lLicense.SigningDocumentCitizenTemplateID = "986635fe-13b0-4e6f-aa43-5d88e30cee69";
                ExciseCard_lLicense.SigningDocumentJuristicTemplateID = "986635fe-13b0-4e6f-aa43-5d88e30cee69";
                ExciseCard_lLicense.SigningDocumentType = EDocumentPermitType.Organization.ToString();
                ExciseCard_lLicense.SigningType = EDocumentType.NotSign.ToString();
                context.Applications.AddOrUpdate(e => e.ApplicationID, ExciseCard_lLicense);
                context.SaveChanges(false);
            }

            #endregion

            #region [ท่องเที่ยว]

            // ขอใบอนุญาตประกอบธุรกิจนำเที่ยว
            var tourismBusinesslLicense = context.Applications
                                          .Include(e => e.SigningExtendedDatas)
                                          .Include(e => e.SigningPersons)
                                          .Include(e => e.SigningPositions)
                                          .Where(e => e.ApplicationSysName == "APP_TOURISM_BUSINESS" && !e.IsDeleted).FirstOrDefault();

            if (tourismBusinesslLicense != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(tourismBusinesslLicense.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(tourismBusinesslLicense.SigningPersons);
                context.SigningPositions.RemoveRange(tourismBusinesslLicense.SigningPositions);

                // update
                tourismBusinesslLicense.IsEnableELicense = true;
                tourismBusinesslLicense.ELicenseConsumerKey = "974afb8d-8974-43af-9282-02fdedfd299e";
                tourismBusinesslLicense.ELicenseSecret = "OFpPlC2lQPb";
                tourismBusinesslLicense.SigningDocumentCitizenTemplateID = "3e4db9a8-5261-4757-9b63-0a62d3ccf5e6";
                tourismBusinesslLicense.SigningDocumentJuristicTemplateID = "3e4db9a8-5261-4757-9b63-0a62d3ccf5e6";
                tourismBusinesslLicense.SigningDocumentType = EDocumentPermitType.Organization.ToString();
                tourismBusinesslLicense.SigningType = EDocumentType.NotSign.ToString();
                context.Applications.AddOrUpdate(e => e.ApplicationID, tourismBusinesslLicense);
                context.SaveChanges(false);
            }

            #endregion

            #region [กสทช]

            // ขอใบอนุญาตให้ค้า/ให้ค้าเพื่อการซ่อมแซมซึ่งเครื่องวิทยุคมนาคม
            var NBTCRadioLicense = context.Applications
                                          .Include(e => e.SigningExtendedDatas)
                                          .Include(e => e.SigningPersons)
                                          .Include(e => e.SigningPositions)
                                          .Where(e => e.ApplicationSysName == "ใบอนุญาตให้ค้า/ให้ค้าเพื่อการซ่อมแซมซึ่งเครื่องวิทยุคมนาคม" && !e.IsDeleted).FirstOrDefault();

            if (NBTCRadioLicense != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(NBTCRadioLicense.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(NBTCRadioLicense.SigningPersons);
                context.SigningPositions.RemoveRange(NBTCRadioLicense.SigningPositions);

                // update
                NBTCRadioLicense.IsEnableELicense = true;
                NBTCRadioLicense.ELicenseConsumerKey = "6885e11e-9096-4e39-b577-f182ccdc1172";
                NBTCRadioLicense.ELicenseSecret = "hc22cGjxnSk";
                NBTCRadioLicense.SigningDocumentCitizenTemplateID = "a6d608b2-0c74-4e46-9142-9a849c19e7dc";
                NBTCRadioLicense.SigningDocumentJuristicTemplateID = "a6d608b2-0c74-4e46-9142-9a849c19e7dc";
                NBTCRadioLicense.SigningDocumentType = EDocumentPermitType.Organization.ToString();
                NBTCRadioLicense.SigningType = EDocumentType.NotSign.ToString();
                context.Applications.AddOrUpdate(e => e.ApplicationID, NBTCRadioLicense);
                context.SaveChanges(false);
            }

            #endregion

            #region [กรมขนส่ง]

            // ขอใบอนุญาตประกอบการขนส่งไม่ประจำทางด้วยรถบรรทุก
                        
            var DLTTruckLicense = context.Applications
                                          .Include(e => e.SigningExtendedDatas)
                                          .Include(e => e.SigningPersons)
                                          .Include(e => e.SigningPositions)
                                          .Where(e => e.ApplicationSysName == "APP_TRANSPORT_NON_REGULAR_TRUCK" && !e.IsDeleted).FirstOrDefault();

            if (DLTTruckLicense != null)
            {
                // clear
                context.SigningExtendedDatas.RemoveRange(DLTTruckLicense.SigningExtendedDatas);
                context.SigningPersons.RemoveRange(DLTTruckLicense.SigningPersons);
                context.SigningPositions.RemoveRange(DLTTruckLicense.SigningPositions);

                // update
                DLTTruckLicense.IsEnableELicense = true;
                DLTTruckLicense.ELicenseConsumerKey = "0b678692-7e92-43e8-972b-a9ede5262cc8";
                DLTTruckLicense.ELicenseSecret = "uyl71ABVOzC";
                DLTTruckLicense.SigningDocumentCitizenTemplateID = "9141332c-74d5-4f13-bf16-f1c674d18a0c";
                DLTTruckLicense.SigningDocumentJuristicTemplateID = "9141332c-74d5-4f13-bf16-f1c674d18a0c";
                DLTTruckLicense.SigningDocumentType = EDocumentPermitType.Organization.ToString();
                DLTTruckLicense.SigningType = EDocumentType.NotSign.ToString();
                context.Applications.AddOrUpdate(e => e.ApplicationID, DLTTruckLicense);
                context.SaveChanges(false);
            }

            #endregion
        }
    }
}
