namespace Firebase.Auth
{
    public struct Credentials
    {
        public string tokenID { get; private set; }
        public string provider { get; private set; }

        public Credentials(string tokenID, string provider)
        {
            this.tokenID = tokenID;
            this.provider = provider;
        }
    }
}