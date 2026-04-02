namespace DB.Constants
{
    public static class StoredProcedure
    {
        public static class HRMSDB
        {
            public static class Master
            {
                public const string spGenderGet = "master.spGenderGet";
                public const string spGenderSave = "master.spGenderSave";
                public const string spGenderDelete = "master.spGenderDelete";

                public const string spCompanyGet = "master.spCompanyGet";
                public const string spCompanySave = "master.spCompanySave";
                public const string spCompanyDelete = "master.spCompanyDelete";

                public const string spPositionGet = "master.spPositionGet";
                public const string spPositionSave = "master.spPositionSave";
                public const string spPositionDelete = "master.spPositionDelete";
            }
        }
    }
}
