using System;

namespace Firebase.Auth
{
    [Serializable] public struct Token
    {
        public string id_token;
        public string expires_in;
        public string refresh_token;
    }

    [Serializable] public struct RefreshTokenRequest
    {
        public string grant_type;
        public string refresh_token;

        public RefreshTokenRequest(string refresh_token)
        {
            grant_type = "refresh_token";
            this.refresh_token = refresh_token;
        }
    }

    //deleted fields {string:kind, string:email, bool:registered}
    [Serializable] public struct User
    {
        public string idToken;
        public string localId;
        public string displayName;

        public string expiresIn;
        public string refreshToken;

        public string Token => idToken;
        public string UserID => localId;
        public string DisplayName => displayName;

        public void RefreshToken(Token json)
        {
            idToken = json.id_token;
            expiresIn = json.expires_in;
            refreshToken = json.refresh_token;
        }
    }
}