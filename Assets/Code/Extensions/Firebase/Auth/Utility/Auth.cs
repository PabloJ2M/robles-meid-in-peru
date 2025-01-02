using System;
using UnityEngine;

namespace Firebase.Auth
{
    public interface IAuthType
    {
        public string Json => JsonUtility.ToJson(this);
    }

    [Serializable] public struct SimpleAuth : IAuthType
    {
        public string email;
        public string password;
        public string username;
        public bool returnSecureToken;

        public SimpleAuth(string email, string password)
        {
            this.email = email;
            this.password = password;
            returnSecureToken = true;
            username = null;
        }
        public SimpleAuth(string email, string password, string username)
        {
            this.email = email;
            this.password = password;
            this.username = username;
            returnSecureToken = true;
        }
    }

    [Serializable] public struct CredentialsAuth : IAuthType
    {
        public string postBody;
        public string requestUri;
        public bool returnSecureToken;
        public bool returnIdpCredential;

        public CredentialsAuth(string token)
        {
            this.postBody = token;
            requestUri = "http://localhost";
            returnSecureToken = true;
            returnIdpCredential = true;
        }
    }
}