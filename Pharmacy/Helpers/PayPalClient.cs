using PayPalCheckoutSdk.Core;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Helpers
{
    public class PayPalClient
    {
        public static PayPalEnvironment environment()
        {
            return new SandboxEnvironment(
                "AYprUXv1Y6tPemMNwfUeJ9IMUPVMxwgPm4deenB6Z55fX5Esd400KcglzIgibM_xUllG6UQIzjZ9E16z",
                "EMMC87mIo-xnyGbR7U2nM29yeT0MBr3tAL-3NVHkQPf-RSqybCiPn0UYF6VMhss3RcV-Gvkbm6-PLBgi"
            );
        }

        public static HttpClient client()
        {
            return new PayPalHttpClient(environment());
        }

        public static HttpClient client(string refreshToken)
        {
            return new PayPalHttpClient(environment(), refreshToken);
        }
    }
}
