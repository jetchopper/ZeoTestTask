using System;
using System.Numerics;
using Windows.UI.Composition;

namespace MatrixTestTask.Utils
{
    public static class Helper
    {
        public static ImplicitAnimationCollection GetImlicitAnimation(Visual visual)
        {
            var easing = visual.Compositor.CreateCubicBezierEasingFunction(new Vector2(0.3f, 0.5f), new Vector2(0f, 1.5f));

            var offsetImplicit = visual.Compositor.CreateVector3KeyFrameAnimation();
            offsetImplicit.Target = "Offset";
            offsetImplicit.InsertExpressionKeyFrame(1f, "This.FinalValue", easing);
            offsetImplicit.Duration = TimeSpan.FromMilliseconds(Settings.AnimationTimeMs);

            var implicitAnimations = visual.Compositor.CreateImplicitAnimationCollection();
            implicitAnimations["Offset"] = offsetImplicit;

            return implicitAnimations;
        }
    }
}
