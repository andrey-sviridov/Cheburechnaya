using AutoMapper;
using cheburechnaya_core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace cheburechnaya_core.Data {
    public class BaseController<T> : ControllerBase {

        private readonly ILogger<T> _logger;
        protected readonly IMapper _mapper;
        protected readonly User user;
        protected readonly ModelContext context;

        protected BaseController(IMapper mapper, ILogger<T> logger) {
            _mapper = mapper;
            _logger = logger;
            context = new ModelContext();
            //user = int.TryParse(request.Headers["CurrentUser"][0], out _) ? context.Users.Find(int.Parse(request.Headers["CurrentUser"][0])) : null;
        }

        protected static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv) {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0) {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0) {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0) {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged()) {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText)) {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                        using (var srDecrypt = new StreamReader(csDecrypt)) {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
