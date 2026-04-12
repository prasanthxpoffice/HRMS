namespace DB.Constants
{
    public static class StoredProcedure
    {
        public static class HRMSDB
        {
            public static class Admin
            {
                public const string spCompanyContractGet = "admin.spCompanyContractGet";
                public const string spCompanyContractSave = "admin.spCompanyContractSave";
                public const string spCompanyContractDelete = "admin.spCompanyContractDelete";

                public const string spCompanyPositionGet = "admin.spCompanyPositionGet";
                public const string spCompanyPositionSave = "admin.spCompanyPositionSave";
                public const string spCompanyPositionDelete = "admin.spCompanyPositionDelete";

                public const string spHolidayDetailsGet = "admin.spHolidayDetailsGet";
                public const string spHolidayDetailsSave = "admin.spHolidayDetailsSave";
                public const string spHolidayDetailsDelete = "admin.spHolidayDetailsDelete";

                public const string spLeaveContractPositionGet = "admin.spLeaveContractPositionGet";
            }

            public static class Policy
            {
                public const string spLeavePolicySave = "policy.spLeavePolicySave";
            }

            public static class Master
            {
                public const string spGenericGet = "master.spGenericGet";
                public const string spGenderGet = "master.spGenderGet";

                public const string spCompanyGet = "master.spCompanyGet";
                public const string spCompanySave = "master.spCompanySave";
                public const string spCompanyDelete = "master.spCompanyDelete";

                public const string spPositionGet = "master.spPositionGet";
                public const string spPositionSave = "master.spPositionSave";
                public const string spPositionDelete = "master.spPositionDelete";

                public const string spLeaveTypeGet = "master.spLeaveTypeGet";
                public const string spLeaveTypeSave = "master.spLeaveTypeSave";
                public const string spLeaveTypeDelete = "master.spLeaveTypeDelete";

                public const string spContractTypeGet = "master.spContractTypeGet";
                public const string spContractTypeSave = "master.spContractTypeSave";
                public const string spContractTypeDelete = "master.spContractTypeDelete";

                public const string spHolidaysGet = "master.spHolidaysGet";
                public const string spHolidaysSave = "master.spHolidaysSave";
                public const string spHolidaysDelete = "master.spHolidaysDelete";

                public const string spReligionGet = "master.spReligionGet";
                public const string spNationalityGet = "master.spNationalityGet";
                public const string spShiftTypeGet = "master.spShiftTypeGet";
            }
        }

        public static class CentralLogin
        {
            public const string spGetUserRoles = "dbo.spGetUserRoles";
            public const string spGetUserMenus = "dbo.spGetUserMenus";
        }
    }
}
