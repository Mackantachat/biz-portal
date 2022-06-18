using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Report.Page2
{
    public class PermitRequestStatistics
    {
        public string YearSelected { get; set; }
        public string MonthSelected { get; set; }
        public string RequestorSelected { get; set; }
        public string OrganizationSelected { get; set; }
        public string ProvinceSelected { get; set; }
        public string DistrictSelected { get; set; }
        public string PermitSelected { get; set; }

        #region All Permit
        public int? AllPermit { get; set; }
        public int? AllPermitCompleted { get; set; }
        public int? AllPermitInProgress { get; set; }
        public int? AllPermitInExpectedFinishDate { get; set; }
        public decimal? AllPermitFee { get; set; }
        #endregion

        #region All Permit Selected Year. Default this year if empty
        public int? AllPermitYearSelected { get; set; }
        public int? AllPermitCompletedYearSelected { get; set; }
        public int? AllPermitInProgressYearSelected { get; set; }
        public int? AllPermitInExpectedFinishDateYearSelected { get; set; }
        public decimal? AllPermitFeeYearSelected { get; set; }
        #endregion

        #region All Permit Latest Month.
        public int? AllPermitLatestMonth { get; set; }
        public int? AllPermitCompletedLatestMonth { get; set; }
        public int? AllPermitInProgressLatestMonth { get; set; }
        public int? AllPermitInExpectedFinishDateLatestMonth { get; set; }
        public decimal? AllPermitFeeLatestMonth { get; set; }
        #endregion

        #region SLA
        public SLADataSet slaDataSet { get; set; }
        public class SLADataSet
        {
            public string[] monthLabels { get; set; }
            public int?[] PermitNotStartedYet { get; set; }
            public int?[] PermitInTime { get; set; }
            public int?[] PermitOutTime { get; set; }
            public int?[] PermitDoneInTime { get; set; }
            public int?[] PermitDoneOutTime { get; set; }
        }

        #endregion

        #region IdentityType
        public int? CitizenCount { get; set; }
        public int? JuristicCount { get; set; }
        #endregion

        #region RequestByOrganization
        public RequestByOrganization requestByOrganization { get; set; }
        public class RequestByOrganization
        {
            public string[] OrganizationLabelsByCitizen { get; set; }
            public string[] OrganizationLabelsByJuristic { get; set; }
            public int?[] OrganizationRequestCountByCitizen { get; set; }
            public int?[] OrganizationRequestCountByJuristic { get; set; }
        }
        #endregion

        #region RequestByPermitName
        public RequestByPermitName requestByPermitName { get; set; }
        public class RequestByPermitName
        {
            public string[] PermitNameLabelsByCitizen { get; set; }
            public string[] PermitNameLabelsByJuristic { get; set; }
            public int?[] PermitNameRequestCountByCitizen { get; set; }
            public int?[] PermitNameRequestCountByJuristic { get; set; }
        }
        #endregion

        #region Average Day Each Status and Each Request
        public decimal? CheckStatusAverageDay { get; set; }
        public decimal? PendingStatusAverageDay { get; set; }
        public decimal? PaidFeeStatusAverageDay { get; set; }
        public decimal? PublishPermitStatusAverageDay { get; set; }
        public int RejectedOnCheckStatus { get; set; }
        public int RejectedOnPendingStatus { get; set; }
        #endregion
    }

    public class PermitRequestStatisticsParameterList
    {
        public List<Select2Opt> YearList { get; set; }
        public List<Select2Opt> MonthList { get; set; }
        public List<Select2Opt> RequestorTypeList { get; set; }
        public List<Select2Opt> OrganizationList { get; set; }
        public List<Select2Opt> PermitList { get; set; }
    }
}
