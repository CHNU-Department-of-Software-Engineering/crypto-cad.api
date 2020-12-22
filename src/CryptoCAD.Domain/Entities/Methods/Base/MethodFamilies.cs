namespace CryptoCAD.Domain.Entities.Methods.Base
{
    public enum MethodFamilies
    {
        Custom = 0,

        DES = 1,
        AES = 2,
        GOST = 3,

        SHA256 = 11,
        SHA512 = 12,
        MD5 = 13
    }

    public static class StandardMethodFamiliesExtensions
    {
        public static string ToFriendlyString(this MethodFamilies family)
        {
            switch (family)
            {
                case MethodFamilies.DES:
                    return "des";
                case MethodFamilies.AES:
                    return "aes";
                case MethodFamilies.GOST:
                    return "gost";
                case MethodFamilies.SHA256:
                    return "sha256";
                case MethodFamilies.SHA512:
                    return "sha512";
                case MethodFamilies.MD5:
                    return "md5";
                case MethodFamilies.Custom:
                default:
                    return "custom";
            }
        }

        public static MethodFamilies ToMethodFamily(this string family)
        {
            switch (family.ToLowerInvariant())
            {
                case "des":
                    return MethodFamilies.DES;
                case "aes":
                    return MethodFamilies.AES;
                case "gost":
                    return MethodFamilies.GOST;
                case "sha256":
                    return MethodFamilies.SHA256;
                case "sha512":
                    return MethodFamilies.SHA512;
                case "md5":
                    return MethodFamilies.MD5;
                case "custom":
                default:
                    return MethodFamilies.Custom;
            }
        }
    }
}