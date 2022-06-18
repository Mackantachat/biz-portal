using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps
{
    public class JuristicProfile
    {
        public string JuristicType { get; set; }
        public string JuristicID { get; set; }
        public string OldJuristicID { get; set; }
        public string RegisterDate { get; set; }
        public string JuristicName_TH { get; set; }
        public string JuristicName_EN { get; set; }
        public decimal RegisterCapital { get; set; }
        public decimal? PaidRegisterCapital { get; set; }
        public int? NumberOfObjective { get; set; }
        public int? NumberOfPageOfObjective { get; set; }
        public string Address { get; set; }
        public string JuristicStatus { get; set; }
        public int NumberOfCommittee { get; set; }
        public JuristicCommittee[] CommitteeInformations { get; set; }
        public AuthorizeDescription[] AuthorizeDescriptions { get; set; }
        public StandardObjective[] StandardObjectives { get; set; }
        public AddressInformation[] AddressInformations { get; set; }

    }

    public class AddressInformation
    {
        public int Sequense { get; set; }
        public string Building { get; set; }
        public string RoomNo { get; set; }
        public string Floor { get; set; }
        public string VillageName { get; set; }
        public string AddressNo { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public string Province { get; set; }
        public string Ampur { get; set; }
        public string Tumbol { get; set; }
        public string Phone { get; set; }
    }

    public class StandardObjective
    {
        public string StanddardID { get; set; }
        public string ObjectiveDescription { get; set; }
    }

    public class AuthorizeDescription
    {
        [JsonProperty("AuthorizeDescription")]
        public string Description { get; set; }
    }

    public class JuristicCommittee
    {
        public int Sequence { get; set; }
        public string CommitteeID { get; set; }
        public string CitizenID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
    }
}
