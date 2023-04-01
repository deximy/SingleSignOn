namespace SingleSignOn.Backend.ViewModels
{
    public class CreateJwtViewModel
    {
        public string provider { get; set; }

        public string? username { get; set; }

        public string? password { get; set; }

        public string? token { get; set; }

        public string? redirect { get; set; }
    }
}
