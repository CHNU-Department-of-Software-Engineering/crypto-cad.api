namespace CryptoCAD.Domain.Entities.Methods.Base
{
    public abstract class Method : Entity
    {
        public string Name { get; set; }
        public MethodTypes Type { get; set; }
        public MethodFamilies Family { get; set; }
    }
}