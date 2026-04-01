public class CircularProgressDrawable : IDrawable
{
    private double _progress = 1.0;
    public double Progress
    {
        get => _progress;
        set => _progress = Math.Clamp(value, 0, 1);
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float stroke = 5f;
        float radius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2 - stroke - 1;
        float centerX = dirtyRect.Width / 2;
        float centerY = dirtyRect.Height / 2;

        // Background circle (always full)
        canvas.StrokeColor = Colors.Gray;
        canvas.StrokeSize = stroke;
        canvas.DrawCircle(centerX, centerY, radius);

        // Progress arc – starts full (360°), shrinks clockwise
        if (Progress > 0)
        {
            canvas.StrokeColor = Colors.Green;
            canvas.StrokeSize = stroke;
            float sweepAngle = (float)(360 * Progress);

            canvas.DrawArc(
                centerX - radius,
                centerY - radius,
                radius * 2,
                radius * 2,
                -90,           // start at top (12 o'clock)
                sweepAngle,    // positive = clockwise
                false,         // not closed
                false);        // not filled
        }
    }
}
