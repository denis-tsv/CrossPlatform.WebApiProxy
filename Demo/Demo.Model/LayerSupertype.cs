namespace Demo.Model
{
    /// <summary>
    /// Class documentation
    /// </summary>
    public abstract class LayerSupertype
    {
        /// <summary>
        /// Property documentation
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
    }

    public class User : LayerSupertype
    {
        public string Name { get; set; }
    }

}