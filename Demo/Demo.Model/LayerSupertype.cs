namespace Demo.Model
{
    public abstract class LayerSupertype
    {
        public int Id { get; set; }

        public string Code { get; set; }
    }

    public class User : LayerSupertype
    {
        public string Name { get; set; }
    }

}