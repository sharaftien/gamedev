
namespace Almoravids.Interfaces
{
    public interface ICamera
    {
        Vector2 Position { get; set; }
        void Update(Vector2 targetPosition, bool clampToMap = true);
        Matrix GetSnappedTransformMatrix(); // GetTransformMatrix()
        Matrix GetSmoothTransformMatrix();  // new
    }
}
