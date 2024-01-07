namespace FleetManagement.Common.Models
{
    public class DriverLicense
    {
        private LicenseType _licenseType = new LicenseType();

        public LicenseType LicenseType
        {
            get => _licenseType;
            set
            {
                _licenseType = value ?? throw new ArgumentNullException(nameof(value), "LicenseType cannot be null.");
            }
        }

        public DriverLicense()
        {

        }

    }
}
