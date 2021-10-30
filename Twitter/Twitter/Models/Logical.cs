using System;
namespace Twitter.Models
{
    public class Logical
    {
        Boolean v;
        public Logical(Boolean f)
        {
            v = f;
        }
        public void setLogical(Boolean f)
        {
            v = f;
        }
        public Boolean booleanValue()
        {
            return v;
        }
    }
}