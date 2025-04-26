Unity Editor tool that automatically sets the anchors of selected UI elements based on their current position and size within their parent RectTransform.
This helps maintain consistent layouts across different resolutions by removing hardcoded positions and sizes.

How to Use:
  1. Import the script inside your Unity project.
  2. Make sure your Canvas is set to Screen Space - Camera | Screen Space - Overlay, this does not work in World Space.
  3. Place your UI Elements where you want them to be.
  4. With the UI element selected, Go to Tools -> Auto Anchor UI or Press Ctrl + Alt + A

Your UI Elements should now be where you placed them no matter what device you are using.
