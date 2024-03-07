namespace ClickThroughWindowExample
{
    public partial class fmMain : Form
    {
        public fmMain()
        {
            InitializeComponent();
            ClickThroughExtender extender = new(this.Handle);
            extender.IsCanClickThrough = true;
            extender.WindowActiveAlpha = 0.8f;
            extender.WindowTransparentAlpha = 0.7f;
        }
    }
}
