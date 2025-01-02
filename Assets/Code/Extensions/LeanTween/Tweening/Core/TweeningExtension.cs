using Unity.Mathematics;

namespace Toulouse.Effects
{
    public static class TweeningExtension
    {
        public static float3 GetDirection(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Left: return math.left();
                case Direction.Right: return math.right();
                case Direction.Top: return math.up();
                case Direction.Bottom: return math.down();
                default: return mathf.zero;
            }
        }
    }
}