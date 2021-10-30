namespace Twitter.Models
{
    public class NodoDoblementeEnlazado
    {
        public NodoDoblementeEnlazado siguiente { get; set; }
        public NodoDoblementeEnlazado anterior { get; set; }
        public Tweet tweet { get; set; }
        public NodoDoblementeEnlazado()
        {
            siguiente = null;
            anterior = null;
            tweet = null;
        }
    }
}