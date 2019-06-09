using Microsoft.Xna.Framework;

namespace elemental.Dusts{
    public class TwoColorsAndANumber{
        public TwoColorsAndANumber(int n, Color a, Color b){
            number = n;
            color = a;
            secondcolor = b;
        }
        public override string ToString(){
            return "color 1: "+color.ToString() + "color 2: "+secondcolor.ToString() + "number: "+number;
        }
        public int number = new int();//I wasn't going to keep it like this, but I found it so hilarious that it actually works that I'm keeping it now.
        public Color color = new Color();
        public Color secondcolor = new Color();
    }
}