namespace SysTrack.Client.Models
{
    public record UserData
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public UserData(string Name = "", string Password = "")
        { 
            this.Name = Name;
            this.Password = Password;
        }
    };
}
