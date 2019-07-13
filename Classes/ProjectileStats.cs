namespace elemental.Classes {
    public class ProjectileStats {
        public static ProjectileStats bullet => new ProjectileStats(7,2,4,14);
        public int damage;
        public float knockback;
        public float speed;
        public int id;
        public ProjectileStats() : this(0,0,0,0){}
        public ProjectileStats(int d, float k, float s, int i){
            damage = d;
            knockback = k;
            speed = s;
            id = i;
        }
    }
}