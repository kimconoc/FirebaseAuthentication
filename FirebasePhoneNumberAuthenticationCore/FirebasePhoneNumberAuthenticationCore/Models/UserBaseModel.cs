namespace FirebasePhoneNumberAuthenticationCore.Models
{
    public class UserBaseModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int LocationId { get; set; }
        public string Email { get; set; }
    }
}
