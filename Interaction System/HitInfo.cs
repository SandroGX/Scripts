namespace GX
{
    public class HitInfo<T> : IHitInfo
    {
        public Hitbox Me { get; set; }
        public Hitbox Other { get; set; }
        private T t;

        public HitInfo(T t)
        {
            this.t = t;
        }

        public T Get() { return t;  }
    }
}
