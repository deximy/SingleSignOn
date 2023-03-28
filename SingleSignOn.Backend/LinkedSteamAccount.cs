namespace SingleSignOn.Backend
{
    public class LinkedSteamAccount
    {
        public string steam_id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
