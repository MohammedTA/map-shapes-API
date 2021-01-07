namespace MapShapes.Data.Entities
{
    using System.Collections.Generic;

    public class ShapeType : KeyInt32
    {
        protected ShapeType()
        {
        }

        public ShapeType(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
        public virtual ICollection<OverlayShape> OverlayShapes { get; set; } = new List<OverlayShape>();

        public void Edit(string name)
        {
            this.Name = name;
        }
    }
}