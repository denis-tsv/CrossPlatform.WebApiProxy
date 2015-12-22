using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using AdventureWorks.UILogic.Models;
using AdventureWorks.UILogic.Services;
using AdventureWorks.UILogic.WebApiProxy;

namespace AdventureWorks.UILogic.WebApiProxyServices
{
    public class IdentityServiceGeneratedProxy: IIdentityService
    {
        private readonly IWebApiProxyFactory _proxyFactory;

        public IdentityServiceGeneratedProxy(IWebApiProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }

        public async Task<LogOnResult> LogOnAsync(string userId, string password)
        {
            using (var client = _proxyFactory.GetIdentityClient())
            {
                // Ask the server for a password challenge string
                var requestId = CryptographicBuffer.EncodeToHexString(CryptographicBuffer.GenerateRandom(4));
                var challengeEncoded = await client.GetPasswordChallengeAsync(requestId);
                challengeEncoded = challengeEncoded.Replace(@"""", string.Empty);
                var challengeBuffer = CryptographicBuffer.DecodeFromHexString(challengeEncoded);

                // Use HMAC_SHA512 hash to encode the challenge string using the password being authenticated as the key.
                var provider = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha512);
                var passwordBuffer = CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf8);
                var hmacKey = provider.CreateKey(passwordBuffer);
                var buffHmac = CryptographicEngine.Sign(hmacKey, challengeBuffer);
                var hmacString = CryptographicBuffer.EncodeToHexString(buffHmac);

                var userInfo = await client.GetIsValidAsync(userId, requestId, hmacString);

                _proxyFactory.SaveCookies();

                return new LogOnResult
                {
                    UserInfo = userInfo
                };
            }
        }

        public async Task<bool> VerifyActiveSessionAsync(string userId)
        {
            using (var client = _proxyFactory.GetIdentityClient())
            {
                var result = await client.GetIsValidSessionAsync();
                return result;
            }
        }
    }
}
