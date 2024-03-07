namespace ClickThroughWindowExample;

using static ClickThroughWindowExample.User32Native;

public sealed class ClickThroughExtender
{
    private int m_InitialStyle;
    private IntPtr _handleOfWindow;

    public ClickThroughExtender(IntPtr handleOfWindow)
    {
        //Grab the Extended Style information for this window and store it.
        m_InitialStyle = GetWindowLongW(handleOfWindow, GWL.ExStyle);
        _handleOfWindow = handleOfWindow;
    }

    private void SetFormToTransparent()
    {

        // This creates a new extended style for our window, which takes effect immediately
        // upon being set, that combines the initial style of our window and adds the ability
        // to be transparent to the mouse.  Both Layered and Transparent must be turned on for
        // this to work and the window to render properly!
        SetWindowLongW(_handleOfWindow, GWL.ExStyle, m_InitialStyle | (int)WS_EX.Layered | (int)WS_EX.Transparent);

        // Set the Alpha for our window to the percentage specified by our TransparentAlpha trackbar.
        // Note: This has NOTHING to do with making the form transparent to the mouse!  This is solely
        // for visual effect!
        SetLayeredWindowAttributes(_handleOfWindow, 0, (byte)(255 * WindowTransparentAlpha), LWA.Alpha);
    }

    private void SetFormToOpaque()
    {

        // This resets our window's "Transparent" attribute to what it was when the application
        // was launched.  We're still keeping the "Layered" attribute turned on so our window's
        // Alpha is maintained and renders properly.
        SetWindowLongW(_handleOfWindow, GWL.ExStyle, m_InitialStyle | (int)WS_EX.Layered);

        // Set the Alpha for our window to the percentage specified by our ActiveAlpha trackbar.
        // Note: This has NOTHING to do with making the form transparent to the mouse!  This is solely
        // for visual effect!
        SetLayeredWindowAttributes(_handleOfWindow, 0, (byte)(255 * WindowActiveAlpha), LWA.Alpha);
    }

    private float _windowTransparentAlpha = 0.7f;

    /// <summary>
    /// 不能点击时的透明度
    /// </summary>
    public float WindowTransparentAlpha
    {
        get { return _windowTransparentAlpha; }
        set
        {
            _windowTransparentAlpha = value;
            SetFormToTransparent();
        }
    }

    private float _windowActiveAlpha = 1.0f;

    /// <summary>
    /// 可以点击时的透明度。不要设置Opacity属性
    /// </summary>
    public float WindowActiveAlpha
    {
        get { return _windowActiveAlpha; }
        set
        {
            _windowActiveAlpha = value;
            SetFormToOpaque();
        }
    }

    private bool _isCanClickThrough = false;

    /// <summary>
    /// 获取或设置是否可以鼠标穿透
    /// </summary>
    public bool IsCanClickThrough
    {
        get { return _isCanClickThrough; }
        set
        {
            _isCanClickThrough = value;
            if (_isCanClickThrough) SetFormToTransparent();
            else SetFormToOpaque();
        }
    }
}