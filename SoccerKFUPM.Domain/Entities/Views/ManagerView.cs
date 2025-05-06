namespace SoccerKFUPM.Domain.Entities.Views
{
    public class ManagerView
    {
        public int ManagerId { get; set; }
        public string KFUPMId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int NationalityId { get; set; }
        public string? TeamName { get; set; }
        public List<PersonalContactInfoView> ContactInfos { get; set; } = new();
    }
    public class PersonalContactInfoView
    {
        public int ContactType { get; set; }
        public string Value { get; set; } = string.Empty;
    }

}


