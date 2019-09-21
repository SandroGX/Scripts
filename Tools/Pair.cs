public class Pair<T, K>
{
    public T t;
    public K k;

    public Pair(T t, K k) { this.t = t; this.k = k; }

    public override bool Equals(object obj)
    {
        return obj.GetType() == GetType() &&
            ((Pair<T, K>)obj).t.Equals(t) &&
            ((Pair<T, K>)obj).k.Equals(k);
    }

    public override int GetHashCode()
    {
        return t.GetHashCode() + k.GetHashCode();
    }
}
