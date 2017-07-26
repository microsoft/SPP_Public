// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
    public class SignInName
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class PasswordProfile
    {
        public object password { get; set; }
        public bool forceChangePasswordNextLogin { get; set; }
        public bool enforceChangePasswordPolicy { get; set; }
    }

    public class ADUser
    {
        public string metadata { get; set; }
        public string type { get; set; }
        public string objectType { get; set; }
        public string objectId { get; set; }
        public object deletionTimestamp { get; set; }
        public bool accountEnabled { get; set; }
        public List<SignInName> signInNames { get; set; }
        public List<object> assignedLicenses { get; set; }
        public List<object> assignedPlans { get; set; }
        public object city { get; set; }
        public object companyName { get; set; }
        public object country { get; set; }
        public string creationType { get; set; }
        public object department { get; set; }
        public object dirSyncEnabled { get; set; }
        public string displayName { get; set; }
        public object facsimileTelephoneNumber { get; set; }
        public object givenName { get; set; }
        public object immutableId { get; set; }
        public object isCompromised { get; set; }
        public object jobTitle { get; set; }
        public object lastDirSyncTime { get; set; }
        public object mail { get; set; }
        public string mailNickname { get; set; }
        public object mobile { get; set; }
        public object onPremisesSecurityIdentifier { get; set; }
        public List<object> otherMails { get; set; }
        public object passwordPolicies { get; set; }
        public PasswordProfile passwordProfile { get; set; }
        public object physicalDeliveryOfficeName { get; set; }
        public object postalCode { get; set; }
        public object preferredLanguage { get; set; }
        public List<object> provisionedPlans { get; set; }
        public List<object> provisioningErrors { get; set; }
        public List<object> proxyAddresses { get; set; }
        public string refreshTokensValidFromDateTime { get; set; }
        public object showInAddressList { get; set; }
        public object sipProxyAddress { get; set; }
        public object state { get; set; }
        public object streetAddress { get; set; }
        public object surname { get; set; }
        public object telephoneNumber { get; set; }
        public object usageLocation { get; set; }
        public string userPrincipalName { get; set; }
        public string userType { get; set; }
    }
}