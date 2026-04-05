namespace DB.Constants
{
    public static class StoredProcedure
    {
        public static class HRMSDB
        {
            public static class Master
            {
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
            }
        }
    }
}
