using BlizzardFetcher.Helpers;
using BlizzardFetcher.Models;
using Newtonsoft.Json;

namespace BlizzardFetcher
{
    public class BlizzFetch
    {
        public string _client_id;
        public string _secret;
        public Auth BlizzardApiAccess = new Auth();
        private string _baseUrl = "https://{region}.api.blizzard.com/{path}?namespace=dynamic-{region}&locale=en_US&access_token={token}";

        private string getBaseUrl(string region, string path)
        {
            UpdateBlizzardAPIKey();
            return $@"{_baseUrl.Replace("{region}", region).Replace("{path}", path).Replace("{token}",BlizzardApiAccess.access_token)}";
        }

        public BlizzFetch (string client_id, string secret, Auth auth = null)
        {
            _client_id = client_id;
            _secret = secret;
        }

        public List<RealmIndexRequest.Realm> GetRealmIndexes(string region)
        {
            var helper = new GenericHelper();
            var result = helper.HTTPGET(getBaseUrl(region, "data/wow/realm/index"));

            var ls = JsonConvert.DeserializeObject<RealmIndexRequest>(result);
            return ls.realms.ToList();
        }


        public RealmDetailRequest GetRealmDetails(string region, string realmslug)
        {
            var helper = new GenericHelper();
            var result = helper.HTTPGET(getBaseUrl(region, $"data/wow/realm/${realmslug}"));
            var item = JsonConvert.DeserializeObject<RealmDetailRequest>(result);
            return item;
        }

        public List<ConnectedRealmIndexRequest.Connected_Realms> GetConnectedRealmIndexes(string region)
        {
            var helper = new GenericHelper();
            var result = helper.HTTPGET(getBaseUrl(region, "/data/wow/connected-realm/index"));

            var ls = JsonConvert.DeserializeObject<ConnectedRealmIndexRequest>(result);
            return ls.connected_realms.ToList();
        }
        public ConnectedRealmDetailsRequest GetConnectedRealmDetails(string region, int id)
        {
            var helper = new GenericHelper();
            var result = helper.HTTPGET(getBaseUrl(region, $"data/wow/connected-realm/{id}"));
            var item = JsonConvert.DeserializeObject<ConnectedRealmDetailsRequest>(result);
            return item;
        }

        private void UpdateBlizzardAPIKey()
        {
            var helper = new GenericHelper();

            if (BlizzardApiAccess.expires == 0 || BlizzardApiAccess.expires < helper.ToUnixTime(DateTime.Now))
            {
                var access = helper.Get_Access_TokenAsync(_client_id, _secret);
                access.expires = helper.ToUnixTime(DateTime.Now) + access.expires_in - 1000;
                BlizzardApiAccess = access;
            }
        }

    }
}