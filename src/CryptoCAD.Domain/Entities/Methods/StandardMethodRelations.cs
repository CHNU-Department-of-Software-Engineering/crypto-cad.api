namespace CryptoCAD.Domain.Entities.Methods
{
    public enum StandardMethodRelations
    {
        None = 0,
        Parent = 1,
        Child = 2
    }

    public static class StandardMethodRelationsExtensions
    {
        private const string PARENT_NAME = "parent";
        private const string CHILD_NAME = "child";

        public static string ToFriendlyString(this StandardMethodRelations relation)
        {
            switch (relation)
            {
                case StandardMethodRelations.Parent:
                    return PARENT_NAME;
                case StandardMethodRelations.Child:
                    return CHILD_NAME;
                case StandardMethodRelations.None:
                default:
                    return "none";
            }
        }

        public static StandardMethodRelations ToMethodRelation(this string relation)
        {
            switch (relation.ToLowerInvariant())
            {
                case PARENT_NAME:
                    return StandardMethodRelations.Parent;
                case CHILD_NAME:
                    return StandardMethodRelations.Child;
                case "none":
                default:
                    return StandardMethodRelations.None;
            }
        }
    }
}