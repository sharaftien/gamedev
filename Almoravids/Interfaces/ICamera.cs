using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Interfaces
{
    public interface ICamera
    {
        Vector2 Position { get; set; }
        void Update(Vector2 targetPosition, bool clampToMap = true);
        Matrix GetTransformMatrix();
    }
}
