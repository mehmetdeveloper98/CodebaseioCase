using Customer.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace CustomerAPI.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            configuration.GetSection("JwtOptions").Bind(options);
        }
    }
}
