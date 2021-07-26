using UnityEngine;
namespace HappyBat
{
    public static class ExtensionMethods
    {
        public static bool CheckIfRectsOverlap(this RectTransform rectTrans1, RectTransform rectTrans2)
        {
            Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
            Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

            return rect1.Overlaps(rect2);
        }
        public static Vector3 GetSignedEulerAngles(Vector3 angles)
        {
            Vector3 signedAngles = Vector3.zero;
            for (int i = 0; i < 3; i++)
            {
                signedAngles[i] = (angles[i] + 180f) % 360f - 180f;
            }
            return signedAngles;
        }
    }
}
